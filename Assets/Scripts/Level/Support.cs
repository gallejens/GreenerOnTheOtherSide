using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Support : MonoBehaviour, ISelectable, ISupport
{
    [SerializeField] private Material hoveringMaterial = null;
    private Material normalMaterial;

    private Renderer rd;
    private Transform tf;

    private Vector2 position;
    public Vector2 Position { get; set; } = Vector2.zero;

    private bool hovering;
    public bool Hovering { get; set; } = false;

    private void Start()
    {
        rd = GetComponent<Renderer>();
        tf = transform;

        normalMaterial = GetComponent<MeshRenderer>().material;
    }

    /// <summary>
    /// Changes material on call depending on current state.
    /// </summary>
    /// <param name="selected"></param>
    public void Hover(bool selected)
    {
        try
        {
            if (selected)
            {
                rd.material = hoveringMaterial;
                Hovering = true;
            }
            else
            {
                rd.material = normalMaterial;
                Hovering = false;
            }
        }
        catch (MissingReferenceException e)
        {
            Debug.Log(e);
        }
    }

    /// <summary>
    /// If this support gets clicked the move function of the character gets called provided with the supports position
    /// </summary>
    public void Clicked() => Character.Instance.ClickedSupport(Position);
}
