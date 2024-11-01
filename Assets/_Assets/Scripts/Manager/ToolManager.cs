using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : SingletonBase<ToolManager>
{
    public UIPanelLeft uiPanelLeft;

    [Header("Controller")]
    public ColorPickerController colorController;
    public ObstacleController obstacleController;

    public bool isFreeMap;

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    BottleManager.Instance.CreateBottle(new BottleData(4));
        //}
    }

    public bool UpdateFreeMap()
    {
        isFreeMap = !isFreeMap;
        return isFreeMap;
    }
}
