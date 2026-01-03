using System;
using TMPro;
using UnityEngine;

public class TablePlacer : MonoBehaviour
{
    [SerializeField] private Transform _table;
    [SerializeField] private NubicMovement _nubicMovement;
    [SerializeField] private TextMeshProUGUI _scoreText;

    public Vector3 TablePosition { get; private set; } = Vector3.zero;

    public void Show(int distance)
    {
        SetScoreText(distance);
        TablePosition = _nubicMovement.DeathPosition;
        _table.position = TablePosition;
    }

    private void SetScoreText(int distance)
    {
        _scoreText.text = distance.ToString();
    }
}