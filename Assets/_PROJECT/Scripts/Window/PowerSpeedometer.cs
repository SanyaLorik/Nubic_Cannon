using Cysharp.Threading.Tasks;
using DG.Tweening;
using SanyaBeerExtension;
using UnityEngine;

public class PowerSpeedometer: MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private Transform _from;
    [SerializeField] private Transform _to;
    [SerializeField] private AnimationCurve _falloffCurve;
    [SerializeField] private ParametrBase<Transform> _arrow;

    private Tween _tween;

    public float Multiplayer { get; private set; } = 0;

    public void Show()
    {
        _container.ActiveSelf();
    }

    public void Hide()
    {
        _container.DisactiveSelf();
    }

    public async UniTaskVoid StartArrow()
    {
        Vector3 source = _arrow.Source.transform.position;
        Vector3 target = _to.position.SetY(_arrow.Source.transform.position.y);

        while (destroyCancellationToken.IsCancellationRequested == false)
        {
            _tween = _arrow.Source
                .DOMove(target, _arrow.Duration)
                .SetEase(_arrow.Ease);

            await UniTask.WhenAny(
                _tween.AsyncWaitForCompletion().AsUniTask(),
                _tween.AsyncWaitForKill().AsUniTask());

            _tween = _arrow.Source.DOMove(source, _arrow.Duration)
                .SetEase(_arrow.Ease);

            await UniTask.WhenAny(
                _tween.AsyncWaitForCompletion().AsUniTask(),
                _tween.AsyncWaitForKill().AsUniTask());
        }
    }

    public void StopArrow()
    {
        _tween?.Kill();

        CaclulateMultiplayer();
    }

    private void CaclulateMultiplayer()
    {
        float distance = (_arrow.Source.position.x - _to.position.x) / (_from.position.x - _to.position.x);
        Multiplayer = _falloffCurve.Evaluate(distance);
    }
}