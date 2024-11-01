using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelLeft : MonoBehaviour
{
    [Header("Save Load Components")]
    public TMP_InputField levelInputField;
    [SerializeField] private Button saveBtn;
    [SerializeField] private Button loadBtn;
    [SerializeField] private Button nextLevelBtn;
    [SerializeField] private Button prevLevelBtn;

    [Header("Other")]
    [SerializeField] private Button freeMapBtn;
    [SerializeField] private TextMeshProUGUI textStateFreeMap;

    private void Awake()
    {
        saveBtn.onClick.AddListener(() =>
        {
            DataManager.Instance.CheckLevelExistsToSave();
        });

        loadBtn.onClick.AddListener(OnClickLoadLevel);

        freeMapBtn.onClick.AddListener(OnClickFreeMap);

        nextLevelBtn.onClick.AddListener(() => OnClickLevelTrans(1));
        prevLevelBtn.onClick.AddListener(() => OnClickLevelTrans(-1));
    }

    private void OnClickLoadLevel()
    {
        int level;
        if (!int.TryParse(ToolManager.Instance.uiPanelLeft.levelInputField.text, out level))
        {
            NotifyControl.Instance.Notify("Chưa nhập level");
            return;
        }

        DataManager.Instance.LoadLevelData(level);
    }

    private void OnClickLevelTrans(int valueChange)
    {
        int level;
        int.TryParse(levelInputField.text, out level);

        level += valueChange;
        if (level <= 0) level = 1;

        levelInputField.text = level.ToString();
        DataManager.Instance.LoadLevelData(level);
    }

    private void OnClickFreeMap()
    {
        if (ToolManager.Instance.UpdateFreeMap())
        {
            textStateFreeMap.text = "FreeMap: On";
            freeMapBtn.image.color = Color.green;
        }
        else
        {
            textStateFreeMap.text = "FreeMap: Off";
            freeMapBtn.image.color = Color.white;
        }
    }
}
