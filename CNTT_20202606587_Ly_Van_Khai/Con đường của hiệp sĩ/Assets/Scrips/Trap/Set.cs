using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set : MonoBehaviour
{
    public Animator animator; // Reference đến Animator của lightning animation
    public int SatThuong = 10; // Số lượng máu bị trừ khi bị trúng sét
    public float ThoiGian = 2f; // Thời gian giữa mỗi lần gọi animation
    public PolygonCollider2D polygonCollider;
    public float KhoangCach;
    public float PhamVi;
    private float dem;
    private MauPlayer mau; 
    void Start()
    {
        dem = ThoiGian;
        mau = FindObjectOfType<MauPlayer>();
    }

    void Update()
    {
        // Giảm đếm thời gian
        dem -= Time.deltaTime;

        // Nếu đến thời gian gọi animation và chưa trong trạng thái tấn công
        if (dem <= 0f )
        {
            // Gọi animation danh
            animator.SetTrigger("tancong");
            // Đặt lại đếm thời gian
            dem = ThoiGian;
        }
    }

    // Phương thức để xử lý khi sét đánh
    private bool PhatHienPlayer()
    {
        //SoundManager.instance.PlaySound(SoundTancong);
        RaycastHit2D hit = Physics2D.BoxCast(polygonCollider.bounds.center + transform.right * PhamVi * transform.localScale.x * KhoangCach,
                                              new Vector3(polygonCollider.bounds.size.x * PhamVi, polygonCollider.bounds.size.y, polygonCollider.bounds.size.z),
                                              0, Vector2.left, 0, LayerMask.GetMask("Default"));

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            // Kiểm tra xem có tham chiếu đến script MauScript không trước khi gọi ChangeHealth
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
        Gizmos.DrawWireCube(polygonCollider.bounds.center + transform.right * PhamVi * transform.localScale.x * KhoangCach,
                            new Vector3(polygonCollider.bounds.size.x * PhamVi, polygonCollider.bounds.size.y, polygonCollider.bounds.size.z));
    }

    // Phương thức để xử lý khi kết thúc animation "tancong"
    public void TanCong()
    {
        PhatHienPlayer();
    }
}
