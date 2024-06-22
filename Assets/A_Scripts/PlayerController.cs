using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject targetObject;  // Đối tượng cần bật/tắt
    public Transform playerTransform;  // Transform của người chơi
    private bool isCollidingWithBall;
    private float previousRotationY;
    private float rotationSpeedThreshold = 150f; // Ngưỡng tốc độ xoay để tắt đối tượng
    private float normalizedCurrentRotationY;

    void Start()
    {
        // Lưu góc xoay ban đầu của người chơi
        previousRotationY = playerTransform.eulerAngles.y;
    }

    void Update()
    {
      
         // Lấy góc xoay hiện tại của người chơi theo trục y
         float currentRotationY = playerTransform.eulerAngles.y;

         // Chuyển đổi góc từ 0-360 về -180 đến 180 để dễ kiểm tra
         normalizedCurrentRotationY = currentRotationY > 180 ? currentRotationY - 360 : currentRotationY;
         float normalizedPreviousRotationY = previousRotationY > 180 ? previousRotationY - 360 : previousRotationY;

         // Tính toán tốc độ xoay của người chơi
         float rotationSpeed = Mathf.Abs(normalizedCurrentRotationY - normalizedPreviousRotationY) / Time.deltaTime;

         // Cập nhật previousRotationY
         previousRotationY = currentRotationY;

         // Kiểm tra nếu tốc độ xoay vượt quá ngưỡng hoặc nếu góc nằm trong khoảng -60 đến 60 và va chạm với Ball
         if (rotationSpeed > rotationSpeedThreshold || !isCollidingWithBall || !(normalizedCurrentRotationY >= -60 && normalizedCurrentRotationY <= 60))
         {
             targetObject.SetActive(false);  // Tắt GameObject
         }
         else if (isCollidingWithBall && normalizedCurrentRotationY >= -55 && normalizedCurrentRotationY <= 55)
         {
             targetObject.SetActive(true);  // Bật GameObject
         }
        
    }

    void OnTriggerStay(Collider other)
    {
        // Kiểm tra nếu đối tượng va chạm có tag là "Ball"
        if (other.CompareTag("Ball") && (normalizedCurrentRotationY >= -60 && normalizedCurrentRotationY <= 60))
        {
            isCollidingWithBall = true;
        }
        else
        {
            isCollidingWithBall = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Kiểm tra nếu đối tượng rời khỏi va chạm có tag là "Ball"
        if (other.CompareTag("Ball"))
        {
            isCollidingWithBall = false;
        }
    }
}
