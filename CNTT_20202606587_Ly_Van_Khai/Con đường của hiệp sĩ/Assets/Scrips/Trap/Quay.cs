using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quay : MonoBehaviour
{
    public Transform VatQuay; // GameObject cần quay
    public Transform TrucQuay; // Điểm trục quay
    public float TocDoQuay = 30f; // Tốc độ quay
    public bool DoiHuong = true; // Biến để theo dõi hướng quay hiện tại
    void Update()
    {
        // Xác định hướng quay
        float huong = DoiHuong ? 1f : -1f;

        // Tính toán góc quay mới
        float GocQuay = VatQuay.rotation.eulerAngles.z + (huong * TocDoQuay * Time.deltaTime);

        // Kiểm tra nếu góc quay mới nằm ngoài khoảng góc quay mong muốn, đổi hướng quay
        if (GocQuay > 70f && GocQuay < 290f)
        {
            DoiHuong = !DoiHuong;
        }

        // Quay vật quay xung quanh trục quay với góc quay mới
        VatQuay.RotateAround(TrucQuay.position, Vector3.forward, huong * TocDoQuay * Time.deltaTime);
    }
}
