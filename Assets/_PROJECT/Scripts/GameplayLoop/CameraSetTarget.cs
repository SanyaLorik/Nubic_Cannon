using DG.Tweening;
using SanyaBeerExtension;
using UnityEngine;

public class CameraSetTarget : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [Header("Inplay")]
    [SerializeField] private Transform _inplayPoint;
    [SerializeField] private Ease _inplayEase;
    [SerializeField] private float _inplayDuration;
    [SerializeField] private float _fovInplay;

    [Header("Menu")]
    [SerializeField] private Transform _menuPoint;
    [SerializeField] private Ease _menuEase;
    [SerializeField] private float _menuDuration;
    [SerializeField] private float _fovMenu;

    [Header("Table")]
    [SerializeField] private Ease _tableEase;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _tableDuration;

    [Header("Other Settings")]
    [SerializeField] private CameraPlayerFollower _cameraPlayerFollower;

    private Transform _cameraTransform;

    private Tween _tweenPosition;
    private Tween _tweenRotation;
    private Tween _tweenFov;

    private void Awake()
    {
        _cameraTransform = _camera.transform;
    }

    public void DirectToMenu()
    {
        _tweenPosition?.Kill();
        _tweenRotation?.Kill();
        _tweenFov?.Kill();

        _tweenPosition = _cameraTransform
            .DOMove(_menuPoint.position, _menuDuration)
            .SetEase(_menuEase);

        _tweenRotation = _cameraTransform
            .DORotate(_menuPoint.eulerAngles, _menuDuration)
            .SetEase(_menuEase);

        _tweenFov = _camera
            .DOFieldOfView(_fovMenu, _menuDuration)
            .SetEase(_menuEase);
    }

    public void DirectToInplay()
    {
        _tweenPosition?.Kill();
        _tweenRotation?.Kill();
        _tweenFov?.Kill();

        _tweenPosition = _cameraTransform
            .DOMove(_inplayPoint.position, _inplayDuration)
            .SetEase(_inplayEase);

        _tweenRotation = _cameraTransform
            .DORotate(_inplayPoint.eulerAngles, _inplayDuration)
            .SetEase(_inplayEase);

        _tweenFov = _camera
            .DOFieldOfView(_fovInplay, _inplayDuration)
            .SetEase(_inplayEase);
    }

    public void DirectToTableScore(Vector3 tablePosition)
    {
        _cameraPlayerFollower.StopFollow();

        _tweenPosition?.Kill();
        _tweenRotation?.Kill();

        Vector3 cameraPosition = tablePosition + _offset;

        Vector3 direction = cameraPosition - _cameraTransform.position;
        direction = direction.ResetZ();

        _tweenPosition = _cameraTransform
            .DOMove(cameraPosition, _tableDuration)
            .SetEase(_tableEase);

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            _tweenRotation = _cameraTransform
                .DORotate(targetRotation.eulerAngles, _tableDuration)
                .SetEase(_tableEase);
        }
    }

    public void DirectToNubicFly()
    {
        _cameraPlayerFollower.StartFollow();
    }
}
