using Architecture_M;
using SanyaBeerExtension;
using System;
using UnityEngine;
using Zenject;

public class NubicMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    [Header("Z Movement Settings")]
    [SerializeField] private float _speedZ = 10f;
    [SerializeField][Range(0f, 1f)] private float _inertia = 0.5f;
    [SerializeField] private float _smoothTime = 0.2f;

    [Header("Rebound Settings")]
    [SerializeField] private Vector3 _reboundRatio;
    [SerializeField] private Vector3 _minReboundVelocity;

    [Inject] private IInputDirection3 _inputDirection;
    [Inject] private GameDataNC _gameData;

    public event Action OnDead;

    private Vector3 _velocity = Vector3.zero;
    private Vector3 _smoothVelocity = Vector3.zero;
    private bool _isMoving = false;
    private Vector3 _currentMaxVelocity = Vector2.zero;

    private void OnDrawGizmos()
    {
        DrawVelocityGizmo();
    }

    private void FixedUpdate()
    {
        if (_isMoving == false)
            return;

        HandleMovement();
        TrackMaximumVelocity();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(_gameData.ReboundTag) == true)
            HandleRebound();
    }

    public void SetVelocity(Vector3 velocity)
    {
        _velocity = velocity;
        _rigidbody.linearVelocity = velocity;

        _isMoving = true;
    }

    public void Stop()
    {
        _isMoving = false;
    }

    private void DrawVelocityGizmo()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(_rigidbody.position,
            _rigidbody.position + _rigidbody.linearVelocity * 5);
    }

    private void HandleMovement()
    {
        Vector3 targetVelocity = CalculateTargetVelocity();
        ApplyInertiaToVelocity(targetVelocity);
        ApplyVelocityToRigidbody();
    }

    private Vector3 CalculateTargetVelocity()
    {
        return new Vector3(0, 0f, -_inputDirection.Direction3.x * _speedZ);
    }

    private void ApplyInertiaToVelocity(Vector3 targetVelocity)
    {
        if (_inertia < 1f)
        {
            ApplySmoothInertia(targetVelocity);
        }
        else
        {
            ApplyFullInertia(targetVelocity);
        }
    }

    private void ApplySmoothInertia(Vector3 targetVelocity)
    {
        _velocity = Vector3.SmoothDamp(_velocity, targetVelocity,
            ref _smoothVelocity, _smoothTime * (1f - _inertia));
    }

    private void ApplyFullInertia(Vector3 targetVelocity)
    {
        _velocity = targetVelocity.magnitude > 0.1f ? targetVelocity : _velocity;
    }

    private void ApplyVelocityToRigidbody()
    {
        _rigidbody.linearVelocity = _rigidbody.linearVelocity
            .SetZ(_velocity.z)
            .MinX(1f);
    }

    private void TrackMaximumVelocity()
    {
        _currentMaxVelocity.y = Mathf.Max(_currentMaxVelocity.y, _rigidbody.linearVelocity.y);
        _currentMaxVelocity.x = Mathf.Max(_currentMaxVelocity.x, _rigidbody.linearVelocity.x);
    }

    private void HandleRebound()
    {
        if (_isMoving == false)
            return;

        Vector3 reboundVelocity = CalculateReboundVelocity();

        Debug.Log($"Current velocity: {_rigidbody.linearVelocity}, Rebound velocity: {reboundVelocity}");

        if (reboundVelocity.ResetZ().magnitude < _minReboundVelocity.ResetZ().magnitude)
        {
            StopMovementWithDeath();
            return;
        }

        ApplyReboundVelocity(reboundVelocity);
    }

    private Vector3 CalculateReboundVelocity()
    {
        return new Vector3
        {
            x = _currentMaxVelocity.x * _reboundRatio.x,
            y = _currentMaxVelocity.y * _reboundRatio.y,
            z = _rigidbody.linearVelocity.z
        };
    }

    private void StopMovementWithDeath()
    {
        _rigidbody.linearVelocity = Vector3.zero;
        _isMoving = false;
        OnDead?.Invoke();
    }

    private void ApplyReboundVelocity(Vector3 reboundVelocity)
    {
        _rigidbody.linearVelocity = reboundVelocity;
        _currentMaxVelocity = reboundVelocity;
    }
}