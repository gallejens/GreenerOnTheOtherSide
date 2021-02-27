using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PauseMenu : MonoBehaviour, IOptionsBack
{
    public static PauseMenu Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void ResumeButtonPressed()
    {
        Disable();
        PlayingUI.Instance.Enable();
    }

    public void RestartButtonPressed()
    {
        Disable();
        PlayingUI.Instance.Enable(LevelDataManager.LevelNumber);
    }

    public void OptionsButtonPressed()
    {
        Disable();
        OptionsMenu.Instance.Enable(GetComponent<IOptionsBack>());
    }

    public void MenuButtonPressed()
    {
        Disable();
        MainMenu.Instance.Enable();
    }

    public void Enable()
    {
        AudioController.Instance.GamePaused();
        GenericMethods.SetAllChildrenActive(gameObject, true);
    }

    public void Disable() => GenericMethods.SetAllChildrenActive(gameObject, false);
}
