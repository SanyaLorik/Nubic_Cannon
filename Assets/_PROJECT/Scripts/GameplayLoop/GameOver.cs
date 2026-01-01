using Cysharp.Threading.Tasks;
using SanyaBeerExtension;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TablePlacer _tablePlacer;
    [SerializeField] private DistanceTracker _distanceTracker;
    [SerializeField] private CameraSetTarget _cameraSetTarget;
    [SerializeField] private NubicMovement _nubicMovement;

    [Header("Delay")]
    [SerializeField] private float _delayAfterDied;
    [SerializeField] private float _delayAfterTable;

    [Header("Continue")]
    [SerializeField] private Button _continueButton;

    [Inject] private WindowSwitcher _windowSwitcher;
    [Inject] private GameDataNC _gameDataNC;

    private void OnEnable()
    {
        _nubicMovement.OnDead += OnOver;
        _continueButton.onClick.AddListener(OnNext);
    }

    private void OnDisable()
    {
        _nubicMovement.OnDead -= OnOver;
        _continueButton.onClick.RemoveListener(OnNext);
    }

    private void OnOver()
    {
        Final().Forget();
    }

    private void OnNext()
    {
        _cameraSetTarget.DirectToMenu();
        _windowSwitcher.Switch<UiGameoverWindow, UiMenuWindow>(true, true);
    }

    private async UniTaskVoid Final()
    {
        UpdateAward();

        _nubicMovement.Stop();

        await UniTask.Delay(_delayAfterDied.ToDelayMillisecond());

        _tablePlacer.Show();
        _cameraSetTarget.DirectToTableScore(_tablePlacer.TablePosition);

        await UniTask.Delay(_delayAfterTable.ToDelayMillisecond());

        _windowSwitcher.Switch<UiInplayingWindow, UiGameoverWindow>(true, true);
    }

    private void UpdateAward()
    {
        UiGameoverWindow gameoverWindow = _windowSwitcher.GetWindow<UiGameoverWindow>();

        int awardMoney = (int)(_distanceTracker.DistantionTracked * _gameDataNC.CurrentMoneyRatio);
        gameoverWindow.SetAwardMoneyText(awardMoney);
    }
}
