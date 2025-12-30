using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UiMenuWindow : Window
{
    [Header("Buttons")]
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _faceChangingButton;

    [Inject] private WindowSwitcher _windowSwitcher;

    private void OnEnable()
    {
        _shopButton.onClick.AddListener(OnShowShop);
        _settingButton.onClick.AddListener(OnShowSetting);
        _faceChangingButton.onClick.AddListener(OnShowFaceChanging);
    }

    private void OnDisable()
    {
        _shopButton.onClick.RemoveListener(OnShowShop);
        _settingButton.onClick.RemoveListener(OnShowSetting);
        _faceChangingButton.onClick.RemoveListener(OnShowFaceChanging);
    }

    private void OnShowShop()
    {
        _windowSwitcher.Switch<UiMenuWindow, UiShopWindow>(true, true);
    }

    private void OnShowSetting()
    {
        _windowSwitcher.Show<UiSettingWindow>();
    }

    private void OnShowFaceChanging()
    {
        _windowSwitcher.Switch<UiMenuWindow, UiFaceWindow>(true, true);
    }
}