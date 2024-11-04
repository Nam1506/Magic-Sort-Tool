using UnityEngine.UI;
using UnityEngine;
using System;

public class HiddenTubePanel : MonoBehaviour
{
    [SerializeField] Transform tfColorContainer;
    [SerializeField] Button cancelButton;
    public Action<ColorData> OnPickColorHiddenTube;

    private void Start()
    {
        cancelButton.onClick.AddListener(() => gameObject.SetActive(false));

        foreach (var colorData in ToolManager.Instance.colorSO.listColorData)
        {
            if (colorData.eColor == EColor.None)
                continue;

            ColorData cdata = colorData;

            var go = Instantiate(new GameObject(colorData.eColor.ToString()), tfColorContainer);
            Button button = go.AddComponent<Button>();
            Image image = go.AddComponent<Image>();

            button.image = image;

            button.image.color = colorData.color;
            button.onClick.AddListener(() => OnClickColor(cdata));
        }
    }

    private void OnClickColor(ColorData colorData)
    {
        OnPickColorHiddenTube?.Invoke(colorData);
        gameObject.SetActive(false);
    }
}
