using SanyaBeerExtension;
using UnityEngine;

public class CameraPlayerFollower : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private NubicMovement _nubicMovement;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector3 _minDistance; 
    [SerializeField] private float _positionSmoothness = 5f; 
    [SerializeField] private float _magnitudeSensivity = 0.75f;

    private bool _isFollowing = false;

    private Vector3 _currentCameraPosition;
    private Vector3 _initialDirection;

    private void LateUpdate()
    {
        if (_isFollowing == false)
            return;
        /*
        float magnitude = _nubicMovement.Rigidbody.linearVelocity.x * _magnitudeSensivity;
        Vector3 target = _nubicMovement.transform.position - _initialDirection * magnitude;

        target = ApplyMinDistance(target, _nubicMovement.transform.position);

        // Плавное движение камеры
        _currentCameraPosition = Vector3.Slerp(
            _currentCameraPosition,
            target,
            _positionSmoothness * Time.deltaTime
        );

        _camera.position = target;
        */
        _camera.position = _nubicMovement.transform.position + _offset;
    }

    private Vector3 ApplyMinDistance(Vector3 cameraPosition, Vector3 playerPosition)
    {
        Vector3 toCamera = playerPosition - cameraPosition;


        if (toCamera.x < _minDistance.x)
        {
            // Нормализуем направление и устанавливаем минимальную дистанцию
            cameraPosition = cameraPosition.SetX(playerPosition.x - _minDistance.x);
        }

        if (toCamera.y < _minDistance.y)
        {
            // Нормализуем направление и устанавливаем минимальную дистанцию
            cameraPosition = cameraPosition.SetY(playerPosition.y + _minDistance.y);
        }

        return cameraPosition;
    }


    public void StartFollow()
    {
        _isFollowing = true;

        _currentCameraPosition = _camera.position;

        _initialDirection = _nubicMovement.transform.position - _camera.position;
        _initialDirection.Normalize();
    }

    public void StopFollow()
    {
        _isFollowing = false;
    }
}