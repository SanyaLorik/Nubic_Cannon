using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UiMenuWindow : Window
{
    [Header("Buttons")]
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _settingButton;

    [Inject] private WindowSwitcher _windowSwitcher;

    private void OnEnable()
    {
        _shopButton.onClick.AddListener(OnShowShop);
        _settingButton.onClick.AddListener(OnShowSetting);
    }

    private void OnDisable()
    {
        _shopButton.onClick.RemoveListener(OnShowShop);
        _settingButton.onClick.RemoveListener(OnShowSetting);
    }

    private void OnShowShop()
    {
        _windowSwitcher.Switch<UiMenuWindow, UiShopWindow>();
    }

    private void OnShowSetting()
    {
        _windowSwitcher.Show<UiSettingWindow>();
    }
}