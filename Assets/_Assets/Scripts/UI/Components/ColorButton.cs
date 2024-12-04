using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{
    [Header("Components Inspector")]
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI numText;
    [SerializeField] private GameObject selectedObject;

    public bool isSelected;
    private EColor eColor;
    private int numUsed;

    private void Start()
    {
        button.onClick.AddListener(()  => { SetSelectedColor(!isSelected); });
    }

    public void SetData(ColorData colorData)
    {
        if (colorData.eColor == EColor.None)
        {
            Debug.Log("None Color");
        }

        Debug.Log("other color");

        numUsed = 0;
        eColor = colorData.eColor;
        button.image.color = colorData.color;

        SetSelectedColor(false);
    }

    public void SetSelectedColor(bool isSelected)
    {
        this.isSelected = isSelected;
        selectedObject.SetActive(isSelected);

        ColorPickerController.OnPickColor?.Invoke(eColor, isSelected);
    }

    public void UpdateNumUsed(int changedValue)
    {
        numUsed += changedValue;
        numText.text = numUsed.ToString();
    }

    public void ResetNumWater()
    {
        numUsed = 0;
        numText.text = numUsed.ToString();
    }
}
