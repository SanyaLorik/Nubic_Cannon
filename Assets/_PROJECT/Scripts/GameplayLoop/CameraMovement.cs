using DG.Tweening;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _camera;

    [Header("Position")]
    [SerializeField] private Transform _inplayPoint;
    [SerializeField] private Transform _menuPoint;

    [Header("Speed")]
    [SerializeField] private float _inplayDuration;
    [SerializeField] private float _menuDuration;

    [Header("Ease")]
    [SerializeField] private Ease _inplayEase;
    [SerializeField] private Ease _menuEase;

    private Tween _tweenPosition;
    private Tween _tweenRotation;

    public void DirectToMenu()
    {
        _tweenPosition?.Kill();
        _tweenRotation?.Kill();

        _tweenPosition = _camera
            .DOMove(_menuPoint.position, _menuDuration)
            .SetEase(_menuEase);

        _tweenRotation = _camera
            .DORotate(_menuPoint.eulerAngles, _menuDuration)
            .SetEase(_menuEase);
    }

    public void DirectToInplay()
    {
        _tweenPosition?.Kill();
        _tweenRotation?.Kill();

        _tweenPosition = _camera
            .DOMove(_inplayPoint.position, _inplayDuration)
            .SetEase(_inplayEase);

        _tweenRotation = _camera
            .DORotate(_inplayPoint.eulerAngles, _inplayDuration)
            .SetEase(_inplayEase);
    }
}