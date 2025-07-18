using TMPro;
using UnityEngine;

public class CashierUI : MonoBehaviour
{
    [Header("Debug")]
    public bool enableDebugLogs = true; // Toggle in Inspector

    // CashierUI Variables
    public static CashierUI Instance;

    public GameObject panel;
    public TMP_Text expectedChangeText;
    public TMP_Text inputDisplayText;

    private int correctChange;

    [HideInInspector] public CustomerAI currentCustomer;
    [HideInInspector] public GameObject currentProductGO;

    private string currentInput = "";
    private readonly int submittedAmount;
    private int customerPaid;

    private bool transactionSubmitted = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Allows debug logs for non-game breaking errors
    private void Log(string message)
    {
        if (enableDebugLogs) Debug.Log(message);
    }

    private void LogWarning(string message)
    {
        if (enableDebugLogs) Debug.LogWarning(message);
    }

    public void OpenUI(int moneyGiven, int productPrice) // Changed parameters to int
    {
        panel.SetActive(true);
        Time.timeScale = 0f;

        customerPaid = moneyGiven;
        correctChange = moneyGiven - productPrice;

        expectedChangeText.text = $"Change for {CurrencyManager.Instance.currencySymbol}{moneyGiven}";
        currentInput = "";
        inputDisplayText.text = $"{CurrencyManager.Instance.currencySymbol}0";
        transactionSubmitted = false;

        Log($"Cashier UI opened. Expecting change: {CurrencyManager.Instance.currencySymbol}{correctChange}");
    }

    public void AddDigit(string digit)
    {
        if (transactionSubmitted)
        {
            Log("⚠️ Transaction already submitted.");
            return;
        }

        if (currentInput.Length < 7)
        {
            currentInput += digit;
            Log($"🔢 Added digit: {digit} → Current input: {currentInput}");
            UpdateDisplay();
        }
    }

    public void ClearInput()
    {
        if (transactionSubmitted)
        {
            Log("⚠️ Cannot clear input. Transaction already submitted.");
            return;
        }

        currentInput = "";
        Log("🧽 Input cleared.");
        UpdateDisplay();
    }

    public void SubmitChange()
    {
        if (!int.TryParse(currentInput, out int enteredAmount))
        {
            inputDisplayText.text = "❌ Invalid amount!";
            return;
        }

        if (Mathf.Approximately(enteredAmount, correctChange))
        {
            // Only add the money now that transaction is complete
            CurrencyManager.Instance.AddFunds(customerPaid); // Add the full amount customer paid

            if (currentCustomer != null)
                currentCustomer.isServed = true;

            Destroy(currentProductGO);
            inputDisplayText.text = $"✅ Correct! {CurrencyManager.Instance.currencySymbol}{enteredAmount:F2}";
            Invoke(nameof(CloseUI), 1.5f);
        }
        else
        {
            inputDisplayText.text = $"❌ Try again! Expected: {CurrencyManager.Instance.currencySymbol}{correctChange:F2}";
        }
    }

    private void UpdateDisplay()
    {
        if (!string.IsNullOrEmpty(currentInput))
        {
            if (int.TryParse(currentInput, out int val))
                inputDisplayText.text = $"{CurrencyManager.Instance.currencySymbol}{val}";
            else
                inputDisplayText.text = $"{CurrencyManager.Instance.currencySymbol}0";
        }
        else
        {
            inputDisplayText.text = $"{CurrencyManager.Instance.currencySymbol}0";
        }
        Log("UI Updated: Change: " + inputDisplayText.text);
    }

    public void CloseUI()
    {
        Time.timeScale = 1f;
        panel.SetActive(false);

        Log("Cashier UI closed.");

        // Reset state
        currentCustomer = null;
        currentProductGO = null;
        currentInput = "";
        transactionSubmitted = false;
    }
}