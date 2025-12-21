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

    public void Show()
    {
        _tween?.Kill();

        container.ActiveSelf();
    }

    public void ShowWithUnfade()
    {
        _tween?.Kill();

        _tween = canvasGroup
            .DOFade(1, unfadeDuration)
            .OnComplete(Show);
    }

    public void Hide()
    {
        _tween?.Kill();

        container.DisactiveSelf();
    }

    public void HideWithFade()
    {
        _tween?.Kill();

        _tween = canvasGroup
            .DOFade(0, fadeDuration)
            .OnComplete(Hide);
    }
}
