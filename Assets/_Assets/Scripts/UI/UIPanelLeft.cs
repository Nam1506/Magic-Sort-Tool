using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelLeft : MonoBehaviour
{
    [Header("Save Load Components")]
    public TMP_InputField levelInputField;
    [SerializeField] private Button saveBtn;
    [SerializeField] private Button loadBtn;
    [SerializeField] private Button nextLevelBtn;
    [SerializeField] private Button prevLevelBtn;

    [Header("Other")]
    [SerializeField] private Button freeMapBtn;
    [SerializeField] private TextMeshProUGUI textStateFreeMap;

    [Header("TimeLimit")]
    [SerializeField] private TMP_InputField inputTime;

    [Header("Difficulty")]
    [SerializeField] private TMP_Dropdown dropdownDifficulty;

    private void Awake()
    {
        saveBtn.onClick.AddListener(() =>
        {
            DataManager.Instance.CheckLevelExistsToSave();
        });

        loadBtn.onClick.AddListener(OnClickLoadLevel);

        freeMapBtn.onClick.AddListener(OnClickFreeMap);

        nextLevelBtn.onClick.AddListener(() => OnClickLevelTrans(1));
        prevLevelBtn.onClick.AddListener(() => OnClickLevelTrans(-1));

        inputTime.onValueChanged.AddListener((timeStr) => { ChangeTimeLimit(timeStr); });
        dropdownDifficulty.onValueChanged.AddListener((option) => { ChangeDifficulty(option); });
    }

    private void Start()
    {
        DropdownUtil.SetupDropdown<EDifficulty>(dropdownDifficulty);
    }

    private void OnClickLoadLevel()
    {
        int level;
        if (!int.TryParse(levelInputField.text, out level))
        {
            NotifyControl.Instance.Notify("Chưa nhập level");
            return;
        }

        DataManager.Instance.LoadLevelData(level);
    }

    private void OnClickLevelTrans(int valueChange)
    {
        int level;
        int.TryParse(levelInputField.text, out level);

        level += valueChange;
        if (level <= 0) level = 1;

        levelInputField.text = level.ToString();
        DataManager.Instance.LoadLevelData(level);
    }

    private void OnClickFreeMap()
    {
        if (ToolManager.Instance.UpdateFreeMap())
        {
            textStateFreeMap.text = "FreeMap: On";
            freeMapBtn.image.color = Color.green;
        }
        else
        {
            textStateFreeMap.text = "FreeMap: Off";
            freeMapBtn.image.color = Color.white;
        }
    }

    private void ChangeTimeLimit(string timeStr)
    {
        int timeLimit = 60;

        int.TryParse(timeStr, out timeLimit);

        ToolManager.Instance.timeLimit = timeLimit;
    }

    private void ChangeTimeLimit(int timeLimit)
    {
        LoadTimeLimit(timeLimit);
        ToolManager.Instance.timeLimit = timeLimit;
    }

    public void LoadTimeLimit(int timeLimit)
    {
        inputTime.text = timeLimit.ToString();
    }

    public void LoadDifficulty(EDifficulty difficulty)
    {
        dropdownDifficulty.value = (int)difficulty;
    }

    private void ChangeDifficulty(int option)
    {
        EDifficulty eDifficulty = (EDifficulty)option;

        ToolManager.Instance.difficulty = eDifficulty;
    }
}
