using Architecture_M;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class EconomicNC : MonoBehaviour
{
    [SerializeField] private CurrencyTypeSO _baseCurrency;

    [Inject] private CurrencyManager _currencyManager;
    [Inject] private IGameSave<GameSaveNC> _gameData;

    private void Start()
    {
        int money = _gameData.GetSave.Economic.Money;
        AddMoney(money);
    }

    public void AddMoney(int money)
    {
        _currencyManager.AddCurrencyAmount(_baseCurrency, money);
    }
}