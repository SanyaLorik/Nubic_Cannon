using UnityEngine;

public class UiInplayingWindow : Window
{
    [Header("Power Speedometer")]
    [SerializeField] private PowerSpeedometer _powerSpeedometer;

    public void ShowPowerSpeedometer()
    {
        _powerSpeedometer.Show();
    }

    public void StartSpeedometer()
    {
        _powerSpeedometer.StartArrow().Forget();
    }
}
