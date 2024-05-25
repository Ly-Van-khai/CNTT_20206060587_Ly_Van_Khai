using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BayRoi : MonoBehaviour
{
    public int SatThuong = 10; // Số lượng máu bị trừ khi bị trúng sét
    public PolygonCollider2D boxCollider;
    public float ChieuNgang;
    public Vector2 DiChuyen = Vector2.down; // Hướng raycast mặc định là từ trên xuống
    public float verticalDistance = 1f; // Khoảng cách di chuyển lên hoặc xuống
    Rigidbody2D rigidbody2d;
    public float ChieuDoc = 1f; // Tham số để điều chỉnh kích thước của hit theo chiều dọc
    [SerializeField] private AudioClip soundClip;
    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        PhatHien();
    }
    private void PhatHien()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.up * ChieuNgang * transform.localScale.y * verticalDistance,
                                              new Vector3(boxCollider.bounds.size.x * ChieuNgang, boxCollider.bounds.size.y * ChieuDoc, boxCollider.bounds.size.z),
                                              0, DiChuyen, 0, LayerMask.GetMask("Default"));

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            rigidbody2d.gravityScale = 5;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            SoundManager.instance.PhatNhac(soundClip);
        MauPlayer mau = collision.gameObject.GetComponent<MauPlayer>();
        if (mau != null)
        {
            mau.ThayDoiMau(-SatThuong);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.up * ChieuNgang * transform.localScale.y * verticalDistance,
                            new Vector3(boxCollider.bounds.size.x * ChieuNgang, boxCollider.bounds.size.y * ChieuDoc, boxCollider.bounds.size.z));
    }
}
