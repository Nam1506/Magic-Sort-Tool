using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TubeCountController : SingletonBase<TubeCountController>
{
    [SerializeField] private UICountTubeBtn uiCountTubeBtnPrefab;
    public ToggleGroup toggleGroup;

    private const int MAX_TUBE = 17;

    private void Start()
    {
        SetupListCountTubeBtn();
    }

    private void SetupListCountTubeBtn()
    {
        for (int i = 0; i < MAX_TUBE; i++)
        {
            var btn = Instantiate(uiCountTubeBtnPrefab, this.transform);

            btn.Setup(i);
        }
    }
}
