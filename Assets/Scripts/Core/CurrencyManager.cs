using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    [Header("Debug")]
    public bool enableDebugLogs = true; // Toggle in Inspector

    public static CurrencyManager Instance { get; private set; }

    [Header("Settings")]
    public string currencySymbol = "₱";
    public int startingCurrency = 100;
    private int currentCurrency;

    [Header("UI")]
    public TextMeshProUGUI currencyText;

    // Allows debug logs for non-game breaking errors
    private void Log(string message)
    {
        if (enableDebugLogs) Debug.Log(message);
    }

    private void LogWarning(string message)
    {
        if (enableDebugLogs) Debug.LogWarning(message);
    }

    private void LogError(string message)
    {
        if (enableDebugLogs) Debug.LogError(message);
    }


    public void AddFunds(int amount)
    {
        currentCurrency += amount;
        UpdateUI();
        Log($"Added {currencySymbol}{amount}. Total: {currencySymbol}{currentCurrency}");
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            currentCurrency = startingCurrency;
            UpdateUI();
            Debug.Log($"CurrencyManager initialized with {currencySymbol}{currentCurrency:F2}");
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public bool SpendFunds(int amount)
    {
        Log($"Attempting to spend {amount} (Current: {currentCurrency})");
        if (currentCurrency >= amount)
        {
            currentCurrency -= amount;
            UpdateUI();
            Log($"Successfully spent {amount}. Remaining: {currentCurrency}");
            return true;
        }
        LogWarning($"Insufficient funds! Tried to spend {amount} but only have {currentCurrency}");
        return false;
    }

    private void UpdateUI()
    {
        if (currencyText != null)
            currencyText.text = $"{currencySymbol}{currentCurrency}"; // Removed :F2 formatting
    }

    public int GetCurrentBalance() => currentCurrency;

    public void ResetCurrency()
    {
        currentCurrency = startingCurrency;
        UpdateUI();
    }
}