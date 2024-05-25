using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Riu : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        MauPlayer controller = other.GetComponent<MauPlayer>();

        if (controller != null)
        {
            controller.ThayDoiMau(-10);
        }
    }
}
