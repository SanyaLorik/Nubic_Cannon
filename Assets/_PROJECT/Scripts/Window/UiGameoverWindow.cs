using Cysharp.Threading.Tasks;
using DG.Tweening;
using SanyaBeerExtension;
using UnityEngine;

public class UiGameoverWindow : Window
{
    [Header("Button Continue")]
    [SerializeField] private ParametrBase<CanvasGroup> _canvasGroupContinue;
    [SerializeField] private float _delayContinue;

    public override void Show()
    {
        base.Show();

        ShowContinueButton().Forget();
    }

    private async UniTaskVoid ShowContinueButton()
    {
        _canvasGroupContinue.Source.alpha = 0;

        await UniTask.Delay(_delayContinue.ToDelayMillisecond());

        _canvasGroupContinue.Source
            .DOFade(1, _canvasGroupContinue.Duration)
            .SetEase(_canvasGroupContinue.Ease);
    }
}