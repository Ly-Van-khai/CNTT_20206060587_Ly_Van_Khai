using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiChuyen : MonoBehaviour
{
    [Header("Diem Di Chuyen")]
    public Transform DiemTrai;
    public Transform DiemPhai;

    [Header("Vat")]
    public Transform rangcua;

    [Header("Di Chuyen")]
    public float TocDo;
    private Vector3 BanDau;
    private bool SangTrai;
    private float dem;
    public float ThoiGianDung;
    public int SatThuong;
    private MauPlayer MauPlayer;
    public AudioClip Box;
    private void Awake()
    {
        MauPlayer = FindAnyObjectByType<MauPlayer>();
        BanDau = rangcua.localScale;
    }
    void Update()
    {
        if (SangTrai)
        {
            if (rangcua.position.x >= DiemTrai.position.x)
                DiChuyenTheoHuong(-1);
            else
                DoiHuong();
        }
        else
        {
            if (rangcua.position.x <= DiemPhai.position.x)
                DiChuyenTheoHuong(1);
            else
                DoiHuong();
        }
    }
    private void DoiHuong()
    {
        dem += Time.deltaTime;
        if (dem > ThoiGianDung)
            SangTrai = !SangTrai;
    }

    private void DiChuyenTheoHuong(int Huong)
    {
        dem = 0;
        rangcua.localScale = new Vector3(Mathf.Abs(BanDau.x) * Huong,BanDau.y, BanDau.z);
        rangcua.position = new Vector3(rangcua.position.x + Time.deltaTime * Huong * TocDo,rangcua.position.y, rangcua.position.z);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (MauPlayer != null)
            {
                SoundManager.instance.PhatNhac(Box);
                MauPlayer.ThayDoiMau(-SatThuong);
            }
        }
    }
}
