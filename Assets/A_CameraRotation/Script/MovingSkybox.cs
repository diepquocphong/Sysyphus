using UnityEngine;

public class MovingSkybox : MonoBehaviour
{
    public float rotationSpeed = 1.0f; // Tốc độ di chuyển của mây

    void Update()
    {
        // Lấy vật liệu Skybox hiện tại
        Material skyboxMaterial = RenderSettings.skybox;

        // Tính toán offset mới dựa trên thời gian và tốc độ
        float newRotation = Time.time * rotationSpeed;

        // Cập nhật offset cho Skybox Material
        skyboxMaterial.SetFloat("_Rotation", newRotation);
    }
}