using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class Menu
{
    [MenuItem("NamTT/Convert Level")]
    public static void ConvertLevel()
    {
        Object[] objectArray = Selection.objects;

        foreach (var obj in objectArray)
        {
            var jsonData = JsonConvert.DeserializeObject<Dictionary<string, int[]>>(obj.ToString());

            if (!jsonData.ContainsKey("ids"))
                continue;

            var bottleDatas = jsonData["ids"];

            LevelData levelData = new();
            levelData.isFree = false;

            int id = 0;

            for (int i = 0; i < bottleDatas.Length; i += 4)
            {
                BottleData bottleData = new BottleData(id++);

                for (int j = 3; j >= 0; j--)
                {
                    bottleData.WaterDatas[3 - j].eColor = (EColor)bottleDatas[i + j];
                }

                levelData.BottleDatas.Add(bottleData);
            }

            var objSplit = obj.name.Split("_");

            File.WriteAllText(Application.dataPath + $"/Resources/LevelConvertTemp/{objSplit[objSplit.Length - 1]}.json", JsonConvert.SerializeObject(levelData));
            Debug.Log("Save Successfully: " + obj.name);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
