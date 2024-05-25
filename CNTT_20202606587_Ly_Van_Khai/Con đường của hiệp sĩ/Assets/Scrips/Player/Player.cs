using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float TocDo;
    public float LucNhay;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    private float Dem = Mathf.Infinity;
    public float ThoiGianHoiChieu;
    public float DieuChinh;
    public CapsuleCollider2D boxCollider;
    public float PhamVi;
    public AudioClip SoundTancong;
    public AudioClip SoundNhay;
    public int SatThuong;
    private NPC npc;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        npc = FindObjectOfType<NPC>();
    }

    private void Update()
    {
        float chay = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * TocDo, body.velocity.y);
        // quay nhân vật sang trái, phải
        if (chay > 0.01f)
            transform.localScale = new Vector2(1, 1);
        else if (chay < -0.01f)
            transform.localScale = new Vector2(-1, 1);

        if (Input.GetKey(KeyCode.Space) && grounded)
            nhay();
        if (Input.GetMouseButtonDown(0) && Dem > ThoiGianHoiChieu)
        {
            PhatHienKeDich();
            Dem = 0;
        }
        Dem += Time.deltaTime;

        // Gọi animator chạy, nhảy và kiểm tra chạm đất
        anim.SetBool("chay", chay != 0);
        anim.SetTrigger("nhay");
        anim.SetBool("chamdat", grounded);
    }

    private void nhay()
    {
        SoundManager.instance.PhatNhac(SoundNhay);
        body.velocity = new Vector2(body.velocity.x, LucNhay);
        grounded = false;
    }
    
    // Kiểm tra chạm đất
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }

    private bool PhatHienKeDich()
    {
        anim.SetTrigger("tancong");
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * PhamVi * transform.localScale.x * DieuChinh,
                                              new Vector2(boxCollider.bounds.size.x * PhamVi, boxCollider.bounds.size.y),
                                              0, transform.right, 0, LayerMask.GetMask("Default"));
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Soi e = hit.collider.GetComponentInChildren<Soi>();
                SoundManager.instance.PhatNhac(SoundTancong);
                if (e != null)
                {
                    e.TruMau(-SatThuong);
                    return true;
                }
            } else if(hit.collider.CompareTag("Boss"))
            {
                SoundManager.instance.PhatNhac(SoundTancong);
                MauEnemy mau = hit.collider.GetComponent<MauEnemy>();
                if (mau != null)
                {
                    mau.TruMau(-SatThuong);
                    return true;
                }
            }
            else if (hit.collider.CompareTag("NPC"))
            {
                npc.HienThi();
                return true;
            }
        }
        ThoiGianHoiChieu = 0;
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * PhamVi * transform.localScale.x * DieuChinh,
                            new Vector3(boxCollider.bounds.size.x * PhamVi, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
