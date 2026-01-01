using TMPro;
using UnityEngine;

public class TablePlacer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] private DistanceTracker _distanceTracker;

    public Vector3 TablePosition { get; private set; } = Vector3.zero;

    public void Show()
    {

    }
}