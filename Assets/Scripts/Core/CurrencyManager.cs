using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    [Header("Debug")]
    public bool enableDebugLogs = true; // Toggle in Inspector

    public static CurrencyManager Instance { get; private set; }

    [Header("Settings")]
    public string currencySymbol = "₱";
    public float startingCurrency = 100f;
    private float currentCurrency;

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


    public void AddFunds(float amount)
    {
        currentCurrency += amount;
        UpdateUI();
        Log($"Added {currencySymbol}{amount:F2}. Total: {currencySymbol}{currentCurrency:F2}");
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

    public bool SpendFunds(float amount)
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
            currencyText.text = $"{currencySymbol}{currentCurrency:F2}";
    }

    public float GetCurrentBalance() => currentCurrency;
}