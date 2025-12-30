using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UiFaceWindow : Window
{
    [Header("Buttons")]
    [SerializeField] private Button _backButton;

    [Inject] private WindowSwitcher _windowSwitcher;

    private void OnEnable()
    {
        _backButton.onClick.AddListener(OnOnMainWindowShow);
    }

    private void OnDisable()
    {
        _backButton.onClick.RemoveListener(OnOnMainWindowShow);
    }

    private void OnOnMainWindowShow()
    {
        _windowSwitcher.Switch<UiFaceWindow, UiMenuWindow>(true, true);
    }
}