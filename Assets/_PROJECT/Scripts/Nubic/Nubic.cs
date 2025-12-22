using UnityEngine;

public class Nubic : MonoBehaviour
{
    [SerializeField] private NubicMovement _movement;

    public void SetVelocity(Vector3 velocity)
    {
        _movement.SetVelocity(velocity);
    }
}
