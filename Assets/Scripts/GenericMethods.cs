using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public static class GenericMethods
{
    /// <summary>
    /// Set all children of provided GameObject to active state defined by bool.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="active"></param>
    public static void SetAllChildrenActive(GameObject go, bool active)
    {
        foreach (Transform child in go.transform)
        {
            child.gameObject.SetActive(active);
        }
    }

    /// <summary>
    /// Reads class from JSON file.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="file"></param>
    /// <returns></returns>
    public static T ReadJSONFile<T>(string file)
    {
        string path = $"{Application.dataPath}/StreamingAssets/{file}.json";

        if (!File.Exists(path))
        {
            File.WriteAllText($"{Application.dataPath}/StreamingAssets/{file}.json", "[\n]");
        }

        string json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<T>(json);
    }

    /// <summary>
    /// Writes class to JSON file.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="file"></param>
    public static void WriteJSONFile<T>(T data, string file)
    {
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText($"{Application.dataPath}/StreamingAssets/{file}.json", json);
    }

    /// <summary>
    /// Return levelnumber of first level that isnt completed starting from provided number.
    /// </summary>
    /// <param name="start"></param>
    /// <returns></returns>
    public static int FindFirstUncompletedLevel(int start)
    {
        for (int i = start; i < LevelDataManager.levelDataList.Count; i++)
        {
            if (!SaveDataManager.LoadedSaveData.Contains(i)) return i;
        }

        return -1;
    }
}


