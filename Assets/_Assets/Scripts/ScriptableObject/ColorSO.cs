using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ColorSO")]
public class ColorSO : ScriptableObject
{
    public List<ColorData> listColorData;

    public Color GetColor(EColor eColor)
    {
        ColorData colorData = listColorData.Find(x => x.eColor == eColor);
        return colorData.color;
    }

    private void Reset()
    {
        var colors = Enum.GetValues(typeof(EColor));
        listColorData = new List<ColorData>();

        foreach (var color in colors)
        {
            ColorData colorData = new ColorData();
            colorData.eColor = (EColor)color;
            colorData.color.a = 1f;

            listColorData.Add(colorData);
        }
    }
}

[Serializable]
public struct ColorData
{
    public EColor eColor;
    public Color color;
}

