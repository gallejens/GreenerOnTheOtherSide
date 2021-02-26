using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditMenu : MonoBehaviour
{
    public static CreditMenu Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void BackButtonPressed()
    {
        MainMenu.Instance.Enable();

        Disable();
    }

    public void Enable() => GenericMethods.SetAllChildrenActive(gameObject, true);

    public void Disable() => GenericMethods.SetAllChildrenActive(gameObject, false);
}
