using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource TiengGame;
    private AudioSource AmThanh;

    private void Awake()
    {
        TiengGame = GetComponent<AudioSource>();
        AmThanh = transform.GetChild(0).GetComponent<AudioSource>();
        instance = this;
        changeTiengGame(0);
        changeAmThanh(0);
    }
    public void PhatNhac(AudioClip sound)
    {
        TiengGame.PlayOneShot(sound);
    }
    public void DungNhac()
    {
        TiengGame.Stop();
    }
    public void LapLai(AudioClip sound)
    {
        TiengGame.clip = sound;
        TiengGame.loop = true; 
        TiengGame.Play();
    }
    public void changeTiengGame(float GT)
    {
        ThayDoiAmLuong(1f, "TiengGame", GT, TiengGame);
    }
    public void changeAmThanh(float GT)
    {
        ThayDoiAmLuong(0.3f, "AmThanh", GT, AmThanh);
    }
    private void ThayDoiAmLuong(float AmThanh, string TenAmThanh, float GiaTri, AudioSource source)
    {
        float AmThanhHienTai = PlayerPrefs.GetFloat(TenAmThanh, 1);
        AmThanhHienTai += GiaTri;

        if (AmThanhHienTai > 1)
            AmThanhHienTai = 0;
        else if (AmThanhHienTai < 0)
            AmThanhHienTai = 1;

        float AmThanhCuoi = AmThanhHienTai * AmThanh;
        source.volume = AmThanhCuoi;
        PlayerPrefs.SetFloat(TenAmThanh, AmThanhHienTai);
    }
}
