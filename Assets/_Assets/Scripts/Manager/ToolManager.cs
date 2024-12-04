using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : SingletonBase<ToolManager>
{
    public UIPanelLeft uiPanelLeft;

    [Header("Data")]
    public ColorSO colorSO;

    [Header("Controller")]
    public ColorPickerController colorController;
    public ObstacleController obstacleController;

    public bool isFreeMap;
    public int timeLimit;
    public EDifficulty difficulty;

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

    public void LoadData(LevelData levelData)
    {
        isFreeMap = levelData.isFree;
        timeLimit = levelData.timeLimit;
        difficulty = levelData.difficulty;

        SetUI();
    }

    private void SetUI()
    {
        uiPanelLeft.LoadDifficulty(difficulty);
        uiPanelLeft.LoadTimeLimit(timeLimit);
    }
}
