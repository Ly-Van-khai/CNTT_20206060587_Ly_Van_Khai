using System.Collections;
using UnityEngine;

public class Kiem : MonoBehaviour
{
    public float KhoangCachDC = 1f; // Khoảng cách di chuyển lên xuống
    public float TocDo = 2f; // Tốc độ di chuyển
    public bool DoiHuong = true; // Biến đánh dấu hướng di chuyển hiện tại
    private MauPlayer mau;
    public int SatThuong;
    [Header("Am Thanh")]
    public Transform player; 
    public float KhoangCachNghe = 5f;
    private bool KiemTra = false;
    public AudioClip soundClip;
    void Start()
    {
        mau = FindObjectOfType<MauPlayer>();
        // Bắt đầu Coroutine để điều khiển di chuyển lên xuống
        StartCoroutine(LenXuong());
    }
    void Update()
    {
        float KhoangCach = Vector3.Distance(transform.position, player.position);

        // Kiểm tra xem người chơi có trong tầm nghe được không
        if (KhoangCach <= KhoangCachNghe && !KiemTra)
        {
            KiemTra = true;
        }
        else if (KhoangCach > KhoangCachNghe && KiemTra)
        {
            KiemTra = false;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (mau != null)
            {
                mau.ThayDoiMau(-SatThuong);
            }
        }
    }

    IEnumerator LenXuong()
    {
        
        while (true) // Lặp vô hạn
        {
            if (KiemTra)
            {
                SoundManager.instance.PhatNhac(soundClip);
            }
            // Di chuyển lên hoặc xuống dựa trên hướng hiện tại
            float TrucY = DoiHuong ? transform.position.y + KhoangCachDC : transform.position.y - KhoangCachDC;
            Vector3 DiemDCD = new Vector3(transform.position.x, TrucY, transform.position.z);
            float KhoangCach = Vector3.Distance(transform.position, DiemDCD);

            while (KhoangCach > 0.01f)
            {
                // Di chuyển vật thể tới vị trí mới
                transform.position = Vector3.MoveTowards(transform.position, DiemDCD, TocDo * Time.deltaTime);
                KhoangCach = Vector3.Distance(transform.position, DiemDCD);
                yield return null;
            }

            // Đảo ngược hướng di chuyển
            DoiHuong = !DoiHuong;
            yield return null;
        }
    }
}
