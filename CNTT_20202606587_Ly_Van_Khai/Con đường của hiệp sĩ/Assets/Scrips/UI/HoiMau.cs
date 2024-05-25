using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoiMau : MonoBehaviour
{
    [SerializeField] private AudioClip soundClip;
    public float ThoiGianHoiLai;
    public GameObject tim;
    void OnTriggerEnter2D(Collider2D other)
    {
        MauPlayer mauPlayer = other.GetComponent<MauPlayer>();
        if (mauPlayer.MauHT < mauPlayer.MauBD)
        {
            SoundManager.instance.PhatNhac(soundClip);
            mauPlayer.ThayDoiMau(1);
            StartCoroutine(HoiSinh());
        }
    }
    IEnumerator HoiSinh()
    {
        // Tạm thời vô hiệu hóa
        tim.SetActive(false);
        // Chờ đợi trong một khoảng thời gian
        yield return new WaitForSeconds(ThoiGianHoiLai);
        // Tái tạo và kích hoạt lại
        tim.SetActive(true);
    }
}
