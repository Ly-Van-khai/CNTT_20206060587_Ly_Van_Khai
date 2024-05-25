using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MauEnemy : MonoBehaviour
{
    public GameObject winScene;
    [Header("Health")]
    public int MauBD;
    public AudioClip winSound;
    [Header("Iframes")]
    private SpriteRenderer spriteRend;
    public int Mau { get { return MauHT; } }
    public int MauHT;
    bool kiemTra;
    float dem;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        MauHT = MauBD;
        winScene.SetActive(false);
        spriteRend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (kiemTra)
        {
            dem -= Time.deltaTime;
            if (dem < 0)
                kiemTra = false;
        }
    }
    private IEnumerator Invunerability()
    {
        // Bật trạng thái bất tử
        kiemTra = true;
        // Lưu trữ màu sắc ban đầu
        Color MauBanDau = spriteRend.color;
        // Thời gian nhấp nháy
        float ThoiGianNhapNhay = 0.1f;
        // Tổng thời gian bất tử
        float ThoiGianBatTu = 0.5f;
        // Khởi tạo thời gian đã trôi qua
        float dem = 0.0f;
        // Vòng lặp cho hiệu ứng nhấp nháy
        while (dem <= ThoiGianBatTu)
        {
            // Đặt màu sang đỏ
            spriteRend.color = Color.red;
            // Chờ thời gian flashTime
            yield return new WaitForSeconds(ThoiGianNhapNhay);
            // Đặt lại màu ban đầu
            spriteRend.color = MauBanDau;
            // Chờ thời gian flashTime
            yield return new WaitForSeconds(ThoiGianNhapNhay);
            // Cập nhật thời gian đã trôi qua
            dem += ThoiGianNhapNhay * 2; // Cập nhật thời gian đã trôi qua sau mỗi chu kỳ nhấp nháy
        }
        // Đảm bảo màu cuối cùng là màu gốc
        spriteRend.color = MauBanDau;

        // Tắt trạng thái bất tử
        kiemTra = false;
    }

    public void TruMau(int amount)
    {
        if (amount < 0)
        {
            anim.SetTrigger("dau");
            StartCoroutine(Invunerability());
        }
        MauHT = Mathf.Clamp(MauHT + amount, 0, MauBD);
        UIMauBoss.instance.SetValue(MauHT / (float)MauBD);
        if (MauHT == 0)
        {
            Destroy(gameObject);
            winScene.SetActive(true);
            SoundManager.instance.PhatNhac(winSound);
        }
    }
    public int MauHienTai()
    {
        return MauHT;
    }
}
