using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Arrange")]
public class ArrangeSO : ScriptableObject
{
    public List<ArrangeData> Datas = new();

    public ArrangeData GetDataAt(int index)
    {
        if (index < 0 || index >= Datas.Count)
            return null;

        return Datas[index];
    }
}

[Serializable]
public class ArrangeData
{
    public int Row;
    public float Scale = 1;
    public Vector2 Spacing;
}