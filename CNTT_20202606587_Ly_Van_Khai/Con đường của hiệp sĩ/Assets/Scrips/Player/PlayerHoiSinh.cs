using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHoiSinh : MonoBehaviour
{
    [SerializeField] private AudioClip checkpoint;
    private Transform CheckpointHienTai;
    private MauPlayer mau;
    private UIManager uiManager;

    private void Awake()
    {
        mau = GetComponent<MauPlayer>();
        uiManager = FindAnyObjectByType<UIManager>();
    }

    public void HoiSinh()
    {
        // Kiểm tra checkpoint
        if(CheckpointHienTai == null)
        {
            uiManager.Thua();
            return;
        }
        mau.HoiSinh(); // Hoi sinh
        transform.position = CheckpointHienTai.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            CheckpointHienTai = collision.transform;
            SoundManager.instance.PhatNhac(checkpoint);
            collision.GetComponent<Collider2D>().enabled = false;
        }
    }
}
