using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UiSettingWindow : Window
{
    [Header("Buttons")]
    [SerializeField] private Button _backButton;

    [Inject] private WindowSwitcher _windowSwitcher;

    private void OnEnable()
    {
        _backButton.onClick.AddListener(OnClose);
    }

    private void OnDisable()
    {
        _backButton.onClick.RemoveListener(OnClose);
    }

    private void OnClose()
    {
        _windowSwitcher.Hide<UiSettingWindow>();
    }
}
