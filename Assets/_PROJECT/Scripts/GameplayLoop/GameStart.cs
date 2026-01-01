using Architecture_M;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class GameStart : MonoBehaviour
{
    [SerializeField] private Cannon _cannon;
    [SerializeField] private CameraSetTarget _cameraMovement;
    [SerializeField] private DistanceTracker _distanceTracker;

    [Inject] private WindowSwitcher _windowSwitcher;
    [Inject] private IInputDirection3 _inputDirection;
    [Inject] private IInputHaving _inputHaving;

    private void Start()
    {
        WaitInputAsync().Forget();
    }

    public async UniTaskVoid WaitInputAsync()
    {
        await UniTask.WaitWhile(() => _inputDirection.Direction3 == Vector3.zero);

        _cameraMovement.DirectToInplay();
        (_, UiInplayingWindow inplayingWindow) = _windowSwitcher.Switch<UiMenuWindow, UiInplayingWindow>(true, true);

        inplayingWindow.ShowPowerSpeedometer();
        inplayingWindow.StartSpeedometer();

        await UniTask.WaitWhile(() => _inputHaving.IsHaving == true);

        _cameraMovement.DirectToNubicFly();
        _cannon.Shoot();
        inplayingWindow.HidePowerSpeedometer();
        _distanceTracker.StartTrackAsync().Forget();
    } 
}
