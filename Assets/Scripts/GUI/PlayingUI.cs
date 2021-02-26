using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingUI : MonoBehaviour
{
    public static PlayingUI Instance { get; private set; }

    public static bool IsEnabled { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause") && IsEnabled)
        {
            Disable();
            PauseMenu.Instance.Enable();
        }
    }

    public void Enable(int levelNumber)
    {
        CameraMovement.Instance.enabled = true;
        CameraRotate.Instance.enabled = false;

        GenericMethods.SetAllChildrenActive(gameObject, true);

        LevelDataManager.Instance.NewLevel(levelNumber);

        IsEnabled = true;
    }

    public void Enable()
    {
        GenericMethods.SetAllChildrenActive(gameObject, true);

        IsEnabled = true;
    }

    public void Disable()
    {
        GenericMethods.SetAllChildrenActive(gameObject, false);

        IsEnabled = false;
    }
}
