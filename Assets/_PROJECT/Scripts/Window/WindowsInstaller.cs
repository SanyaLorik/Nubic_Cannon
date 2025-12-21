using UnityEngine;
using Zenject;

public class WindowsInstaller : MonoInstaller
{
    [SerializeField] private Window[] _windows;

    private WindowSwitcher _windowSwitcher;

    public override void InstallBindings()
    {
        BindWindowSwitcher();
    }

    private void BindWindowSwitcher()
    {
        _windowSwitcher = new(_windows);

        Container
            .BindInterfacesAndSelfTo(typeof(WindowSwitcher))
            .FromInstance(_windowSwitcher)
            .AsCached();
    }
}