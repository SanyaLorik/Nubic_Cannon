using Cysharp.Threading.Tasks;
using DG.Tweening;
using SanyaBeerExtension;
using UnityEngine;

public class Window : MonoBehaviour
{
    [SerializeField] protected GameObject container;

    [Header("Fade")]
    [SerializeField] protected CanvasGroup canvasGroup;
    [SerializeField] protected float fadeDuration;
    [SerializeField] protected float unfadeDuration;

    private Tween _tween;

    public virtual void Show()
    {
        _tween?.Kill();

        container.ActiveSelf();
    }

    public virtual async UniTask ShowWithUnfade()
    {
        _tween?.Kill();

        canvasGroup.alpha = 0;
        Show();

        _tween = canvasGroup
            .DOFade(1, unfadeDuration);

        await UniTask.WhenAny(
            _tween.AsyncWaitForCompletion().AsUniTask(), 
            _tween.AsyncWaitForKill().AsUniTask());
    }

    public virtual void Hide()
    {
        _tween?.Kill();

        container.DisactiveSelf();
    }

    public virtual async UniTask HideWithFade()
    {
        _tween?.Kill();

        _tween = canvasGroup
            .DOFade(0, fadeDuration)
            .OnComplete(Hide);

        await UniTask.WhenAny(
            _tween.AsyncWaitForCompletion().AsUniTask(),
            _tween.AsyncWaitForKill().AsUniTask());
    }
}
