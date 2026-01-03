using UnityEngine;

public class NubicReady : MonoBehaviour
{
    [SerializeField] private Transform _nubic;
    [SerializeField] private Transform _startPosition;

    public void ReturnToBeReady()
    {
        _nubic.position = _startPosition.position;
    }
}
