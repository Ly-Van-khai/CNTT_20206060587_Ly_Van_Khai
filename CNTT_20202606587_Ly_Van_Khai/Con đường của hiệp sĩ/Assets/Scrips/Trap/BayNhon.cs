using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BayNhon : MonoBehaviour
{
    public Animator animator; 
    public int SatThuong; // Số lượng máu bị trừ khi bị trúng sét
    public float ThoiGianGoi; // Thời gian giữa mỗi lần gọi animation
    public BoxCollider2D boxCollider;
    public float KhoangCach;
    public float PhamVi;
    private float Dem;
    private MauPlayer mau; // Thêm tham chiếu đến script MauScript
    [Header("Am Thanh")]
    public Transform player; 
    public float KhoangCachNghe = 5f;
    private bool KiemTra = false;
    public AudioClip AmThanh;
    void Start()
    {
        Dem = ThoiGianGoi;
        animator = GetComponent<Animator>();
        // Tìm và gán tham chiếu đến script MauScript
        mau = FindObjectOfType<MauPlayer>();
    }

    void Update()
    {
        float KhoangCach = Vector3.Distance(transform.position, player.position);

        if (KhoangCach <= KhoangCachNghe && !KiemTra)
        {
            KiemTra = true;
        }
        else if (KhoangCach > KhoangCachNghe && KiemTra)
        {
            KiemTra = false;
        }
        // Giảm đếm thời gian
        Dem -= Time.deltaTime;
        // Nếu đến thời gian gọi animation và chưa trong trạng thái tấn công
        if (Dem <= 0f)
        {
            // Gọi animation
            animator.SetTrigger("tancong");
            // Đặt lại đếm thời gian
            Dem = ThoiGianGoi;
        }
    }

    // Phương thức để xử lý khi sét đánh
    private bool PhatHien()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * PhamVi * transform.localScale.x * KhoangCach,
                                              new Vector3(boxCollider.bounds.size.x * PhamVi, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
                                              0, Vector2.left, 0, LayerMask.GetMask("Default"));

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            if (mau != null)
            {
                mau.ThayDoiMau(-SatThuong);
            }
            return true;
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * PhamVi * transform.localScale.x * KhoangCach,
                            new Vector3(boxCollider.bounds.size.x * PhamVi, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
    public void TanCong()
    {
        PhatHien();
        if (KiemTra)
        {
            SoundManager.instance.PhatNhac(AmThanh);
        }
    }
}
