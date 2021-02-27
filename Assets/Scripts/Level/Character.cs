using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Character : MonoBehaviour
{
    public static Character Instance { get; private set; } // singleton for global access
    
    private Transform tf;

    private IPlatform holdingPlatform;
    public IPlatform HoldingPlatform { get; set; } = null;

    private Vector2 position;
    public Vector2 Position { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        tf = transform;
    }

    private void Update()
    {
        if (PlayingUI.IsEnabled)
        {
            tf.eulerAngles = new Vector3(0, MouseSelection.Instance.FindNewCharacterOrientation(), 0);
        }   
    }

    /// <summary>
    /// Gets called when user clicks a support
    /// </summary>
    public void ClickedSupport(Vector2 supportPosition)
    {
        foreach (IPlatform platform in CreateLevel.PlatformList)
        {
            int length = platform.Size;
            
            // Check if the first point is the same as either the playerposition or target position if so check if the second coord is also the same as one of those
            if ((platform.Points[0].x == tf.position.x && platform.Points[0].y == tf.position.z) || (platform.Points[0].x == supportPosition.x && platform.Points[0].y == supportPosition.y))
            {
                if ((platform.Points[length].x == tf.position.x && platform.Points[length].y == tf.position.z) || (platform.Points[length].x == supportPosition.x && platform.Points[length].y == supportPosition.y))
                {
                    Position = new Vector2(supportPosition.x, supportPosition.y);
                    tf.position = new Vector3(Position.x, tf.position.y, Position.y);
                    MouseSelection.Instance.Moved = true;

                    AudioController.Instance.SupportClicked();

                    return;
                }
            }
        }

        MouseSelection.Instance.Moved = false;
    }

    /// <summary>
    /// Moves the character to its start position. Gets called when creating level.
    /// </summary>
    public void MoveToStartPos(Vector3 startPos)
    {
        Position = new Vector2(startPos.x, startPos.z);
        tf.position = startPos;
    }
}
