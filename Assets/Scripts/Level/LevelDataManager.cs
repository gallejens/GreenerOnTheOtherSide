using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class LevelDataManager : MonoBehaviour
{
    public static LevelDataManager Instance { get; private set; }
    
    public static int LevelNumber = 0;

    public static List<LevelData> levelDataList = new List<LevelData>();

    [SerializeField] private GameObject CreateLevelPrefab;

    private GameObject lastCreateLevelObject;

    private void Awake()
    {
        Instance = this;
        levelDataList = GenericMethods.ReadJSONFile<List<LevelData>>("leveldata");
    }

    public void NewLevel(int levelNumber)
    {
        LevelNumber = levelNumber;
        
        Destroy(lastCreateLevelObject);
        lastCreateLevelObject = Instantiate(CreateLevelPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        lastCreateLevelObject.GetComponent<CreateLevel>().Create(levelDataList[LevelNumber]);
    }

    /// <summary>
    /// Create new JSON File from start
    /// </summary>
    private void CreateNewJSON()
    {
        LevelData levelData1 = new LevelData();
        levelData1.supportCoords = new Vector2[] { new Vector2(-2, -3), new Vector2(-1, -1), new Vector2(-1, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(1, 3) };
        levelData1.platformCoords = new Vector2[][] { new Vector2[] { new Vector2(-1, -3), new Vector2(-1, -2), new Vector2(-1, -1) }, new Vector2[] { new Vector2(-1, -1), new Vector2(-1, 0) } };

        LevelData levelData2 = new LevelData();
        levelData2.supportCoords = new Vector2[] { new Vector2(-2, -3), new Vector2(-2, -2), new Vector2(-2, -1), new Vector2(0, -1), new Vector2(0, 0), new Vector2(0, 1), new Vector2(2, 1), new Vector2(2, 2), new Vector2(2, 3) };
        levelData2.platformCoords = new Vector2[][] { new Vector2[] { new Vector2(-2, -3), new Vector2(-2, -2) }, new Vector2[] { new Vector2(-2, -1), new Vector2(-1, -1), new Vector2(0, -1) }, new Vector2[] { new Vector2(0, 1), new Vector2(1, 1), new Vector2(2, 1) } };

        List<LevelData> levelList = new List<LevelData>();
        levelList.Add(levelData1);
        levelList.Add(levelData2);

        string json = JsonConvert.SerializeObject(levelList, Formatting.Indented);
        File.WriteAllText($"{Application.dataPath}/StreamingAssets/leveldata.json", json);
    }
}
