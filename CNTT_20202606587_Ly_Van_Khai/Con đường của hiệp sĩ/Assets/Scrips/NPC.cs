using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float ThoiGianDuyTri = 4.0f;
    public GameObject NoiDung;
    float dem;
    public GameObject ChamThan;

    void Start()
    {
        NoiDung.SetActive(false);
        dem = -1.0f;
    }

    void Update()
    {
        if (dem >= 0)
        {
            dem -= Time.deltaTime;
            if (dem < 0)
            {
                NoiDung.SetActive(false);
            }
        }
    }

    public void HienThi()
    {
        dem = ThoiGianDuyTri;
        NoiDung.SetActive(true);
        ChamThan.SetActive(false);
    }
}
