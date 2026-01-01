using Cysharp.Threading.Tasks;
using SanyaBeerExtension;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TablePlacer _tablePlacer;
    [SerializeField] private CameraSetTarget _cameraSetTarget;
    [SerializeField] private NubicMovement _nubicMovement;

    [Header("Delay")]
    [SerializeField] private float _delayAfterDied;
    [SerializeField] private float _delayAfterTable;

    [Header("Continue")]
    [SerializeField] private Button _continueButton;

    [Inject] private WindowSwitcher _windowSwitcher;

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
        _nubicMovement.Stop();

        await UniTask.Delay(_delayAfterDied.ToDelayMillisecond());

        _tablePlacer.Show();
        _cameraSetTarget.DirectToTableScore(_tablePlacer.TablePosition);

        await UniTask.Delay(_delayAfterTable.ToDelayMillisecond());

        _windowSwitcher.Switch<UiInplayingWindow, UiGameoverWindow>(true, true);
    }
}
