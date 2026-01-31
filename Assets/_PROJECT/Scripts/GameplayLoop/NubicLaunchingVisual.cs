using SanyaBeerExtension;
using UnityEngine;

public class NubicLaunchingVisual : MonoBehaviour 
{
    [SerializeField] private GameObject _nubicMenu;
    [SerializeField] private GameObject _nubicCannon;

    public void SetLaunching()
    {
        _nubicMenu.DisactiveSelf();
        _nubicCannon.ActiveSelf();
    }
}