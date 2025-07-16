using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public TMP_Dropdown ResolutionDD;
    public Toggle Fullscreen;

    Resolution[] AllResolutions;
    bool IsFullscreen;
    void Start()
    {
        IsFullscreen = true;
        AllResolutions = Screen.resolutions;

        List<string> resolutionStringList = new List<string>();
        foreach (Resolution res in AllResolutions)
        {
            resolutionStringList.Add(res.ToString());
        }

        ResolutionDD.AddOptions(resolutionStringList);
    }
}
