using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData
{
    public int levelNumber; // 0 is the mainmenu showcase, this is just for easy readability of the json file
    public Vector2[] supportCoords;
    public Vector2[][] platformCoords; //first one is first coords, second one is second coords
}
