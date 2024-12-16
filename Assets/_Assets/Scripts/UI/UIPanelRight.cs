
using UnityEngine;
using UnityEngine.UI;

public class UIPanelRight : MonoBehaviour
{
    [SerializeField] private Button showWaterLayerBtn;
    [SerializeField] private Button showTubeBtn;


    private void Start()
    {
        showWaterLayerBtn.onClick.AddListener(OnClickShowWaterLayer);
        showTubeBtn.onClick.AddListener(OnClickShowTube);
    }

    private void OnClickShowWaterLayer()
    {
        bool isShow = ToolManager.Instance.SetShowLayer();
        Color color = isShow ? Color.green : Color.white;
        showWaterLayerBtn.image.color = color;
    }

    private void OnClickShowTube()
    {
        bool isShow = ToolManager.Instance.SetShowTube();
        Color color = isShow ? Color.green : Color.white;
        showTubeBtn.image.color = color;
    }
}
