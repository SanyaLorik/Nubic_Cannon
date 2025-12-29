using SanyaBeerExtension;
using UnityEngine;

public class CameraPlayerFollower : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private NubicMovement _nubicMovement;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _rotationSmoothness = 5f;

    private bool _isFollowing = false;

    private void LateUpdate()
    {
        if (_isFollowing == false)
            return;

        Vector3 nubicPosition = _nubicMovement.transform.position;
        Vector3 cameraOffsetByVelocity = _nubicMovement.Rigidbody.linearVelocity.normalized;
        cameraOffsetByVelocity = cameraOffsetByVelocity
            .SetX(cameraOffsetByVelocity.x * _offset.x)
            .SetY(cameraOffsetByVelocity.y * _offset.y);

        Vector3 cameraPosition = nubicPosition - cameraOffsetByVelocity;
        _camera.position = cameraPosition;

        // Вычисляем направление к цели
        Vector3 direction = _nubicMovement.transform.position - cameraPosition;
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
}