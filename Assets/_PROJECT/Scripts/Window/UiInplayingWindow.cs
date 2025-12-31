using System;
using TMPro;
using UnityEngine;

public class UiInplayingWindow : Window
{
    [Header("Power Speedometer")]
    [SerializeField] private PowerSpeedometer _powerSpeedometer;

    [Header("Distance Tracker")]
    [SerializeField] private DistanceTracker _distanceTracker;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void OnEnable()
    {
        _distanceTracker.OnTracked += OnUpdateScore;
    }

    private void OnDisable()
    {
        _distanceTracker.OnTracked -= OnUpdateScore;
    }

    private void OnUpdateScore(int score)
    {
        _scoreText.text = score.ToString();
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
}
