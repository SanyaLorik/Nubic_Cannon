using Architecture_M;
using System;
using TMPro;
using UnityEngine;
using Zenject;

public class UiInplayingWindow : Window
{
    [Header("Money")]
    [SerializeField] private TextMeshProUGUI _moneyText;

    [Header("Power Speedometer")]
    [SerializeField] private PowerSpeedometer _powerSpeedometer;

    [Header("Distance Tracker")]
    [SerializeField] private DistanceTracker _distanceTracker;
    [SerializeField] private TextMeshProUGUI _scoreText;

    [Inject] private CurrencyManager _currencyManager;

    private void OnEnable()
    {
        _distanceTracker.OnTracked += OnUpdateScore;
        _currencyManager.OnCurrencyChanged += OnUpdateMoney;
    }

    private void OnDisable()
    {
        _distanceTracker.OnTracked -= OnUpdateScore;
        _currencyManager.OnCurrencyChanged -= OnUpdateMoney;
    }

    private void OnUpdateScore(int score)
    {
        _scoreText.text = score.ToString();
    }

    private void OnUpdateMoney(CurrencyTypeSO _, double money)
    {
        SetMoneyText((int)money);
    }

    public void ShowPowerSpeedometer()
    {
        _powerSpeedometer.Show();
    }

    public void HidePowerSpeedometer()
    {
        _powerSpeedometer.Hide();
    }

    public void StartSpeedometer()
    {
        _powerSpeedometer.StartArrow().Forget();
    }

    private void SetMoneyText(int money)
    {
        _moneyText.text = money.ToString();
    }
}
