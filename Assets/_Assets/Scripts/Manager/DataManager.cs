using Newtonsoft.Json;
using SFB;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
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
        File.WriteAllText(FolderPath + level + ".json", /*JsonConvert.SerializeObject(levelData)*/ " ");
        AssetDatabase.Refresh();

        Debug.Log("Save ok");
    }
}
