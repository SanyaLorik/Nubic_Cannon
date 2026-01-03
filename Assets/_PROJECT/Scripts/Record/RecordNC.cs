using System;
using UnityEngine;
using Zenject;

public class RecordNC : MonoBehaviour
{
    [Inject] private WindowSwitcher _windowSwitcher;
    [Inject] private GameDataNC _gameData;

    private void Start()
    {
        SetRecordText(_gameData.RecordSave.Distance);
    }

    public void SetNewRecord(int distance)
    {
        int maxDistance = Mathf.Max(distance, _gameData.RecordSave.Distance);
        SetRecordText(maxDistance);
    }

    private void SetRecordText(int distance)
    {
        UiMenuWindow menuWindow = _windowSwitcher.GetWindow<UiMenuWindow>();
        menuWindow.SetRecordText(distance);
    }
}

[Serializable]
public class RecordNCSave
{
    public int Distance;
}