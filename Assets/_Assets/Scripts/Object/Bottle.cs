using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private Water waterPrefab;

    public List<Water> waters = new();

    public void Init(BottleData data)
    {
        foreach (var waterData in data.WaterDatas)
        {
            var water = Instantiate(waterPrefab, content);

            waters.Add(water);
        }
    }
}
