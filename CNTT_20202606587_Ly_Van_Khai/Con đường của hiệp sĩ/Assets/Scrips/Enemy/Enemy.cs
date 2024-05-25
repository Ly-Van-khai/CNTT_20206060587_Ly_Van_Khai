using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rigidbody2d;
    public float PhamViTanCong; // Phạm vi tấn công của kẻ địch
    public float ThoiGianHoiChieu; // Thời gian cooldown giữa các lần tấn công
    private bool KiemTra = true; // Biến để kiểm tra xem kẻ địch có thể tấn công hay không
    public int SatThuong;
    private GameObject player; // GameObject của nhân vật
    public AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player"); // Gán giá trị cho biến player
    }

    void Update()
    {
        // Kiểm tra xem có nhân vật nào nằm trong phạm vi tấn công không
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, PhamViTanCong);
        bool playerFound = false; // Biến để kiểm tra xem có nhân vật nào trong tầm đánh không
        foreach (Collider2D collider in hitColliders)
        {
            if (collider.CompareTag("Player"))
            {
                player = collider.gameObject; // Gán giá trị của collider vào biến player
                playerFound = true;
                break;
            }
        }

        // Nếu có nhân vật trong tầm đánh và kẻ địch có thể tấn công (cooldown đã qua)
        if (playerFound && KiemTra)
        {
            
            // Kiểm tra vị trí của nhân vật so với kẻ địch và gọi animation tương ứng
            if (player.transform.position.x > transform.position.x)
            {
                // Nếu nhân vật ở bên phải, gọi animation tấn công sang phải
                anim.SetTrigger("TanCongPhai");
            }
            else
            {
                // Ngược lại, gọi animation tấn công sang trái
                anim.SetTrigger("TanCong");
            }
            SoundManager.instance.PhatNhac(audioClip);
            // Gọi hàm tấn công khi nhân vật vào tầm đánh của kẻ địch
            TanCong(player);

            // Khởi động cooldown
            StartCoroutine(HoiChieu());
        }
    }

    // Hàm thực hiện tấn công
    void TanCong(GameObject player)
    {
        MauPlayer playerHealth = player.GetComponent<MauPlayer>();
        if (playerHealth != null)
        {
            playerHealth.ThayDoiMau(-SatThuong);
        }
    }

    // Hàm bắt đầu cooldown
    IEnumerator HoiChieu()
    {
        KiemTra = false;
        yield return new WaitForSeconds(ThoiGianHoiChieu);
        KiemTra = true;
    }
}
