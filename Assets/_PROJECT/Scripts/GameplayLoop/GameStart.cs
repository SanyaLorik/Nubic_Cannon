using Architecture_M;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class GameStart : MonoBehaviour
{
    [SerializeField] private Cannon _cannon;
    [SerializeField] private CameraMovement _cameraMovement;

    [Inject] private WindowSwitcher _windowSwitcher;
    [Inject] private IInputDirection3 _input;

    private void Start()
    {
        WaitInputAsync().Forget();
    }

    public async UniTaskVoid WaitInputAsync()
    {
        await UniTask.WaitWhile(() => _input.Direction3 == Vector3.zero);

        _cameraMovement.DirectToInplay();
        (_, UiInplayingWindow inplayingWindow) = _windowSwitcher.Switch<UiMenuWindow, UiInplayingWindow>(true, false);

        inplayingWindow.ShowPowerSpeedometer();
        inplayingWindow.StartSpeedometer();

        //WaitInputAsync().Forget();
    }

    public async UniTaskVoid WaitShoot()
    {
        //await UniTask.WaitWhile(() => _input.Direction3 == Vector3.zero);
    }
}
