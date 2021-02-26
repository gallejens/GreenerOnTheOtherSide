using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingCycle : MonoBehaviour
{
    private Light directionalLight;
    [SerializeField] private LightingPreset preset = null;

    private float cycleLength = 600; // in seconds
    [SerializeField, Range(0, 600)] private float timeOfDay;

    void Start()
    {
        directionalLight = GameObject.Find("Sun").GetComponent<Light>();
        timeOfDay = cycleLength / 2; // set starting time as middle of day

        UpdateLighting(350 / cycleLength);
    }

    void Update()
    {
        //timeOfDay += Time.deltaTime;
        //timeOfDay %= cycleLength;

        //UpdateLighting(timeOfDay / cycleLength);
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = preset.ambientColor.Evaluate(timePercent);
        RenderSettings.fogColor = preset.fogColor.Evaluate(timePercent);
        RenderSettings.skybox.SetColor("_Tint", preset.skyColor.Evaluate(timePercent));

        directionalLight.color = preset.directionalColor.Evaluate(timePercent);
        directionalLight.transform.localRotation = Quaternion.Euler(new Vector3(timePercent * 360f, 180, 0));
    }
}
