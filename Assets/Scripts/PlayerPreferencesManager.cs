using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.UI;

public class PlayerPreferencesManager : MonoBehaviour
{
    public static PlayerPreferencesManager Instance { get; private set; }

    public static PlayerPreferences LoadedPlayerPreferences = new PlayerPreferences();

    [SerializeField] private GameObject sensSlider;
    [SerializeField] private GameObject soundSlider;
    [SerializeField] private GameObject musicSlider;
    [SerializeField] private GameObject fullscreenToggle;

    [SerializeField] private OptionsMenu optionsMenu;

    private void Awake()
    {
        Instance = this;

        try
        {
            LoadedPlayerPreferences = GenericMethods.ReadJSONFile<PlayerPreferences>("playerpreferences");
        }
        catch (JsonSerializationException) 
        {
            // default sens value to 5 instead of 0 if no playerpreference files exists
            LoadedPlayerPreferences.mouseSens = 5;
        }
    }

    private void Start()
    {
        sensSlider.GetComponent<Slider>().SetValueWithoutNotify(LoadedPlayerPreferences.mouseSens);
        optionsMenu.SetMouseSensitivity(LoadedPlayerPreferences.mouseSens);

        soundSlider.GetComponent<Slider>().SetValueWithoutNotify(LoadedPlayerPreferences.soundVolume);
        optionsMenu.SetSoundVolume(LoadedPlayerPreferences.soundVolume);

        musicSlider.GetComponent<Slider>().SetValueWithoutNotify(LoadedPlayerPreferences.musicVolume);
        optionsMenu.SetMusicVolume(LoadedPlayerPreferences.musicVolume);

        fullscreenToggle.GetComponent<Toggle>().SetIsOnWithoutNotify(LoadedPlayerPreferences.fullscreen);
        optionsMenu.FullscreenToggle(LoadedPlayerPreferences.fullscreen);
    }

    public void SavePlayerPreferences()
    {
        GenericMethods.WriteJSONFile(LoadedPlayerPreferences, "playerpreferences");
    }
}