using UnityEngine;

public class HeadLookAtCursor : MonoBehaviour
{
    [SerializeField] private Camera cam;           // Камера меню
    [SerializeField] private Transform lookTarget; // Таргет для Multi-Aim Constraint
    [SerializeField] private float distance = 5f;  // Дистанция "вперёд" от камеры
    [SerializeField] private float smoothTime = 0.1f; // Скорость сглаживания (меньше = быстрее)

    private Vector3 velocity = Vector3.zero; // Для SmoothDamp

    private void Update()
    {
        // Получаем позицию курсора в мировых координатах
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPos = ray.origin + ray.direction * distance;

        // Плавно двигаем lookTarget к этой позиции
        lookTarget.position = Vector3.SmoothDamp(
            lookTarget.position, 
            targetPos, 
            ref velocity, 
            smoothTime
        );
    }
}