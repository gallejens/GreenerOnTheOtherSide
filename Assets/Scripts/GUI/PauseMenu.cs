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
        PlayingUI.Instance.Enable();
        Disable();
    }

    public void OptionsButtonPressed()
    {
        Disable();
        OptionsMenu.Instance.Enable(GetComponent<IOptionsBack>());
    }

    public void MenuButtonPressed()
    {
        MainMenu.Instance.Enable();
        Disable();
    }

    public void Enable()
    {
        GenericMethods.SetAllChildrenActive(gameObject, true);
    }

    public void Disable()
    {
        GenericMethods.SetAllChildrenActive(gameObject, false);
    }
}
