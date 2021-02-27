using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MouseSelection : MonoBehaviour
{
    public static MouseSelection Instance { get; private set; }

    private Camera cam;

    private ISelectable selected;
    private ISelectable lastSelected;
    public HashSet<ISelectable> selectableList = new HashSet<ISelectable>(); // list of selected objects to reset texture after hovering

    private BoxCollider platformPlacementCollider;

    private bool moved;
    public bool Moved { get; set; } = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        cam = Camera.main;
        platformPlacementCollider = GameObject.Find("PlatformPlacement").GetComponent<BoxCollider>();

        selected = FindSelected();
        lastSelected = selected; // set lastselected to selected so we can check the ifstatement on first update call otherwise it will be a null error
    }

    private void Update()
    {
        if (PlayingUI.IsEnabled)
        {
            selected = FindSelected();
            selectableList.Add(selected);

            // selected will be null if we dont select anything, we check if the same as last selected for when we go from object to object
            if (selected != null && lastSelected == selected)
            {
                if (!selected.Hovering) selected.Hover(true);

                if (Input.GetButtonDown("Select"))
                {
                    SelectButtonClicked();
                }
            }
            else
            {
                foreach (ISelectable selectable in selectableList)
                {
                    if (selectable?.Hovering ?? false)
                    {
                        selectable.Hover(false);
                    }
                }

                if (Input.GetButtonDown("Select"))
                {
                    Character.Instance.HoldingPlatform?.PlacePlatform();
                }
            }

            lastSelected = selected;
        }
    }

    /// <summary>
    /// Handles logic when select button gets clicked
    /// </summary>
    private void SelectButtonClicked()
    {
        if (selected is ISupport)
        {
            selected.Clicked();

            if (!Moved) Character.Instance.HoldingPlatform?.PlacePlatform();

            Moved = false;
        }
        else
        {
            Character.Instance.HoldingPlatform?.PlacePlatform();
            selected.Clicked();
        }
    }

    /// <summary>
    /// Find the currently selected ISelectable
    /// </summary>
    /// <returns></returns>
    private ISelectable FindSelected()
    {
        platformPlacementCollider.enabled = false;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1 << 9))
        {
            platformPlacementCollider.enabled = true;
            Transform selection = hit.transform;
            return selection.GetComponent<ISelectable>();
        }

        platformPlacementCollider.enabled = true;
        return null;
    }

    /// <summary>
    /// Finds orientation of platform (Used in update of Platform script)
    /// </summary>
    /// <returns></returns>
    public (int, Vector2) FindNewPlatformOrientation()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1 << 8))
        {
            Vector2 hitpoint = new Vector2(hit.point.x, hit.point.z) - new Vector2(Character.Instance.transform.position.x, Character.Instance.transform.position.z);

            float angle = Mathf.Acos(hitpoint.normalized.x) * Mathf.Rad2Deg;

            if (hitpoint.y < 0)
            {
                angle *= -1;
            }

            if (angle < -135)
            {
                return (0, Vector2.left);
            }
            else if (angle < -45)
            {
                return (90, Vector2.down);
            }
            else if (angle < 45)
            {
                return (0, Vector2.right);
            }
            else if (angle < 135)
            {
                return (90, Vector2.up);
            }
            else
            {
                return (0, Vector2.left);
            }
        }

        return (0, Vector2.right);
    }

    /// <summary>
    /// Find orientation to change characters rotation.
    /// </summary>
    /// <returns></returns>
    public float FindNewCharacterOrientation()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1 << 8))
        {
            Vector2 hitpoint = new Vector2(hit.point.x, hit.point.z) - new Vector2(Character.Instance.transform.position.x, Character.Instance.transform.position.z);

            float angle = Mathf.Acos(hitpoint.normalized.x) * Mathf.Rad2Deg;

            if (hitpoint.y > 0)
            {
                angle *= -1;
            }

            return angle + 90f;
        }

        return 0;
    }
}
