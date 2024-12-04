using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData
{
    public List<TubeData> listTubeData = new();
    public bool isFree;
    public EDifficulty difficulty;
    public int timeLimit;
}

[Serializable]
public class TubeData
{
    public int id;
    public Vector3Serialized pos = new();

    public List<WaterData> WaterDatas = new();

    [Header("Obstacles")]
    public bool isLock;
    public bool IsHidden;
    public EColor unlockColor;
    public bool HasTap;
    public bool HasCap;
    public int NumRotate;
    public EColor CapColor;
    public bool HasIce;

    public TubeData(int id = -1)
    {
        this.id = id;

        for (int i = 0; i < 4; i++)
        {
            WaterDatas.Add(new WaterData());
        }
    }
}

[Serializable]
public class WaterData
{
    public EColor eColor;

    [Header("Obstacles")]
    public bool isHidden;
    public LockKeyObstacle lockKeyObstacle;

    public WaterData()
    {
        eColor = EColor.None;
        isHidden = false;
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


public enum EDifficulty
{
    Normal = 0,
    Hard = 1,
    SuperHard = 2,
}


#region Custom Attributes
[Serializable]
public class Vector3Serialized
{
    public float x, y, z;

    public Vector3Serialized()
    {

    }

    public Vector3Serialized(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Vector3Serialized(Vector3 v)
    {
        this.x = v.x;
        this.y = v.y;
        this.z = v.z;
    }

    public Vector3 GetValue()
    {
        return new Vector3(x, y, z);
    }
}

[Serializable]
public class LockKeyObstacle
{
    public int bottleID;

    public LockKeyObstacle(int bottleID)
    {
        this.bottleID = bottleID;
    }   
}
#endregion
