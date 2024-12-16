using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : SingletonBase<ToolManager>
{
    public static Action<bool> OnShowLayer;
    public static Action<bool> OnShowTube;

    private bool isShowLayer;
    private bool isShowTube;

    public UIPanelLeft uiPanelLeft;

    [Header("Data")]
    public ColorSO colorSO;

    [Header("Controller")]
    public ColorPickerController colorController;
    public ObstacleController obstacleController;

    public bool isFreeMap;
    public int timeLimit;
    public EDifficulty difficulty;

    private void Awake()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
        Screen.SetResolution(1920, 1080, false);
    }

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

    public bool SetShowLayer()
    {
        isShowLayer = !isShowLayer;

        OnShowLayer?.Invoke(isShowLayer);

        return isShowLayer;
    }

    public bool SetShowTube()
    {
        isShowTube = !isShowTube;

        OnShowTube?.Invoke(isShowTube);

        return isShowTube;
    }

}
