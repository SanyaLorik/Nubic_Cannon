using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UiShopWindow : Window
{
    [Header("Buttons")]
    [SerializeField] private Button _backButton;

    [Inject] private WindowSwitcher _windowSwitcher;

    private void OnEnable()
    {
        _backButton.onClick.AddListener(OnMainWindowShow);
    }

    private void OnDisable()
    {
        _backButton.onClick.RemoveListener(OnMainWindowShow);
    }

    private void OnMainWindowShow()
    {
        _windowSwitcher.Switch<UiShopWindow, UiMenuWindow>(true, true);
    }
}