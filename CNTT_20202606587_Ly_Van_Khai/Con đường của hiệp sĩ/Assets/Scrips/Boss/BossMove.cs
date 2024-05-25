using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    [Header("Diem Di Chuyen")]
    public Transform DiemTrai;
    public Transform DiemPhai;

    [Header("Boss")]
    public Transform boss;

    [Header("Di Chuyen")]
    public float TocDo;
    private Vector3 BanDau;
    private bool SangTrai;

    [Header("Dung yen")]
    public float ThoiGianDung;
    private float dem;
    public Animator anim;

    [Header("Player")]
    public Transform player;
    bool PhatHien = false;
    Vector2 ViTriPlayer;
    private void Awake()
    {
        BanDau = boss.localScale;
    }
    private void OnDisable()
    {
        anim.SetBool("dichuyen", false);
    }
    private void Update()
    {
        // Hàm phát hiện player
        if (player.position.x >= DiemTrai.position.x && player.position.x <= DiemPhai.position.x)
        {
            PhatHien = true;
        }
        else
        {
            PhatHien = false;
        }

        if (PhatHien)
        {
            ViTriPlayer = player.position;
            DuoiTheo();
        }
        else
        {
            DiChuyen();
        }
    }
    void DiChuyen()
    {
        if (SangTrai)
        {
            if (boss.position.x >= DiemTrai.position.x)
                DiChuyenTheoHuong(-1);
            else
                DoiHuong();
        }
        else
        {
            if (boss.position.x <= DiemPhai.position.x)
                DiChuyenTheoHuong(1);
            else
                DoiHuong();
        }
    }
    void DuoiTheo()
    {
        // Sử dụng để lấy hướng
        float layhuong = ViTriPlayer.x - transform.position.x;
        // Chỉ sử dụng hướng trên trục x
        int huong = Mathf.RoundToInt(Mathf.Sign(layhuong)); // Lấy giá trị -1, 0 hoặc 1 tùy vào vị trí của player
        // Đặt hướng cho animation
        anim.SetBool("dichuyen", true);
        // Đặt hướng cho boss
        boss.localScale = new Vector3(Mathf.Abs(BanDau.x) * huong, BanDau.y, BanDau.z);
        // Di chuyển theo hướng tính được
        boss.position = new Vector3(boss.position.x + Time.deltaTime * huong * TocDo, boss.position.y, boss.position.z);
    }
    private void DoiHuong()
    {
        anim.SetBool("dichuyen", false);
        dem += Time.deltaTime;
        if (dem > ThoiGianDung)
            SangTrai = !SangTrai;
    }
    private void DiChuyenTheoHuong(int Huong)
    {
        dem = 0;
        anim.SetBool("dichuyen", true);
        boss.localScale = new Vector3(Mathf.Abs(BanDau.x) * Huong,BanDau.y, BanDau.z);
        boss.position = new Vector3(boss.position.x + Time.deltaTime * Huong * TocDo, boss.position.y, boss.position.z);
    }
}
