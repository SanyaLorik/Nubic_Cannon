using UnityEngine;

public class Cannon : MonoBehaviour
{

    [SerializeField] private CannonOrientation _orientation;
    [SerializeField] private CannonShooting _shooting;
    [SerializeField] private Nubic _nubic;

    public void Shoot()
    {
        _shooting.Shoot(_nubic);
        _orientation.DisableMoving();
    }
}