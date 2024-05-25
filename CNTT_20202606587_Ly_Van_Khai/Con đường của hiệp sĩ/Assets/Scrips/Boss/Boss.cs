using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;
    private float dem = Mathf.Infinity;
    public float HoiChieu;
    public float KhoangCach;
    public BoxCollider2D boxCollider;
    public float PhamVi;
    private BossMove bossDC;
    private MauPlayer mau;
    public int SatThuong;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        mau = FindAnyObjectByType<MauPlayer>();
        bossDC = GetComponentInParent<BossMove>();
    }

    private void Update()
    {
        dem += Time.deltaTime;
        if (PhatHienPlayer())
        {
            if (dem >= HoiChieu)
            {
                anim.SetTrigger("tancong");
                dem = 0;
            }
        }
        if (bossDC != null)
            bossDC.enabled = !PhatHienPlayer();
    }
    private bool PhatHienPlayer()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * PhamVi * transform.localScale.x * KhoangCach,
                                              new Vector2(boxCollider.bounds.size.x * PhamVi, boxCollider.bounds.size.y),
                                              0, transform.right, 0, LayerMask.GetMask("Default"));

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * PhamVi * transform.localScale.x * KhoangCach,
                            new Vector3(boxCollider.bounds.size.x * PhamVi, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
    void gaySatThuong()
    {
        mau.ThayDoiMau(-SatThuong);
    }
}
