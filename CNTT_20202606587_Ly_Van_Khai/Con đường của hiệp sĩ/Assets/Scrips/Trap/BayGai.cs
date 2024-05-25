using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BayGai : MonoBehaviour
{
    private float dem = 0.0f;
    public int SatThuong;
    void OnTriggerStay2D(Collider2D other)
    {
        MauPlayer controller = other.GetComponent<MauPlayer>();

        if (controller != null)
        {

            if (Time.time - dem >= 1.0f)
            {
                controller.ThayDoiMau(-SatThuong);
                dem = Time.time;
            }
        }
    }
}
