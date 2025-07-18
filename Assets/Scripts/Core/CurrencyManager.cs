using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }

    [Header("Settings")]
    public string currencySymbol = "₱";
    public int startingCurrency = 100;
    private int currentCurrency;

    [Header("UI")]
    public TextMeshProUGUI currencyText;

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

    private void UpdateUI()
    {
        if (currencyText != null)
            currencyText.text = $"{currencySymbol}{currentCurrency}";
    }

    public int GetCurrentBalance() => currentCurrency;

    public void AddFunds(int amount)
    {
        currentCurrency += amount;
        UpdateUI();
        Debug.Log($"Added {currencySymbol}{amount}. Total: {currencySymbol}{currentCurrency}");
    }

    public bool SpendFunds(int amount)
    {
        Debug.Log($"Attempting to spend {amount} (Current: {currentCurrency})");
        if (currentCurrency >= amount)
        {
            currentCurrency -= amount;
            UpdateUI();
            Debug.Log($"Successfully spent {amount}. Remaining: {currentCurrency}");
            return true;
        }
        Debug.LogWarning($"Insufficient funds! Tried to spend {amount} but only have {currentCurrency}");
        return false;
    }

    public void ResetCurrency()
    {
        currentCurrency = startingCurrency;
        UpdateUI();
    }
}