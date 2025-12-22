using Architecture_M;
using SanyaBeerExtension;
using UnityEngine;
using Zenject;

public class NubicMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed;

    [Inject] private IInputDirection3 _inputDirection;

    private bool _isMoving = false;

    private void FixedUpdate()
    {
        if (_isMoving == false)
            return;

        _rigidbody.linearVelocity = _rigidbody.linearVelocity.SetZ(-_inputDirection.Direction3.x * _speed);
    }

    public void SetVelocity(Vector3 velocity)
    {
        _rigidbody.linearVelocity = velocity;
        _isMoving = true;
    }
}
