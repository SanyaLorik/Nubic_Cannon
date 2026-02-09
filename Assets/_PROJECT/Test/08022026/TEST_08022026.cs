using Architecture_M;
using UnityEngine;
using Zenject;

public class TEST_08022026 : MonoBehaviour
{
    public int number; 

    [Inject]
    private void Construct(IGameSave<GameSaveNC> _gameSave, LocalizationDataNC localizationDataNC)
    {
        //_gameSave.GetSave.Economic.Money = number;
        //_gameSave.Save();
        print(localizationDataNC);
    }
}