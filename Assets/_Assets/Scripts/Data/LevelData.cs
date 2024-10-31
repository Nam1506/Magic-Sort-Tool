using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData
{
    public List<BottleData> BottleDatas = new();
}

public class BottleData
{
    public List<WaterData> WaterDatas = new();

    public BottleData(int num)
    {
        for (int i = 0; i < num; i++)
        {
            WaterDatas.Add(new WaterData());
        }
    }
}

public class WaterData
{
    public EColor eColor;

    public bool IsHidden;

    public WaterData()
    {
        eColor = EColor.None;
        IsHidden = false;
    }

    public bool ShouldSerializeIsHidden()
    {
        return IsHidden;
    }
}

public enum EColor
{
    None = 0,
    C1,
    C2,
    C3,
    C4,
    C5,
    C6,
    C7,
    C8,
    C9,
    C10,
    C11,
    C12
}
