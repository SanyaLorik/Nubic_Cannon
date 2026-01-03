using UnityEngine;

public class Nubic : MonoBehaviour
{
    [SerializeField] private NubicMovement _movement;
    [SerializeField] private NubicReady _startPosition;

    public void SetVelocity(Vector3 velocity)
    {
        _movement.SetVelocity(velocity);
    }

    public void ReturnToBeReady()
    {
        _startPosition.ReturnToBeReady();
    }
}
