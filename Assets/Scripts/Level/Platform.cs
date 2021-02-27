using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour, ISelectable, IPlatform
{
    [SerializeField] private Material hoveringMaterial = null;
    [SerializeField] private Material clickedMaterial = null;
    private Material normalMaterial;
    private Renderer rd;
    private Transform tf;

    // The first vector in Points is the starting pos of support, last vector is the ending pos of support, the ones in between are places the platform is using but theres no support
    private Vector2[] points;
    public Vector2[] Points { get; set; }

    private bool playerIsHolding = false;

    private int size;
    public int Size { get; set; } = 0;

    private Vector2[] tempPlacePoints;

    private void Start()
    {
        rd = GetComponent<MeshRenderer>();
        tf = transform;

        normalMaterial = rd.material;
        clickedMaterial.renderQueue = 3100; // transparency bug fix

        tempPlacePoints = new Vector2[Size + 1];
    }

    private void Update()
    {
        if (PlayingUI.IsEnabled)
        {
            if (playerIsHolding)
            {
                for (int i = 0; i < Points.Length; i++)
                {
                    Points[i] = Vector2.positiveInfinity;
                }

                (int, Vector2) angle = MouseSelection.Instance.FindNewPlatformOrientation();

                tempPlacePoints[0] = new Vector2(Character.Instance.Position.x, Character.Instance.Position.y);

                for (int i = 0; i < tempPlacePoints.Length; i++)
                {
                    tempPlacePoints[i] = tempPlacePoints[0] + angle.Item2 * i;
                }

                tf.position = new Vector3((tempPlacePoints[0].x + tempPlacePoints[Size].x) / 2, tf.position.y, (tempPlacePoints[0].y + tempPlacePoints[Size].y) / 2);
                tf.localEulerAngles = new Vector3(0, angle.Item1, 0);     
            }
        }         
    }

    private bool hovering;
    public bool Hovering { get; set; } = false;

    public void Hover(bool selected)
    {
        if (!playerIsHolding)
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
    }

    /// <summary>
    /// When this platform gets clicked and the player is carrying no other platforms, this platform will be the one used
    /// </summary>
    public void Clicked()
    {
        if (Character.Instance.HoldingPlatform == null)
        {
            if ((Points[0].x == Character.Instance.Position.x && Points[0].y == Character.Instance.Position.y) || (Points[Size].x == Character.Instance.Position.x && Points[Size].y == Character.Instance.Position.y))
            {
                playerIsHolding = true;
                Character.Instance.HoldingPlatform = GetComponent<IPlatform>();
                GetComponent<BoxCollider>().enabled = false;
                rd.material = clickedMaterial;
                tf.position = new Vector3(tf.position.x, tf.position.y + 0.3f, tf.position.z);

                AudioController.Instance.PlatformPicked();
            }
        }
    }

    /// <summary>
    /// When this platform is being held and select gets clicked it will check if theres a support on the other side and then place down.
    /// </summary>
    public void PlacePlatform()
    {
        bool endPosOnSupport = false;

        // go through each support to check if there are any the platform is overlapping and if theres a support at the end of the platform
        foreach (ISupport support in CreateLevel.SupportList)
        {
            // check if theres any of the middle points of the platform overlapping with a support
            for (int i = 1; i < Size; i++)
            {
                if (tempPlacePoints[i] == support.Position)
                {
                    return;
                }
            }

            if (tempPlacePoints[Size] == support.Position)
            {
                endPosOnSupport = true;
            }
        }

        // check if the platform has a size bigger than 1 so it may overlap with other platforms
        // go through each platform and check if any of their middle points overlap with any of our middle points depending on the sizes
        if (Size > 1)
        {
            foreach (IPlatform p in CreateLevel.PlatformList)
            {
                for (int i = 1; i < Size; i++)
                {
                    for (int j = 1; j < p.Size; j++)
                    {
                        if (tempPlacePoints[i] == p.Points[j])
                        {
                            return;
                        }
                    }
                }
            }
        }

        // if theres a support at the end place the platform by setting its temppoints to the real ones so we can walk over and re enabling its collider so we can select it
        if (endPosOnSupport)
        {
            playerIsHolding = false;
            Character.Instance.HoldingPlatform = null;
            GetComponent<BoxCollider>().enabled = true;
            rd.material = normalMaterial;

            for (int i = 0; i < tempPlacePoints.Length; i++)
            {
                Points[i] = tempPlacePoints[i];
            }

            tf.position = new Vector3(tf.position.x, tf.position.y - 0.3f, tf.position.z);

            AudioController.Instance.PlatformPlaced();
        }
    } 
}
