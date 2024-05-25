using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cua : MonoBehaviour
{
    [Header("Diem Di Chuyen")]
    public Transform DiemTrai;
    public Transform DiemPhai;

    [Header("Vat")]
    public Transform rangcua;

    [Header("Di Chuyen")]
    public float TocDo;
    // Lưu trữ kích thước ban đầu của đối tượng
    private Vector3 BanDau;
    private bool SangTrai;
    private float dem;
    public float ThoiGianDung;
    public int SatThuong;
    public float TocDoQuay;
    private void Awake()
    {
        BanDau = rangcua.localScale;
    }
    void Update()
    {
        TuQuay();
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
    private void TuQuay()
    {
        // Tạo một Quaternion để lưu trữ góc quay
        Quaternion GocQuay = Quaternion.Euler(0, 0, TocDoQuay * Time.deltaTime);

        // Áp dụng quay cho game object
        rangcua.rotation *= GocQuay;
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

    void OnTriggerEnter2D(Collider2D mau)
    {
        MauPlayer mauPlayer = mau.GetComponent<MauPlayer>();

        if (mauPlayer != null)
        {
            mauPlayer.ThayDoiMau(-SatThuong);
        }
    }
}
