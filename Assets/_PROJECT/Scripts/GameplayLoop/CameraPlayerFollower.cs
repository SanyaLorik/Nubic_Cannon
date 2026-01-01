using SanyaBeerExtension;
using UnityEngine;

public class CameraPlayerFollower : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private NubicMovement _nubicMovement;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _minDistance = 5f; // Минимальная дистанция от камеры к игроку
    [SerializeField] private float _rotationSmoothness = 5f;
    [SerializeField] private float _positionSmoothness = 5f; // Плавность движения камеры

    private bool _isFollowing = false;
    private Vector3 _currentCameraPosition;

    private void LateUpdate()
    {
        if (_isFollowing == false)
            return;

        Vector3 nubicPosition = _nubicMovement.transform.position;

        // Вычисляем смещение на основе скорости
        Vector3 cameraOffsetByVelocity = _nubicMovement.Rigidbody.linearVelocity.normalized;
        cameraOffsetByVelocity = cameraOffsetByVelocity
            .SetX(cameraOffsetByVelocity.x * _offset.x)
            .SetY(cameraOffsetByVelocity.y * _offset.y);

        // Целевая позиция камеры
        Vector3 targetPosition = nubicPosition - cameraOffsetByVelocity;

        // Обеспечиваем минимальную дистанцию
        targetPosition = ApplyMinDistance(targetPosition, nubicPosition);

        // Плавное движение камеры
        _currentCameraPosition = Vector3.Slerp(
            _currentCameraPosition,
            targetPosition,
            _positionSmoothness * Time.deltaTime
        );

        _camera.position = _currentCameraPosition;

        // Вычисляем направление к цели
        Vector3 direction = nubicPosition - _currentCameraPosition;

        direction = direction.ResetZ();

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            _camera.rotation = Quaternion.Slerp(
                _camera.rotation,
                targetRotation,
                _rotationSmoothness * Time.deltaTime
            );
        }
    }

    public void StartFollow()
    {
        _isFollowing = true;
    }

    private Vector3 ApplyMinDistance(Vector3 cameraPosition, Vector3 playerPosition)
    {
        // Вычисляем вектор от игрока к камере
        Vector3 toCamera = cameraPosition - playerPosition;

        // Проверяем дистанцию
        float currentDistance = toCamera.magnitude;

        if (currentDistance < _minDistance)
        {
            // Нормализуем направление и устанавливаем минимальную дистанцию
            return cameraPosition.SetX(playerPosition.x - _minDistance);
        }

        return cameraPosition;
    }

    // Метод для визуализации минимальной дистанции в редакторе
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_nubicMovement.transform.position, _minDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(_nubicMovement.transform.position, _camera.position);
    }
}