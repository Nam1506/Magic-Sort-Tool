using UnityEngine;
using UnityEngine.UI;

public class Water : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Button button;
    [SerializeField] private GameObject keyObj;
    [SerializeField] private GameObject hiddenObj;

    private EColor eColor = EColor.None;

    private bool isHidden;
    private bool isKey;
    private LockKeyObstacle lockKeyObstacle;

    private void Reset()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        button.onClick.AddListener(OnWaterClick);
        InitWater(new WaterData());
    }

    public void InitWater(WaterData waterData)
    {
        isHidden = waterData.isHidden;
        if (waterData.lockKeyObstacle != null )
        {
            isKey = true;
            lockKeyObstacle = waterData.lockKeyObstacle;
        }

        UpdateUI();
        SetColor(waterData.eColor);
    }

    public void UpdateUI()
    {
        hiddenObj.SetActive(isHidden);
        keyObj.SetActive(isKey);
    }

    private void OnWaterClick()
    {
        //Add Obstacle
        if (ToolManager.Instance.obstacleController.HasWaterObstacle())
        {
            if (ToolManager.Instance.obstacleController.dictCheckObstacle[EObstacleKey.isHiddenLayer])
            {
                if (eColor == EColor.None)
                    NotifyControl.Instance.Notify("Ô chưa có màu");
                else
                    isHidden = !isHidden;
            }
            if (ToolManager.Instance.obstacleController.dictCheckObstacle[EObstacleKey.isKey])
            {
                if (isKey)
                {
                    ToolManager.Instance.obstacleController.AddBottleLock(lockKeyObstacle.bottleID);
                    isKey = false;
                    lockKeyObstacle = null;
                }
                else if (ToolManager.Instance.obstacleController.CanAddKey())
                {
                    NotifyControl.Instance.NotifySelectBottleLock(AddKey);
                }
            }

            UpdateUI();
            return;
        }

        //Change Color
        EColor newEColor = ToolManager.Instance.colorController.GetEColor();
        UpdateColor(newEColor);
    }

    private void AddKey(int bottleID)
    {
        isKey = true;
        lockKeyObstacle = new LockKeyObstacle(bottleID);
        ToolManager.Instance.obstacleController.RemoveBottleLock(bottleID);
        keyObj.SetActive(true);
    }

    private void ResetColor()
    {
        UpdateColor(EColor.None);
    }

    public void UpdateColor(EColor newEColor)
    {
        ColorPickerController.OnChangeWaterColor(eColor, -1);
        SetColor(newEColor);
    }

    private void SetColor(EColor newEColor)
    {
        eColor = newEColor;

        ColorPickerController.OnChangeWaterColor(eColor, 1);

        Color color = ToolManager.Instance.colorController.colorSO.GetColor(eColor);
        button.image.color = color;
    }

    public EColor GetColor()
    {
        return eColor;
    }

    public WaterData GetWaterData()
    {
        WaterData waterData = new WaterData();
        waterData.eColor = eColor;
        waterData.isHidden = isHidden;
        waterData.lockKeyObstacle = lockKeyObstacle;

        return waterData;
    }

    public void OnRemove()
    {
        keyObj.SetActive(false);
        hiddenObj.SetActive(false);
        isKey = false;
        isHidden = false;
        lockKeyObstacle = null;

        ColorPickerController.OnChangeWaterColor(eColor, -1);
        button.image.color = Color.white;
    }

    private void OnEnable()
    {
        ColorPickerController.OnResetColor += ResetColor;
    }

    private void OnDisable()
    {
        ColorPickerController.OnResetColor -= ResetColor;
    }
}
