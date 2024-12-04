using Newtonsoft.Json;
using SFB;
using System.IO;
using TMPro;
using UnityEngine;

public class DataManager : SingletonBase<DataManager>
{
    [SerializeField] private TMP_Text folderPath;

    public string FolderPath => folderPath.text + "/";

    #region Select Folder
    public void SelectFolder()
    {
        var paths = StandaloneFileBrowser.OpenFolderPanel("Chọn thư mục", "", false);

        if (paths.Length > 0)
        {
            folderPath.text = paths[0];
            Debug.Log("Đường dẫn thư mục: " + folderPath);
        }
    }
    #endregion


    #region Save LevelData
    public void CheckLevelExistsToSave()
    {
        int level;

        if (!int.TryParse(ToolManager.Instance.uiPanelLeft.levelInputField.text, out level))
        {
            NotifyControl.Instance.Notify("Chưa nhập level");
            return;
        }

        TextAsset textAsset = null;

        if (!Directory.Exists(FolderPath))
            Directory.CreateDirectory(FolderPath);

        if (File.Exists(FolderPath + level + ".json"))
        {
            textAsset = new TextAsset(File.ReadAllText(FolderPath + level + ".json"));
        }

        if (textAsset != null)
        {
            Debug.Log("1");
            NotifyControl.Instance.NotifyConfirm("Ghi đè level " + level + " ?", () => { SaveData(level); });
            return;
        }
        else
        {
            SaveData(level);
        }
    }

    public void SaveData(int level)
    {
        // Init Level Data
        LevelData levelData = new LevelData();
        levelData.isFree = ToolManager.Instance.isFreeMap;
        levelData.timeLimit = ToolManager.Instance.timeLimit;
        levelData.difficulty = ToolManager.Instance.difficulty;

        if(levelData.timeLimit <= 0)
        {
            NotifyControl.Instance.Notify("Vui lòng nhập thời gian hợp lệ");
            return;
        }

        TubeManager.Instance.GetBottlesData(ref levelData.listTubeData);

        //Save
        string json = JsonConvert.SerializeObject(levelData);

        File.WriteAllText(FolderPath + level + ".json", json);

        NotifyControl.Instance.Notify($"Lưu thành công level {level}");
    }

    #endregion

    #region Load LevelData
    public void LoadLevelData(int level)
    {
        string levelPath = FolderPath + level + ".json";
        if (!File.Exists(levelPath))
        {
#if use_notify_console
            NotifyControl.Instance.NotifyConsole($"Không tồn tại level {level}");
#endif
            return;
        }

        string json = File.ReadAllText(levelPath);
        LevelData levelData = JsonConvert.DeserializeObject<LevelData>(json);

        ToolManager.Instance.LoadData(levelData);
        TubeManager.Instance.LoadBottles(levelData.listTubeData);
    }
    #endregion
}
