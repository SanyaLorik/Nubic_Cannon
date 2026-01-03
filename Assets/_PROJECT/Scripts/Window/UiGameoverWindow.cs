using Cysharp.Threading.Tasks;
using DG.Tweening;
using SanyaBeerExtension;
using TMPro;
using UnityEngine;

public class UiGameoverWindow : Window
{
    [Header("Button Continue")]
    [SerializeField] private ParametrBase<CanvasGroup> _canvasGroupContinue;
    [SerializeField] private float _delayContinue;

    [Header("Award")]
    [SerializeField] private TextMeshProUGUI _awardMoneyText;

    public override void Show()
    {
        base.Show();

        ShowContinueButton().Forget();
    }

    public void SetAwardMoneyText(int awardMoney)
    {
        _awardMoneyText.text = awardMoney.ToString();
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