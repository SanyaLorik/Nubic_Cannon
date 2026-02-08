using Architecture_M;
using System;

[Serializable]
public class GameSaveNC : GameSaveBase
{
    public EconomicNCSave Economic = new();
    public RecordNCSave Record = new();
}
