using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    [Header("Display Settings")]
    public TMP_Dropdown ResolutionDD;
    public Toggle Fullscreen;

    [Header("Audio Settings")]
    public Button AudioButton;
    public GameObject VolumeSliderObject;
    public Slider VolumeSlider;

    private Resolution[] allResolutions;

    void Start()
    {
        // 🔈 Volume slider setup
        if (VolumeSlider != null)
        {
            VolumeSlider.value = AudioListener.volume;
            VolumeSlider.onValueChanged.AddListener(SetVolume);
        }

        // 🖥️ Resolution dropdown setup
        allResolutions = Screen.resolutions;
        ResolutionDD.ClearOptions();
        List<string> options = new List<string>();
        int currentResIndex = 0;

        for (int i = 0; i < allResolutions.Length; i++)
        {
            string option = allResolutions[i].width + " x " + allResolutions[i].height;
            options.Add(option);

            if (allResolutions[i].width == Screen.currentResolution.width &&
                allResolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }

        ResolutionDD.AddOptions(options);
        ResolutionDD.value = currentResIndex;
        ResolutionDD.RefreshShownValue();
        ResolutionDD.onValueChanged.AddListener(SetResolution);

        // 🌐 Fullscreen toggle
        Fullscreen.isOn = Screen.fullScreen;
        Fullscreen.onValueChanged.AddListener(SetFullscreen);

        // 🎚️ Audio button toggle
        if (AudioButton != null)
        {
            AudioButton.onClick.AddListener(ToggleVolumeSlider);
        }

        // Make sure volume panel is hidden on start
        if (VolumeSliderObject != null)
        {
            VolumeSliderObject.SetActive(false);
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution res = allResolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void ToggleVolumeSlider()
    {
        if (VolumeSliderObject != null)
        {
            bool isActive = VolumeSliderObject.activeSelf;
            VolumeSliderObject.SetActive(!isActive);
        }
    }
}