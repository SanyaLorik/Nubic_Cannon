using Architecture_M;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class GameStart : MonoBehaviour
{
    [SerializeField] private UiMenuWindow _uiMenuWindow;
    [SerializeField] private CameraMovement _cameraMovement;

    [Inject] private IInputDirection3 _input;

    private void Start()
    {
        WaitInputAsync().Forget();
    }

    public async UniTask WaitInputAsync()
    {
        await UniTask.WaitWhile(() => _input.Direction3 == Vector3.zero);
        _cameraMovement.DirectToInplay();
        _uiMenuWindow.Hide();
    }
}
