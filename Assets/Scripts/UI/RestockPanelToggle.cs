using UnityEngine;

public class RestockPanelToggle : MonoBehaviour
{
    public GameObject restockPanel;

    public void TogglePanel()
    {
        restockPanel.SetActive(!restockPanel.activeSelf);
    }
}