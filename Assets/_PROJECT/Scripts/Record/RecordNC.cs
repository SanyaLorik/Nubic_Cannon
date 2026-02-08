using Architecture_M;
using System;
using UnityEngine;
using Zenject;

public class RecordNC : MonoBehaviour
{
    [Inject] private WindowSwitcher _windowSwitcher;
    [Inject] private IGameSave<GameSaveNC> _gameData;

    private GameSaveNC _gameSave;

    private void Start()
    {
        SetRecordText(_gameData.GetSave.Record.Distance);
    }

    public void SetNewRecord(int distance)
    {
        int maxDistance = Mathf.Max(distance, _gameSave.Record.Distance);
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