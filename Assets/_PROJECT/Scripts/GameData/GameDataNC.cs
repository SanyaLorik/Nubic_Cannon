using System;
using UnityEngine;

[Serializable]
public class GameDataNC : GameDataBase
{
    [field: Header("Cannon")]
    [field: SerializeField] public float PowerCannonShooting { get; private set; }

    [field: Header("Power Speedometer")]
    [field: SerializeField] public AnimationCurve FalloffCurve { get; private set; }
}