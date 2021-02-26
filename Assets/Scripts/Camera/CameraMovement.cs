using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement Instance { get; private set; }
    
    private Transform pivotTransform;
    private Vector2 mouseMovement;

    public static float MouseSensitivity;
    private const float zoomSensitivity = 2f;

    private const int minAngle = 30; //30
    private const int maxAngle = 85;
    private const int minFov = 25;
    private const int maxFov = 55;
    private const int maxCamDistance = -15;
    private const int minCamDistance = -30; // -30

    private Camera cam;
    private Transform tf;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        cam = Camera.main;
        tf = transform;
        pivotTransform = transform.parent;

        transform.localPosition = new Vector3(0, 0, maxCamDistance);
        cam.fieldOfView = maxFov;

        pivotTransform.rotation = Quaternion.Euler(35, 0, 0); // set beginning pos
        pivotTransform.position = Vector3.zero;
    }

    private void LateUpdate()
    {
        if (PlayingUI.IsEnabled)
        {
            if (Input.GetButton("MoveCamera"))
            {
                mouseMovement.x += Input.GetAxis("Mouse X") * MouseSensitivity;
                mouseMovement.y -= Input.GetAxis("Mouse Y") * MouseSensitivity; // minus to invert

                mouseMovement.y = Mathf.Clamp(mouseMovement.y, (float)minAngle, (float)maxAngle); // clamp so it doesnt go below ground or upside down

                pivotTransform.rotation = Quaternion.Euler(mouseMovement.y, mouseMovement.x, 0); // quaternion to avoid gimbal lock       
            }

            float scroll = Input.GetAxis("Zoom");
            if (tf.localPosition.z == maxCamDistance)
            {
                cam.fieldOfView = Mathf.Clamp(cam.fieldOfView -= scroll * zoomSensitivity, (float)minFov, (float)maxFov);
            }

            if (cam.fieldOfView >= maxFov)
            {
                tf.localPosition = new Vector3(0, 0, Mathf.Clamp(tf.localPosition.z + scroll, minCamDistance, maxCamDistance));
            }
        }     
    }
}
