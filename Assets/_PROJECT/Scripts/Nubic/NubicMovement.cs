using Architecture_M;
using Cysharp.Threading.Tasks;
using SanyaBeerExtension;
using System;
using UnityEngine;
using Zenject;

public class NubicMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    [Header("Movement For Z")]
    [SerializeField] private float _speedZ = 10f;
    [SerializeField][Range(0f, 1f)] private float _inertia = 0.5f;
    [SerializeField] private float _smoothTime = 0.2f;

    [Header("Rebound")]
    [SerializeField] private Vector3 _reboundRatio;
    [SerializeField] private Vector3 _minReboundVelocity;

    [Header("Test Animation Curve")]
    [SerializeField] private AnimationCurve _trajectory;
    [SerializeField] private AnimationCurve _acceleration;
    [SerializeField] private float _height;
    [SerializeField] private float _duration;
    [SerializeField] private float _distance;

    [Header("TEST")]
    [SerializeField] private float _minMagnitudeForDeath;

    [Inject] private IInputDirection3 _inputDirection;
    [Inject] private GameDataNC _gameData;

    public event Action OnDead;

    private Vector3 _velocity = Vector3.zero;
    private Vector3 _smoothVelocity = Vector3.zero;

    private bool _isMoving = false;

    private Vector3 _currentMax = Vector2.zero;

    public Rigidbody Rigidbody => _rigidbody;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(Rigidbody.position, Rigidbody.position + Rigidbody.linearVelocity * 5);
    }

    private void FixedUpdate()
    {
        if (_isMoving == false)
            return;

        // Целевая скорость на основе ввода
        Vector3 targetVelocity = new Vector3(0, 0f, -_inputDirection.Direction3.x * _speedZ);

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
        _rigidbody.linearVelocity = _rigidbody.linearVelocity
            .SetZ(_velocity.z)
            .MinX(1f);

        _currentMax.y = Mathf.Max(_currentMax.y, _rigidbody.linearVelocity.y);
        _currentMax.x = Mathf.Max(_currentMax.x, _rigidbody.linearVelocity.x);

        if (_rigidbody.linearVelocity.magnitude < _minMagnitudeForDeath)
        {
            _isMoving = false;
            OnDead?.Invoke();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(_gameData.ReboundTag) == true) 
            Rebound();
    }

    private void Rebound()
    {
        //Vector3 velocity = _rigidbody.linearVelocity.SetY(_currentMax.y * _reboundRatio.y).SetX(_currentMax.x * _reboundRatio.x);
        Vector3 velocity = new()
        {
            x = _currentMax.x * _reboundRatio.x,
            y = _currentMax.y * _reboundRatio.y,
            z = _rigidbody.linearVelocity.z
        };

        print(_rigidbody.linearVelocity  + " " + velocity);

        _rigidbody.linearVelocity = velocity;
        _currentMax = velocity;
    }

    public void SetVelocity(Vector3 velocity)
    {
        _velocity = velocity;
        _rigidbody.linearVelocity = velocity;

        _isMoving = true;
        //Follow().Forget();
    }

    public void Stop()
    {
        _isMoving = false;
    }

    private async UniTaskVoid Follow()
    {
        float elapsedTime = 0;
        Vector3 source = Rigidbody.position;
        float targetDistance = source.x + _distance;

        do
        {
            float normalizedTime = elapsedTime / _duration;

            // Прогресс по X из кривой ускорения
            float xProgress = _acceleration.Evaluate(normalizedTime);
            float x = Mathf.Lerp(source.x, targetDistance, normalizedTime);

            // Высота из кривой траектории
            float heightRatio = _trajectory.Evaluate(xProgress);
            float height = _height * heightRatio;

            Vector3 newPosition = new Vector3(x, source.y + height, source.z);
            Rigidbody.MovePosition(newPosition);

            elapsedTime += Time.deltaTime;
            await UniTask.Yield(PlayerLoopTiming.FixedUpdate);
        }
        while (elapsedTime < _duration);

        print("end");
    }
}