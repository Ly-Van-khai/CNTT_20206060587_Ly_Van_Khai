using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MauPlayer : MonoBehaviour
{
    [Header("Suc Khoe")]
    public int MauBD;
    public int Mau { get { return MauHT; } }
    public int MauHT;
    public float ThoiGianBatTu = 1.0f;
    bool KiemTra;
    float Dem;
    private Animator anim;
    private SpriteRenderer spriteRend;
    public AudioClip SoundHurt;
    public AudioClip SoundDie;
    private bool chet = false;

    public void Awake()
    {
        anim = GetComponent<Animator>();
        MauHT = MauBD;
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        if (KiemTra)
        {
            Dem -= Time.deltaTime;
            if (Dem < 0)
                KiemTra = false;
        }
    }

    public void ThayDoiMau(int GiaTri)
    {
        if (GiaTri < 0 && MauHT > 0)
        {
            if (!KiemTra)  // Kiểm tra nếu không bất tử trước
            {
                SoundManager.instance.PhatNhac(SoundHurt);
                anim.SetTrigger("hurt");
                StartCoroutine(DoiMau());
            }
            KiemTra = true;
            Dem = ThoiGianBatTu;
        }

        MauHT = Mathf.Clamp(MauHT + GiaTri, 0, MauBD);
        UIMau.instance.SetValue(MauHT / (float)MauBD);

        if (MauHT <= 0)
        {
            if (!chet)
            {           
                anim.SetTrigger("die");
                SoundManager.instance.PhatNhac(SoundDie);
                GetComponent<Player>().enabled = false;
                chet = true; // Đặt biến thành true khi nhân vật đã chết
            }
        }
    }
    public void KetThucAniamtion()
    {
        PlayerHoiSinh HoiSinhPlayer = GetComponent<PlayerHoiSinh>();
        if (HoiSinhPlayer != null)
        {
            HoiSinhPlayer.HoiSinh();
        }
    }

    private IEnumerator DoiMau()
    {
        // Bật trạng thái bất tử
        KiemTra = true;
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
            // Chờ thời gian nhấp nháy
            yield return new WaitForSeconds(ThoiGianNhapNhay);
            // Đặt lại màu ban đầu
            spriteRend.color = MauBanDau;
            // Chờ thời gian dem
            yield return new WaitForSeconds(dem);
            // Cập nhật thời gian đã trôi qua
            dem += ThoiGianNhapNhay * 2;
        }
        // Đặt lại màu ban đầu
        spriteRend.color = MauBanDau;
        // Tắt trạng thái bất tử
        KiemTra = false;
    }

    public void HoiSinh()
    {
        ThayDoiMau(MauBD);
        chet = false;
        UIMau.instance.SetValue(MauHT / (float)MauBD);
        anim.Play("Idle");
        GetComponent<Player>().enabled = true;
    }
}
