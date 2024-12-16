using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorPickerController : MonoBehaviour
{
    public static Action<EColor, bool> OnPickColor;
    public static Action<EColor, int> OnChangeWaterColor;
    public static Action OnResetColor;

    [Header("Data")]
    public ColorSO colorSO;

    [Header("UI")]
    [SerializeField] private Transform colorHozContainer;
    [SerializeField] private ColorButton colorButtonPrefabs;
    [Space]
    [SerializeField] private TextMeshProUGUI textNumUnFill;
    [SerializeField] private Button deselectAllBtn;

    private Dictionary<EColor, ColorButton> dictColorButtons;
    public List<EColor> listColorSelected;
    private Dictionary<EColor, int> counterColorUsed;

    private void Start()
    {
        dictColorButtons = new Dictionary<EColor, ColorButton>();
        listColorSelected = new List<EColor>();
        counterColorUsed = new Dictionary<EColor, int>();

        deselectAllBtn.onClick.AddListener(DeselectAllColor);

        // init list button color
        foreach(ColorData colorData in colorSO.listColorData)
        {
            ColorButton colorButton = Instantiate(colorButtonPrefabs, colorHozContainer);

            colorButton.SetData(colorData);
            dictColorButtons.Add(colorData.eColor, colorButton);

            if (colorData.eColor == EColor.None)
            {
                textNumUnFill = colorButton.GetComponentInChildren<TextMeshProUGUI>();
                textNumUnFill.color = Color.black;
            }
        }
    }

    private void DeselectAllColor()
    {
        foreach(var item in dictColorButtons)
        {
            item.Value.SetSelectedColor(false);
        }
    }

    public EColor GetEColor()
    {
        int numColor = listColorSelected.Count;

        if (numColor == 0)
            return EColor.None;

        int indexRand = UnityEngine.Random.Range(0, numColor);

        return listColorSelected[indexRand];
    }

    public void ResetAllColorWater()
    {
        OnResetColor?.Invoke();
    }

    public void ClearTubeSelected()
    {
        TubeManager.Instance.ClearColorTubeSelected();
    }

    public void RandomAllColor()
    {
        if (listColorSelected.Count > TubeManager.Instance.bottles.Count)
        {
            NotifyControl.Instance.Notify("Số màu được chọn lớn hơn cho phép");
            return;
        }

        counterColorUsed.Clear();
        ResetAllColorWater();

        foreach(var item in listColorSelected)
        {
            counterColorUsed.Add(item, 0);
        }

        TubeManager.Instance.RandomColor(listColorSelected.Count);
    }

    public EColor GetEColorRandom()
    {
        while (counterColorUsed.Count > 0)
        {
            int indexRand = UnityEngine.Random.Range(0, counterColorUsed.Count);
            var item = counterColorUsed.ElementAt(indexRand);
            if (item.Value < 4)
            {
                counterColorUsed[item.Key]++;
                return item.Key;
            }
            else
            {
                counterColorUsed.Remove(item.Key);
            }
        }
        return EColor.None;
    }
    
    private void UpdateColorPicked(EColor eColor, bool isSelected)
    {
        if (!isSelected)
        {
            listColorSelected.Remove(eColor);
            return;
        }

        if (!listColorSelected.Contains(eColor))
        {
            listColorSelected.Add(eColor);
        }

        if (HotKeyManager.Instance.IsHoldShift)
            return;

        foreach (var item in dictColorButtons)
        {
            if (item.Key == eColor)
                continue;

            item.Value.SetSelectedColor(false);
        }
    }

    public void ResetCounterColor()
    {
        foreach (var item in dictColorButtons)
        {
            item.Value.ResetNumWater();
        }
    }

    private void UpdateCounterColor(EColor eColor, int changedValue)
    {
        dictColorButtons[eColor].UpdateNumUsed(changedValue);
    }

    private void OnEnable()
    {
        OnPickColor += UpdateColorPicked;
        OnChangeWaterColor += UpdateCounterColor;
    }

    private void OnDisable()
    {
        OnPickColor -= UpdateColorPicked;
        OnChangeWaterColor -= UpdateCounterColor;
    }
}
