using System.Collections;
using UnityEngine;

public class DatRoi : MonoBehaviour
{
    public float ThoiGianCho; // Thời gian trì hoãn trước khi vật thể bắt đầu rơi xuống
    public float ThoiGianRoi; // Thời gian mất để vật thể rơi xuống
    public float TocDo;
    [SerializeField] private AudioClip audioClip;
    private Vector3 ViTriBanDau; // Vị trí ban đầu của vật thể

    void Start()
    {
        // Lưu vị trí ban đầu của vật thể
        ViTriBanDau = transform.position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            StartCoroutine(Roi()); // Bắt đầu Coroutine để kiểm tra thời gian đứng trên
        }
    }
    IEnumerator Roi()
    {
        yield return new WaitForSeconds(ThoiGianCho);
        SoundManager.instance.PhatNhac(audioClip);
        float dem = 0f;
        while (dem < ThoiGianRoi)
        {
            
            // Di chuyển vật thể xuống
            transform.Translate(Vector3.down * Time.deltaTime * TocDo);
            dem += Time.deltaTime;
            yield return null;
        }
        
        // Đặt lại vị trí ban đầu của vật thể
        transform.position = ViTriBanDau;
    }
}
