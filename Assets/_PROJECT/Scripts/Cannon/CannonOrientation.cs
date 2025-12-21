using Architecture_M;
using SanyaBeerExtension;
using UnityEngine;
using Zenject;

public class CannonOrientation : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private Transform _objectY;
    [SerializeField] private Transform _objectZ;

    [Header("Speed")]
    [SerializeField] private float _speedY = 10f;
    [SerializeField] private float _speedZ = 10f;

    [Header("Smooth Rotation")]
    [SerializeField] private bool _useSmoothRotation = true;
    [SerializeField] private float _smoothTime = 0.1f;

    [Header("Limits")]
    [SerializeField] private bool _useLimits = true;
    [SerializeField] private float _minAngleY = -45f;
    [SerializeField] private float _maxAngleY = 45f;
    [SerializeField] private float _minAngleZ = -30f;
    [SerializeField] private float _maxAngleZ = 30f;

    [Inject] private IInputDirection3 _input;

    private float _currentRotationY = 0f;
    private float _currentRotationZ = 0f;
    private float _targetRotationY = 0f;
    private float _targetRotationZ = 0f;
    private float _velocityY = 0f;
    private float _velocityZ = 0f;

    private void Start()
    {
        if (_objectY != null)
        {
            _currentRotationY = NormalizeAngle(_objectY.localEulerAngles.y);
            _targetRotationY = _currentRotationY;
        }

        if (_objectZ != null)
        {
            _currentRotationZ = NormalizeAngle(_objectZ.localEulerAngles.z);
            _targetRotationZ = _currentRotationZ;
        }
    }

    private void Update()
    {
        if (_input == null)
            return;

        Vector3 inputDirection = _input.Direction3;

        // Обновляем целевые углы
        _targetRotationY += inputDirection.x * _speedY * Time.deltaTime;
        _targetRotationZ += inputDirection.z * _speedZ * Time.deltaTime;

        // Применяем ограничения
        if (_useLimits)
        {
            _targetRotationY = Mathf.Clamp(_targetRotationY, _minAngleY, _maxAngleY);
            _targetRotationZ = Mathf.Clamp(_targetRotationZ, _minAngleZ, _maxAngleZ);
        }

        // Применяем плавное вращение или мгновенное
        if (_useSmoothRotation)
        {
            _currentRotationY = Mathf.SmoothDamp(_currentRotationY, _targetRotationY,
                ref _velocityY, _smoothTime);
            _currentRotationZ = Mathf.SmoothDamp(_currentRotationZ, _targetRotationZ,
                ref _velocityZ, _smoothTime);
        }
        else
        {
            _currentRotationY = _targetRotationY;
            _currentRotationZ = _targetRotationZ;
        }

        // Применяем вращение к объектам
        if (_objectY != null)
        {
            _objectY.localRotation = Quaternion.Euler(0f, _currentRotationY, 0f);
        }

        if (_objectZ != null)
        {
            _objectZ.localRotation = Quaternion.Euler(0f, 0f, _currentRotationZ);
        }
    }

    private float NormalizeAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            angle -= 360;
        return angle;
    }
}

public class CannonShooting : MonoBehaviour
{
    [SerializeField] private SpawnpointSpawner<Nubic> _spawner;

    public Nubic Shoot()
    {
        return default;
    }
}
