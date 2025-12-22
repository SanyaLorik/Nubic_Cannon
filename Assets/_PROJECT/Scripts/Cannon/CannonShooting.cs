using UnityEngine;

public class CannonShooting : MonoBehaviour
{
    [SerializeField] private CannonOrientation _cannonOrientation;
    [SerializeField] private PowerSpeedometer _powerSpeedometer;
    [SerializeField] private float _power;

    public void Shoot(Nubic nubic)
    {
        _powerSpeedometer.StopArrow();

        float multiplayer = _powerSpeedometer.Multiplayer;
        float power = _power * multiplayer;

        nubic.SetVelocity(_cannonOrientation.Muzzle.right * power);
    }
}
