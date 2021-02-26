using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public static CameraRotate Instance { get; private set; }
    
    [SerializeField] private float speed = 10f;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        transform.localEulerAngles = new Vector3(35, 0, 0);
        transform.position = new Vector3(0, -3.5f, 0);

        Camera.main.transform.localPosition = new Vector3(-6.31f, 0, -45);
        Camera.main.fieldOfView = 40;
    }

    private void Update()
    {
        transform.localEulerAngles += new Vector3(0, speed * Time.deltaTime, 0);
    }
}
