using Cysharp.Threading.Tasks;
using DG.Tweening;
using SanyaBeerExtension;
using System.Threading;
using UnityEngine;
using Zenject;

public class PowerSpeedometer: MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private Transform _from;
    [SerializeField] private Transform _to;
    [SerializeField] private ParametrBase<Transform> _arrow;

    [Inject] private GameDataNC _gameDataNC;

    private CancellationTokenSource _tokenSource;
    private Tween _tween;

    private void OnDestroy()
    {
        _tokenSource?.Dispose();
    }

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
        _tokenSource = new CancellationTokenSource();

        Vector3 source = _arrow.Source.transform.position;
        Vector3 target = _to.position.SetY(_arrow.Source.transform.position.y);

        while (_tokenSource.IsCancellationRequested == false)
        {
            _tween = _arrow.Source
                .DOMove(target, _arrow.Duration)
                .SetEase(_arrow.Ease);

            await UniTask.WhenAny(
                _tween.AsyncWaitForCompletion().AsUniTask(),
                _tween.AsyncWaitForKill().AsUniTask())
                .AttachExternalCancellation(_tokenSource.Token);

            _tween = _arrow.Source
                .DOMove(source, _arrow.Duration)
                .SetEase(_arrow.Ease);

            await UniTask.WhenAny(
                _tween.AsyncWaitForCompletion().AsUniTask(),
                _tween.AsyncWaitForKill().AsUniTask())
                .AttachExternalCancellation(_tokenSource.Token);
        }
    }

    public void StopArrow()
    {
        _tween?.Kill();

        _tokenSource?.Cancel();
        _tokenSource?.Dispose();

        CaclulateMultiplayer();
    }

    private void CaclulateMultiplayer()
    {
        float distance = (_arrow.Source.position.x - _to.position.x) / (_from.position.x - _to.position.x);
        Multiplayer = _gameDataNC.FalloffCurve.Evaluate(distance);
    }
}