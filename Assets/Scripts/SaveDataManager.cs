using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

public class SaveDataManager : MonoBehaviour
{
    public static HashSet<int> LoadedSaveData = new HashSet<int>();
    private int previousCount;

    private void Awake()
    {
        LoadedSaveData = GenericMethods.ReadJSONFile<HashSet<int>>("savedata");
        previousCount = LoadedSaveData.Count;
    }

    public void Update()
    {
        if (previousCount != LoadedSaveData.Count)
        {
            GenericMethods.WriteJSONFile(LoadedSaveData, "savedata");
            previousCount = LoadedSaveData.Count;
        }
    }
}
