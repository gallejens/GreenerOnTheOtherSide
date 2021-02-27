using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; set; }

    private AudioSource audioSource;

    [SerializeField] AudioClip backButtonSound;
    [SerializeField] AudioClip normalButtonSound;
    [SerializeField] AudioClip platformPickSound;
    [SerializeField] AudioClip platformPlaceSound;
    [SerializeField] AudioClip supportSound;
    [SerializeField] AudioClip pauseSound;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void BackButtonClicked()
    {
        audioSource.clip = backButtonSound;
        audioSource.Play();
    }

    public void NormalButtonClicked()
    {
        audioSource.clip = normalButtonSound;
        audioSource.Play();
    }

    public void PlatformPicked()
    {
        audioSource.clip = platformPickSound;
        audioSource.Play();
    }

    public void PlatformPlaced()
    {
        audioSource.clip = platformPlaceSound;
        audioSource.Play();
    }

    public void SupportClicked()
    {
        audioSource.clip = supportSound;
        audioSource.Play();
    }

    public void GamePaused()
    {
        audioSource.clip = pauseSound;
        audioSource.Play();
    }
}

