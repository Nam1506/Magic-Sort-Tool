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
        keyObj.SetActive(false);
        hiddenObj.SetActive(false);

        UpdateUI();
        SetColor(waterData.eColor);
    }

    public void UpdateUI()
    {
        hiddenObj.SetActive(isHidden);
    }

    private void OnWaterClick()
    {
        //Add Obstacle
        if (ToolManager.Instance.obstacleController.HasWaterObstacle())
        {
            if (ToolManager.Instance.obstacleController.isHiddenLayer)
            {
                if (eColor == EColor.None)
                    NotifyControl.Instance.Notify("Ô chưa có màu");
                else
                    isHidden = !isHidden;
            }
            if (ToolManager.Instance.obstacleController.isKey)
            {
                
            }

            UpdateUI();
            return;
        }


        //Change Color
        EColor newEColor = ToolManager.Instance.colorController.GetEColor();
        UpdateColor(newEColor);
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

    public WaterData GetWaterData()
    {
        WaterData waterData = new WaterData();
        waterData.eColor = eColor;
        waterData.isHidden = isHidden;
        waterData.lockKeyObstacle = new LockKeyObstacle();

        return waterData;
    }

    public void OnRemove()
    {
        ColorPickerController.OnChangeWaterColor(eColor, -1);
        eColor = EColor.None;
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
