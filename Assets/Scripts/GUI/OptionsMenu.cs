using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public static OptionsMenu Instance { get; private set; }

    [SerializeField] private AudioMixer audioMixer;

    private IOptionsBack lastMenu;

    private void Awake()
    {
        Instance = this;
    }

    public void BackButtonPressed()
    {
        PlayerPreferencesManager.Instance.SavePlayerPreferences();

        Disable();
        lastMenu.Enable();
    }

    public void SetMouseSensitivity(float sens)
    {
        CameraMovement.MouseSensitivity = sens;
        PlayerPreferencesManager.LoadedPlayerPreferences.mouseSens = sens;
    }
    
    public void SetSoundVolume(float volume)
    {
        audioMixer.SetFloat("SoundVolume", volume);
        PlayerPreferencesManager.LoadedPlayerPreferences.soundVolume = volume;
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
        PlayerPreferencesManager.LoadedPlayerPreferences.musicVolume = volume;
    }

    public void FullscreenToggle(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPreferencesManager.LoadedPlayerPreferences.fullscreen = isFullscreen;
    }

    public void Enable(IOptionsBack menu)
    {
        GenericMethods.SetAllChildrenActive(gameObject, true);
        lastMenu = menu;
    }

    public void Disable() => GenericMethods.SetAllChildrenActive(gameObject, false);
}

public interface IOptionsBack
{
    public void Enable();
}
