using System.Collections.Generic;
using UnityEngine;

public class TubeManager : SingletonBase<TubeManager>
{
    [SerializeField] private Bottle bottlePrefab;
    [SerializeField] private Transform bottleContainer;
    [SerializeField] private Transform poolContainer;

    [SerializeField] private Beardy.GridLayoutGroup gridLayoutGroup;
    [SerializeField] private ArrangeSO arrangeDataSO;
    [SerializeField] private Transform scaleParent;

    private Queue<Bottle> poolBottles = new();
    public List<Bottle> bottles = new();

    public Bottle bottleSelected;

    public void CreateBottle(TubeData data)
    {
        var bottle = Instantiate(bottlePrefab, bottleContainer);

        bottle.Init(data);

        poolBottles.Enqueue(bottle);
    }

    public void LoadBottles(List<TubeData> listBottleData)
    {
        SetupBottles(0);// Clear All bottles

        ToolManager.Instance.colorController.ResetCounterColor();

        foreach (var bottleData in listBottleData)
        {
            AddBottle(bottleData);
        }
    }

    public void SetupBottles(int num)
    {
        int countBottles = bottles.Count;
        if (num < bottles.Count)
        {
            while (num != countBottles)
            {
                RemoveBottle();
                countBottles--;
            }
        }
        else
        {
            while(num != countBottles)
            {
                AddBottle();
                countBottles++;
            }
        }

        Arrange(num);
    }

    public void AddBottle(TubeData bottleData = null)
    {
        if (bottles.Count >= 18)
            return;

        if (poolBottles.Count == 0)
        {
            CreateBottle(new TubeData(bottles.Count));
        }
        Bottle bottle = poolBottles.Dequeue();
        bottle.transform.SetParent(bottleContainer);
        bottle.gameObject.SetActive(true);

        if (bottleData != null) 
            bottle.Init(bottleData);
        else
            bottle.Init(new TubeData(bottles.Count));

        bottles.Add(bottle);

        Arrange(bottles.Count);
    }

    public void RemoveBottle(Bottle bottle = null)
    {
        if (bottles.Count <= 0)
            return;

        if (bottle == null)
            bottle = bottles[bottles.Count - 1];

        bottle.OnRemove();
        bottle.transform.SetParent(poolContainer);
        bottle.gameObject.SetActive(false);
        poolBottles.Enqueue(bottle);

        bottles.Remove(bottle);

        Arrange(bottles.Count);
    }

    private void Arrange(int num)
    {
        var data = arrangeDataSO.GetDataAt(num);

        if (data == null) return;

        gridLayoutGroup.constraintCount = data.Row;
        scaleParent.localScale = Vector3.one * data.Scale / 2f;
        gridLayoutGroup.spacing = data.Spacing;
    }

    public void UpdateBottleSelected(Bottle bottle, bool isSelected)
    {
        if (!isSelected)
        {
            bottleSelected = null;
            return;
        }

        if (HotKeyManager.Instance.IsHoldShift && bottleSelected)
        {
            SwapBottle(bottle, bottleSelected);
        }

        bottleSelected?.SelectTube(false);
        bottleSelected = bottle;
    }

    private void SwapBottle(Bottle bottle1, Bottle bottle2)
    {
        TubeData tubeData1 = bottle1.GetBottleData();
        TubeData tubeData2 = bottle2.GetBottleData();

        bottle1.OnRemove();
        bottle2.OnRemove();

        bottle1.Init(tubeData2);
        bottle2.Init(tubeData1);
    }

    public void RandomColor(int numColor)
    {
        for (int i = 0; i < numColor; i++)
        {
            bottles[i].SetRandomColor();
        }
    }

    public void ClearColorTubeSelected()
    {
        if (bottleSelected != null)
        {
            bottleSelected.OnRemove();
        }
    }

    public void GetBottlesData(ref List<TubeData> bottleDatas)
    {
        foreach (var bottle in bottles)
        {
            bottleDatas.Add(bottle.GetBottleData());
        }
    }

    public void ClickClearAll()
    {
        foreach (var bottle in bottles)
        {
            bottle.OnRemove();
        }
    }
}
