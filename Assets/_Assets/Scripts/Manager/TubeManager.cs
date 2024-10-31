using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeManager : SingletonBase<TubeManager>
{
    [SerializeField] private Bottle bottlePrefab;
    [SerializeField] private Transform bottleContainer;

    [SerializeField] private Beardy.GridLayoutGroup gridLayoutGroup;
    [SerializeField] private ArrangeSO arrangeDataSO;
    [SerializeField] private Transform scaleParent;

    public List<Bottle> bottles = new();

    public void CreateBottle(BottleData data)
    {
        var bottle = Instantiate(bottlePrefab, bottleContainer);

        bottle.Init(data);

        bottles.Add(bottle);
    }

    public void SetupBottle(int num)
    {
        int cur = bottles.Count;

        while (cur++ < num)
        {
            CreateBottle(new BottleData(4));
        }

        for (int i = 0; i < bottles.Count; i++)
        {
            bottles[i].gameObject.SetActive(i < num);
        }

        Arrange(num);
    }

    private void Arrange(int num)
    {
        var data = arrangeDataSO.GetDataAt(num);

        if (data == null) return;

        gridLayoutGroup.constraintCount = data.Row;
        scaleParent.localScale = Vector3.one * data.Scale / 2f;
        gridLayoutGroup.spacing = data.Spacing;
    }
}
