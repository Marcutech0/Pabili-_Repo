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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            currentCurrency = startingCurrency;
            UpdateUI();
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

    public void AddFunds(float amount)
    {
        currentCurrency += amount;
        UpdateUI();
        Log($"Added {currencySymbol}{amount:F2}. Total: {currencySymbol}{currentCurrency:F2}");
    }

    public bool SpendFunds(float amount)
    {
        if (currentCurrency >= amount)
        {
            currentCurrency -= amount;
            UpdateUI();
            Log($"Spent {currencySymbol}{amount:F2}. Remaining: {currencySymbol}{currentCurrency:F2}");
            return true;
        }
        LogWarning("Insufficient funds!");
        return false;
    }

    private void UpdateUI()
    {
        if (currencyText != null)
            currencyText.text = $"{currencySymbol}{currentCurrency:F2}";
    }

    public float GetCurrentBalance() => currentCurrency;
}