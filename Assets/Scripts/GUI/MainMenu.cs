using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour, IOptionsBack
{
    public static MainMenu Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LevelDataManager.Instance.NewLevel(0);
    }

    public void PlayButtonPressed()
    {
        Disable();
        PlayingUI.Instance.Enable(GenericMethods.FindFirstUncompletedLevel(1) == -1 ? LevelDataManager.levelDataList.Count - 1 : GenericMethods.FindFirstUncompletedLevel(1));
    }

    public void SelectButtonPressed()
    {
        Disable();
        SelectMenu.Instance.Enable();
    }

    public void OptionsButtonPressed()
    {
        Disable();
        OptionsMenu.Instance.Enable(GetComponent<IOptionsBack>());
    }

    public void CreditsButtonPressed()
    {
        Disable();
        CreditMenu.Instance.Enable();
    }

    public void QuitButtonPressed()
    {
        Application.Quit();
    }

    public void HelpButtonPressed()
    {
        Disable();
        HelpMenu.Instance.Enable();
    }

    public void Enable()
    {
        CameraMovement.Instance.enabled = false;
        CameraRotate.Instance.enabled = true;

        LevelDataManager.Instance.NewLevel(0);

        GenericMethods.SetAllChildrenActive(gameObject, true);
    }

    public void Disable() => GenericMethods.SetAllChildrenActive(gameObject, false);
}
