using System.Collections.Generic;
using UnityEngine;

public class CreateLevel : MonoBehaviour
{
    public GameObject[] supports = new GameObject[5]; // 0: start, 1 2 3: middles, 5: end
    public GameObject[] platformGameObjects = new GameObject[3];

    private Vector3 startPos = new Vector3(0, 0.248f, -3);

    public static List<ISupport> SupportList { get; set; } = new List<ISupport>();
    public static List<IPlatform> PlatformList { get; set; } = new List<IPlatform>();

    public void Create(LevelData levelData)
    {
        SupportList.Clear();
        PlatformList.Clear();
        
        CreatePlatforms(levelData.platformCoords);
        CreateSupports(levelData.supportCoords);

        Character.Instance.MoveToStartPos(startPos);
    }

    /// <summary>
    /// Create supports from the JSON leveldata.
    /// </summary>
    /// <param name="levelData"></param>
    private void CreateSupports(Vector2[] supportsCoords)
    {
        // iterate through the list and instantiate a support on the pos
        foreach (Vector2 coords in supportsCoords)
        {
            GameObject supportToUse;
            Vector3 position = Vector3.zero;
            Vector3 rotation = Vector3.zero;

            if (coords.y == startPos.z)
            {
                startPos += new Vector3(coords.x, 0, 0);
                supportToUse = supports[0];
                position = supportToUse.transform.position + new Vector3(coords.x, 0, 0); // we add with the pos of prefab to keep nice placement in world
            }
            else if (coords.y == 3)
            {
                supportToUse = supports[4];
                position = supportToUse.transform.position + new Vector3(coords.x, 0, 0);
            }
            else
            {
                supportToUse = supports[Random.Range(1, supports.Length - 1)]; // avoid first and last 
                position = new Vector3(coords.x, supportToUse.transform.position.y, coords.y);
                rotation = new Vector3(0, Random.Range(0, 5) * 90, 0);   
            }

            ISupport createdSupport = (Instantiate(supportToUse, position, Quaternion.Euler(rotation), transform) as GameObject).GetComponent<ISupport>();
            createdSupport.Position = coords;
            SupportList.Add(createdSupport);
        }
    }
    
    /// <summary>
    /// Create platforms from the JSON leveldata.
    /// </summary>
    /// <param name="platforms"></param>
    private void CreatePlatforms(Vector2[][] platformPointCoords)
    {
        // iterate through the list, choose platform based on length, assign rotation based on orientation and calc middle of platform as pos to instantiate
        // then get the iplatform from the object and set its points
        foreach (Vector2[] pointCoords in platformPointCoords)
        {
            int size = (int)Vector2.Distance(pointCoords[0], pointCoords[1]);
            GameObject platformToUse = platformGameObjects[size - 1];
            Vector3 rotation = new Vector3(0, pointCoords[0].x == pointCoords[1].x ? 90 : 0, 0);
            Vector3 position = new Vector3((pointCoords[0].x + pointCoords[1].x) / 2, platformToUse.transform.position.y, (pointCoords[0].y + pointCoords[1].y) / 2); // average begin and end point for x and y and height of the prefab

            IPlatform createdPlatform = (Instantiate(platformToUse, position, Quaternion.Euler(rotation), transform) as GameObject).GetComponent<IPlatform>();
            PlatformList.Add(createdPlatform);
            createdPlatform.Points = new Vector2[size + 1];

            createdPlatform.Size = size;

            for (int i = 0; i < size + 1; i++)
            {
                createdPlatform.Points[i] = pointCoords[0] + ((float)i / size) * (pointCoords[1] - pointCoords[0]);
            }
        }
    }
}
