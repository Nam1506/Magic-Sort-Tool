using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleController : MonoBehaviour
{
    [Header("Toggles")]
    [SerializeField] private List<Toggle> toggles = new List<Toggle>();
    [SerializeField] private List<bool> checkObstacles = new List<bool>();

    [Header("Var check is on")]
    public bool isHiddenLayer;
    public bool isHiddenTube;
    public bool isLock;
    public bool isKey;
    public bool isTap;
    public bool isCap;
    public bool isWall;
    public bool isRotate;
    public bool isCCap;

    public void UpdateState()
    {
        for (int i = 0; i < toggles.Count; i++)
        {
            toggles[i].isOn = checkObstacles[i];

            if (checkObstacles[i])
                toggles[i].image.color = Color.green;
            else
                toggles[i].image.color = Color.white;
        }
    }

    public bool HasWaterObstacle()
    {
        return isKey || isHiddenLayer;
    }

    public bool HasTubeObstacle()
    {
        return isHiddenTube || isLock || isTap;
    }
}
