using Cysharp.Threading.Tasks;
using DG.Tweening;
using SanyaBeerExtension;
using UnityEngine;

public class PowerSpeedometer: MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private Transform _to;
    [SerializeField] private ParametrBase<Transform> _arrow;

    private Tween _tween;

    public void Show()
    {
        _container.ActiveSelf();
    }

    public async UniTaskVoid StartArrow()
    {
        Vector3 source = _arrow.Source.transform.position;
        Vector3 target = _to.position.SetY(_arrow.Source.transform.position.y);

        while (destroyCancellationToken.IsCancellationRequested == false)
        {
            _tween = _arrow.Source.DOMove(target, _arrow.Duration)
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
    }
}