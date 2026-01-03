using Architecture_M;
using UnityEngine;
using Zenject;

public class EconomicNC : MonoBehaviour
{
    [SerializeField] private CurrencyTypeSO _baseCurrency;

    [Inject] private CurrencyManager _currencyManager;
    [Inject] private GameDataNC _gameData;

    private void Start()
    {
        int money = _gameData.EconomicSave.Money;
        AddMoney(money);
    }

    public void AddMoney(int money)
    {
        _currencyManager.AddCurrencyAmount(_baseCurrency, money);
    }
}