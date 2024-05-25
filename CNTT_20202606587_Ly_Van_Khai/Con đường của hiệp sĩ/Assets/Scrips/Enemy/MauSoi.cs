using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MauSoi : MonoBehaviour
{
    public int MauBD;
    public int Mau { get { return MauHT; } }
    public int MauHT;
    public float timeInvincible = 1.0f;
    bool isInvincible;
    float invincibleTimer;
    private Animator anim;
    public bool boss = false;
    void Start()
    {
        anim = GetComponent<Animator>();
        MauHT = MauBD;
    }

    void Update()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
    }
    public void TruMau(int amount)
    {
        if (amount < 0)
        {
            anim.SetTrigger("dau");
        }
        MauHT = Mathf.Clamp(MauHT + amount, 0, MauBD);
        //UIMauBoss.instance.SetValue(MauHT / (float)MauBD);
        if (MauHT == 0)
        {
            anim.SetTrigger("chet");
        }
    }
    public int MauHienTai()
    {
        return MauHT;
    }
}
