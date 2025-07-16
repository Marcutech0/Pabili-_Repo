using UnityEngine;

public class OptionsToggle : MonoBehaviour
{
    public GameObject optionsPanel;

    public void ToggleOptions()
    {
        if (optionsPanel != null)
            optionsPanel.SetActive(!optionsPanel.activeSelf);
    }
}