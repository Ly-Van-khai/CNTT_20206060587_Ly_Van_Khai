using UnityEngine;

public class DatDc : MonoBehaviour
{
    public float TocDo = 2f;
    public float KhoangCachDiChuyenX = 5f; // Khoảng cách di chuyển theo trục X
    public float KhoangCachDiChuyenY = 2f; // Khoảng cách di chuyển theo trục Y
    public Rigidbody2D playerRigidbody;

    private Vector3 ViTriBanDau;
    [Header("Di Chuyen Ngang")]
    public bool DcNgang = false;
    public bool DcPhai = true; // Biến điều khiển di chuyển trái phải
    
    [Header("Di Chuyen Doc")]
    public bool DcDoc = true;
    public bool DcLen = true;   // Biến điều khiển di chuyển lên xuống
    
    
    void Start()
    {
        ViTriBanDau = transform.position;
    }

    void Update()
    {
        // Di chuyển platform trái phải hoặc lên xuống tùy thuộc vào giá trị của DcPhai và DcLen
        if(DcNgang)
        {
            if (DcPhai)
            {
                transform.Translate(Vector3.right * TocDo * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.left * TocDo * Time.deltaTime);
            }
        }

        if(DcDoc)
        {
            if (DcLen)
            {
                transform.Translate(Vector3.up * TocDo * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.down * TocDo * Time.deltaTime);
            }
        }

        // Đảo hướng di chuyển nếu đi qua khoảng cách di chuyển
        if (Mathf.Abs(transform.position.x - ViTriBanDau.x) >= KhoangCachDiChuyenX)
        {
            DcPhai = !DcPhai;
        }

        if (Mathf.Abs(transform.position.y - ViTriBanDau.y) >= KhoangCachDiChuyenY)
        {
            DcLen = !DcLen;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        // Kiểm tra va chạm với người chơi và di chuyển người chơi cùng với platform
        if (collision.collider.CompareTag("Player"))
        {
            float TrucX = TocDo * Time.deltaTime; // Tính toán di chuyển theo trục X
            float TrucY = TocDo * Time.deltaTime; // Tính toán di chuyển theo trục Y

            // Xác định hướng di chuyển theo trục X
            if (DcPhai)
            {
                TrucX = Mathf.Abs(TrucX); // Di chuyển sang phải
            }
            else
            {
                TrucX = -Mathf.Abs(TrucX); // Di chuyển sang trái
            }

            // Xác định hướng di chuyển theo trục Y
            if (DcLen)
            {
                TrucY = Mathf.Abs(TrucY); // Di chuyển lên
            }
            else
            {
                TrucY = -Mathf.Abs(TrucY); // Di chuyển xuống
            }

            Vector3 DiChuyen = new Vector3(TrucX, TrucY, 0f);
            playerRigidbody.transform.position += DiChuyen;
        }
    }
}
