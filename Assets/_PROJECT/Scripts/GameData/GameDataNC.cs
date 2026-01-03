using System;
using UnityEngine;

[Serializable]
public class GameDataNC : GameDataBase
{
    [field: Header("Cannon")]
    [field: SerializeField] public float PowerCannonShooting { get; private set; }

    [field: Header("Power Speedometer")]
    [field: SerializeField] public AnimationCurve FalloffCurve { get; private set; }

    [field: Header("Money")]
    [field: SerializeField] public CurrencyTypeSO MoneyTypeSO { get; private set; }
    [field: SerializeField] public float CurrentMoneyRatio { get; private set; }

    [field: Header("Ground")]
    [field: SerializeField] public string ReboundTag { get; private set; } = "Rebound";

    [Header("Save")]
    public EconomicNCSave EconomicSave;
}