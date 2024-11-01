using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bottle : MonoBehaviour
{
    [Header("haha")]
    [SerializeField] private Button button;
    [SerializeField] private Transform content;
    [SerializeField] private Water waterPrefab;
    [SerializeField] private GameObject selectedObj;

    [Header("Obstacle")]
    [SerializeField] private GameObject hiddenObj;
    [SerializeField] private GameObject lockedObj;
    [SerializeField] private Image colorUnHidenImg;

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
        foreach (var water in waters)
        {
            water.OnRemove();
        }
    }

    public void OnClick()
    {
        SelectTube(!isSelected);
    }

    public void SelectTube(bool isSelected)
    {
        this.isSelected = isSelected;
        selectedObj.SetActive(isSelected);

        TubeManager.Instance.UpdateBottleSelected(this, isSelected);
    }

    public BottleData GetBottleData()
    {
        BottleData bottleData = new BottleData();

        foreach (var water in waters)
        {
            bottleData.WaterDatas.Add(water.GetWaterData());
        }

        return bottleData;
    }
}
