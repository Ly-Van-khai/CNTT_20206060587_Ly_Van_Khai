using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmThanh : MonoBehaviour
{
    public Transform player; 
    public float KhoangCachNghe = 5f; 
    public AudioClip soundClip; 
    private bool KiemTra = false;
    void Update()
    {
        float KhoangCach = Vector3.Distance(transform.position, player.position);
        if (KhoangCach <= KhoangCachNghe && !KiemTra)
        {
            SoundManager.instance.LapLai(soundClip);
            KiemTra = true;
        }
        else if (KhoangCach > KhoangCachNghe && KiemTra)
        {
            SoundManager.instance.DungNhac();
            KiemTra = false;
        }
    }
}
