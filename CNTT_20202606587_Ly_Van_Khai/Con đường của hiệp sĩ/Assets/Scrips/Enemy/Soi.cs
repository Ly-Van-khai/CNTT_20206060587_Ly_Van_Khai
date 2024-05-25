using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Soi : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;
    private float dem = Mathf.Infinity;
    [SerializeField] private float HoiChieu;
    [SerializeField] private float KhoangCach;
    [SerializeField] private PolygonCollider2D boxCollider;
    [SerializeField] private float PhamVi;
    private BossMove bossDC;
    private MauPlayer mau;
    public int SatThuong;
    private MauEnemy mauboss;
    public GameObject canvas;
    private UIMauBoss uiMauBossInstance;
    public int MauBD;
    public int Mau { get { return MauHT; } }
    public int MauHT;

    public Image mask;
    float originalSize;

    private void Awake()
    {
        originalSize = mask.rectTransform.rect.width;
        MauHT = MauBD;
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        mau = FindAnyObjectByType<MauPlayer>();
        bossDC = GetComponentInParent<BossMove>();
        mauboss = FindAnyObjectByType<MauEnemy>();
        boxCollider = GetComponent<PolygonCollider2D>();

    }

    private void Update()
    {
        dem += Time.deltaTime;
        if (PhatHienPlayer())
        {
            if (dem >= HoiChieu)
            {
                anim.SetTrigger("tancong");
                dem = 0; // Đặt lại cooldownTimer chỉ khi cuộc tấn công được kích hoạt
            }
        }
        if (bossDC != null)
            bossDC.enabled = !PhatHienPlayer();
        if (MauHT<= 0)
        {
            //Destroy(gameObject);
            bossDC.enabled = false;
            boxCollider.enabled = false;
            canvas.SetActive(false);
        }
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
    public void TruMau(int amount)
    {
        MauHT = Mathf.Clamp(MauHT + amount, 0, MauBD);
        SetValue(MauHT / (float)MauBD);
        anim.SetTrigger("dau");
        if (MauHT <= 0)
        {
            anim.SetTrigger("chet");
        }
    }
    public void SetValue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}
