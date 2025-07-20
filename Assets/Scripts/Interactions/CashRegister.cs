using UnityEngine;

public class CashRegisterInteract : MonoBehaviour
{
    private void OnMouseDown()
    {
        CashierUI.Instance.panel.SetActive(true);
    }
}