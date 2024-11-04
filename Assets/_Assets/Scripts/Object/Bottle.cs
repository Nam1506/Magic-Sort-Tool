using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bottle : MonoBehaviour
{
    public int id;
    [SerializeField] private TextMeshProUGUI textID;

    [Header("haha")]
    [SerializeField] private Button button;
    [SerializeField] private Transform content;
    [SerializeField] private Water waterPrefab;
    [SerializeField] private GameObject selectedObj;

    [Header("Obstacle")]
    [SerializeField] private GameObject hiddenObj;
    [SerializeField] private Image colorUnHidenImg;
    [SerializeField] private GameObject lockedObj;
    [SerializeField] private GameObject tapObj;
    [Header("Variables for obstacle data")]
    public bool isLock;
    public bool isHidden;
    public EColor unlockColor;
    public bool isHasTap;
    public bool isHasCap;
    public int numRotate = int.MaxValue;
    public EColor CapColor;
    public bool isHasIce;


    public List<Water> waters = new();

    private bool isSelected;

    private void Reset()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        button.onClick.AddListener(OnClick);

        while (waters.Count < 4)
        {
            Water water = Instantiate(waterPrefab, content);
            waters.Add(water);
        }
    }

    public void Init(BottleData data)
    {
        for (int i = 0; i < waters.Count; i++)
        {
            waters[i].InitWater(data.WaterDatas[i]);
        }

        id = data.id;
        textID.text = $"ID: {id.ToString("D2")}";

        isLock = data.isLock;
        isHidden = data.IsHidden;
        unlockColor = data.unlockColor;
        isHasTap = data.HasTap;
        isHasCap = data.HasCap;
        numRotate = data.NumRotate;
        CapColor = data.CapColor;
        isHasIce = data.HasIce;

        UpdateUI(); 
    }

    public void SetRandomColor()
    {
        foreach(var water in waters)
        {
            water.UpdateColor(ToolManager.Instance.colorController.GetEColorRandom());
        }
    }

    public void OnRemove()
    {
        hiddenObj.SetActive(false);
        lockedObj.SetActive(false);
        tapObj.SetActive(false);

        foreach (var water in waters)
        {
            water.OnRemove();
        }
    }

    public void OnClick()
    {
        if (ToolManager.Instance.obstacleController.dictCheckObstacle[EObstacleKey.isHiddenTube] && CheckCanHidden())
        {
            if (!isHidden)
                NotifyControl.Instance.NotifyColorHiddenTube(CreateHiddenTube);
            else
                RemoveHiddenTube();
        }
        else if (ToolManager.Instance.obstacleController.dictCheckObstacle[EObstacleKey.isLock] && !isHidden)
        {
            isLock = !isLock;
            UpdateBottleLock();
        }
        else if (ToolManager.Instance.obstacleController.dictCheckObstacle[EObstacleKey.isTap])
        {
            if (isHasTap)
                isHasTap = false;
            else if (CheckCanAddTap())
                isHasTap = true;
        }
        else
        {
            SelectTube(!isSelected);
        }

        UpdateUI();
    }

    #region Obstacle Range
    private bool CheckCanAddTap()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1000f, LayerMask.GetMask("Collider"));

        if (hit.collider != null)
        {
            return true;
        }
            NotifyControl.Instance.NotifyConsole("Không có bình ở dưới, không thể thêm Tap");

        return false;
    }

    private bool CheckCanHidden()
    {
        if (isLock)
        {
            NotifyControl.Instance.NotifyConsole("Đã có Obstacle Lock, không thể thêm hidden");
            return false;
        }

        foreach (var water in waters)
        {
            if (water.GetColor() == EColor.None)
            {
                NotifyControl.Instance.NotifyConsole("Vui Lòng điền màu cho tất cả các ô");
                return false;
            }
        }

        return true;
    }

    private void CreateHiddenTube(ColorData colorData)
    {
        isHidden = true;

        hiddenObj.SetActive(true);
        colorUnHidenImg.color = colorData.color;
    }

    private void RemoveHiddenTube()
    {
        isHidden = false;
        hiddenObj.SetActive(false);
    }

    private void UpdateBottleLock()
    {
        if (isLock)
        {
            ToolManager.Instance.obstacleController.AddBottleLock(id);
        }
        else
        {
            ToolManager.Instance.obstacleController.RemoveBottleLock(id);
        }
    }

    private void UpdateUI()
    {
        hiddenObj.SetActive(isHidden);
        lockedObj.SetActive(isLock);
        tapObj.SetActive(isHasTap);

        if (isHidden)
        {
            colorUnHidenImg.color = ToolManager.Instance.colorSO.GetColor(unlockColor);
        }
    }
#endregion

    public void SelectTube(bool isSelected)
    {
        this.isSelected = isSelected;
        selectedObj.SetActive(isSelected);

        TubeManager.Instance.UpdateBottleSelected(this, isSelected);
    }

    public BottleData GetBottleData()
    {
        BottleData bottleData = new BottleData(id);

        bottleData.WaterDatas.Clear();
        foreach (var water in waters)
        {
            bottleData.WaterDatas.Add(water.GetWaterData());
        }

        bottleData.isLock = isLock;
        bottleData.IsHidden = isHidden;
        bottleData.unlockColor = unlockColor;// EColor
        bottleData.HasTap = isHasTap;
        bottleData.HasCap = isHasCap;
        bottleData.HasIce = isHasIce;
        bottleData.NumRotate = numRotate;
        bottleData.CapColor = CapColor;// EColor

        return bottleData;
    }
}
