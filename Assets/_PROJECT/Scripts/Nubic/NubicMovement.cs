using Architecture_M;
using SanyaBeerExtension;
using UnityEngine;
using Zenject;

public class NubicMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed = 10f;
    [SerializeField][Range(0f, 1f)] private float _inertia = 0.5f; // 0 - нет инерции, 1 - полная инерция
    [SerializeField] private float _smoothTime = 0.2f; // Время сглаживания

    [Inject] private IInputDirection3 _inputDirection;

    private Vector3 _velocity = Vector3.zero;
    private Vector3 _smoothVelocity = Vector3.zero;

    private bool _isMoving = false;

    public Rigidbody Rigidbody => _rigidbody;

    private void FixedUpdate()
    {
        if (_isMoving == false)
            return;

        if (_inputDirection == null)
            return;

        // Целевая скорость на основе ввода
        Vector3 targetVelocity = new Vector3(0f, 0f, -_inputDirection.Direction3.x * _speed);

        // Применяем инерцию: смешиваем текущую и целевую скорость
        if (_inertia < 1f)
        {
            // Сглаживаем скорость с учетом инерции
            float smoothFactor = Mathf.Lerp(1f, 0.1f, _inertia);
            _velocity = Vector3.SmoothDamp(_velocity, targetVelocity, ref _smoothVelocity,
                _smoothTime * (1f - _inertia));
        }
        else
        {
            // Полная инерция - скорость не меняется
            _velocity = targetVelocity.magnitude > 0.1f ? targetVelocity : _velocity;
        }

        // Применяем скорость к Rigidbody
        _rigidbody.linearVelocity = _rigidbody.linearVelocity.SetZ(_velocity.z);
    }

    public void SetVelocity(Vector3 velocity)
    {
        _velocity = velocity;
        _rigidbody.linearVelocity = velocity;

        _isMoving = true;
    }

    public void SetInertia(float inertia)
    {
        _inertia = Mathf.Clamp01(inertia);
    }

    public float GetInertia() => _inertia;
}