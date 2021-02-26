using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishMenu : MonoBehaviour
{
    public static FinishMenu Instance { get; private set; }

    private bool arrivedAtFinish = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Character.Instance.Position.y == 3 && !arrivedAtFinish)
        {
            PlayingUI.Instance.Disable();
            Enable();
            arrivedAtFinish = true;
        }
    }

    public void NextButtonPressed()
    {
        PlayingUI.Instance.Enable(GenericMethods.FindFirstUncompletedLevel(LevelDataManager.LevelNumber));
        Disable();
    }

    public void ReplayButtonPressed()
    {
        PlayingUI.Instance.Enable(LevelDataManager.LevelNumber);
        Disable();
    }

    public void MenuButtonPressed()
    {
        MainMenu.Instance.Enable();
        Disable();
    }

    private void Enable()
    {
        GenericMethods.SetAllChildrenActive(gameObject, true);

        SaveDataManager.LoadedSaveData.Add(LevelDataManager.LevelNumber);
    }

    public void Disable() 
    { 
        GenericMethods.SetAllChildrenActive(gameObject, false);
        arrivedAtFinish = false;
    }
}
