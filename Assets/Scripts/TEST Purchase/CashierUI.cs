using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CashierUI : MonoBehaviour
{
    public static CashierUI Instance;

    [Header("UI References")]
    public GameObject panel;
    public TMP_Text amountDueText;
    public TMP_InputField changeInputField;
    public Button confirmButton;

    private float expectedChange;

    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
        confirmButton.onClick.AddListener(CheckChange);
    }

    public void OpenUI(float moneyGiven, float price)
    {
        expectedChange = moneyGiven - price;
        amountDueText.text = $"Expected Change: ₱{expectedChange:0.00}";
        changeInputField.text = "";
        panel.SetActive(true);
    }

    void CheckChange()
    {
        if (float.TryParse(changeInputField.text, out float givenChange))
        {
            if (Mathf.Approximately(givenChange, expectedChange))
            {
                Debug.Log("✅ Correct change given.");
            }
            else
            {
                Debug.Log("❌ Incorrect change.");
            }
        }
        else
        {
            Debug.Log("⚠️ Invalid number input.");
        }

        panel.SetActive(false);
    }
}
