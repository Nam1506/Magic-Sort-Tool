using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleController : MonoBehaviour
{
    [Header("Toggles")]
    [SerializeField] private List<Toggle> toggles = new List<Toggle>();
    [SerializeField] private List<TextMeshProUGUI> listTextToggle = new List<TextMeshProUGUI>();

    [Header("Var check is on")]
    public Dictionary<EObstacleKey, bool> dictCheckObstacle = new Dictionary<EObstacleKey, bool>();
    Array listNameObstacle;

    [Header("Data...")]
    private List<int> listIDBottleLock = new List<int>();

    private void Start()
    {
        listNameObstacle = Enum.GetValues(typeof(EObstacleKey));

        foreach (var obstacle in listNameObstacle)
        {
            dictCheckObstacle.Add((EObstacleKey)obstacle, false);
        }

        for (int i = 0; i < toggles.Count; i++)
        {
            int index = i;
            toggles[index].onValueChanged.AddListener((newValue) =>
            {
                OnChangeToggle(index, newValue);
            });
        }
    }

    private void OnChangeToggle(int index, bool value)
    {
        dictCheckObstacle[(EObstacleKey)(index + 1)] = value;

        if (value)
            toggles[index].image.color = Color.green;
        else
            toggles[index].image.color = Color.white;
    }

    public void ChangeStateToggle(int index, bool value)
    {
        toggles[index].isOn = value;
    }

    public bool HasWaterObstacle()
    {
        return dictCheckObstacle[EObstacleKey.isHiddenLayer] || dictCheckObstacle[EObstacleKey.isKey];
    }

    public bool HasTubeObstacle()
    {
        return dictCheckObstacle[EObstacleKey.isHiddenTube] ||
            dictCheckObstacle[EObstacleKey.isLock] ||
            dictCheckObstacle[EObstacleKey.isTap] ||
            dictCheckObstacle[EObstacleKey.isRotate];
    }

    public void AddBottleLock(int id)
    {
        listIDBottleLock.Add(id);
    }

    public void RemoveBottleLock(int id)
    {
        listIDBottleLock.Remove(id);
    }

    public bool CanAddKey()
    {
        return listIDBottleLock.Count > 0;
    }

    public List<int> GetListIDBottleLock()
    {
        return listIDBottleLock;
    }
}

public enum EObstacleKey
{
    isHiddenLayer = 1, // Water
    isHiddenTube = 2,
    isLock = 3,
    isKey = 4,// Water
    isTap = 5,
    isCap = 6,
    isWall = 7,
    isRotate = 8,
    isCCap = 9,
}
