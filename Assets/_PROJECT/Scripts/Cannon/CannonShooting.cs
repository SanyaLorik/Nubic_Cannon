using UnityEngine;
using Zenject;

public class CannonShooting : MonoBehaviour
{
    [SerializeField] private CannonOrientation _cannonOrientation;
    [SerializeField] private PowerSpeedometer _powerSpeedometer;

    [Inject] private GameDataNC _gameDataNC;

    public void Shoot(Nubic nubic)
    {
        _powerSpeedometer.StopArrow();

        float multiplayer = _powerSpeedometer.Multiplayer;
        float power = _gameDataNC.PowerCannonShooting * multiplayer;

        nubic.SetVelocity(_cannonOrientation.Muzzle.right * power);
    }
}
