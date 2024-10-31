using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICountTubeBtn : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;
    [SerializeField] private TMP_Text _text;

    private int _value;
    public int Value
    {
        get
        {
            return _value;
        }
        set
        {
            _text.text = value.ToString();
            _value = value;
        }
    }

    public void Setup(int index)
    {
        Value = index;

        _toggle.group = TubeCountController.Instance.toggleGroup;

        _toggle.onValueChanged.AddListener(isOn =>
        {
            if (isOn)
            {
                TubeManager.Instance.SetupBottle(Value);
            }
        });
    }
}
