using TMPro;
using UnityEngine;
using System.Collections;

public class CashierUI : MonoBehaviour
{
    [Header("Debug")]
    public bool enableDebugLogs = true;

    [Header("UI References")]
    public GameObject panel;
    public TMP_Text expectedChangeText;
    public TMP_Text inputDisplayText;

    [Header("Feedback Settings")]
    public float errorDisplayDuration = 1.5f;
    public float shakeIntensity = 10f;
    public float successDisplayDuration = 1f;

    private int correctChange;
    [HideInInspector] public CustomerAI currentCustomer;
    [HideInInspector] public GameObject currentProductGO;
    private string currentInput = "";
    private int customerPaid;
    private bool transactionSubmitted = false;
    private ProductControls currentProductControls;

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

    public static CashierUI Instance;

    public void OpenUI(int moneyGiven, int productPrice)
    {
        if (panel == null)
        {
            LogError("CashierUI panel reference is missing!");
            return;
        }

        int change = moneyGiven - productPrice;
        if (change <= 0)
        {
            LogError("CashierUI opened with invalid change amount!");
            CloseUI();
            return;
        }

        panel.SetActive(true);
        Time.timeScale = 0f;

        customerPaid = moneyGiven;
        correctChange = change;

        expectedChangeText.text = $"Change for {CurrencyManager.Instance.currencySymbol}{moneyGiven}";
        ClearInput();
        transactionSubmitted = false;
    }

    public void AddDigit(string digit)
    {
        if (transactionSubmitted) return;
        if (currentInput.Length < 7)
        {
            currentInput += digit;
            UpdateDisplay();
        }
    }

    public void ClearInput()
    {
        if (transactionSubmitted) return;
        currentInput = "";
        inputDisplayText.text = $"{CurrencyManager.Instance.currencySymbol}0";
    }

    public void SubmitChange()
    {
        if (transactionSubmitted || currentCustomer == null) return;

        if (!int.TryParse(currentInput, out int enteredAmount))
        {
            ShowError("Invalid amount!");
            return;
        }

        if (enteredAmount == correctChange)
        {
            ProcessCorrectChange(enteredAmount);
        }
        else
        {
            ShowIncorrectChange(enteredAmount);
        }
    }

    private void ProcessCorrectChange(int amount)
    {
        transactionSubmitted = true;
        CurrencyManager.Instance.AddFunds(customerPaid);

        if (currentCustomer != null)
        {
            currentCustomer.isServed = true;
            Log($"Successfully served customer. Received ₱{customerPaid}");
        }

        if (currentProductGO != null)
        {
            // Get product controls before destroying
            currentProductControls = currentProductGO.GetComponent<ProductControls>();

            // Return to pool or destroy with delay
            StartCoroutine(CompleteProductTransaction());
        }

        inputDisplayText.text = $"<color=green>✅ Correct! {CurrencyManager.Instance.currencySymbol}{amount}</color>";
        StartCoroutine(CloseAfterDelay(successDisplayDuration));
    }

    private IEnumerator CompleteProductTransaction()
    {
        yield return new WaitForSecondsRealtime(0.5f); // Small delay for visual feedback

        if (currentProductControls != null)
        {
            // Update shelf display if product came from shelf
            ProductDisplay shelfDisplay = currentProductControls.GetComponent<ProductDisplay>();
            if (shelfDisplay != null)
            {
                shelfDisplay.UpdateStack(currentProductControls.productData.productStock);
            }
        }

        Destroy(currentProductGO);
    }

    private IEnumerator CloseAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        CloseUI();
    }

    private void ShowIncorrectChange(int enteredAmount)
    {
        inputDisplayText.text =
            $"<color=red>❌ Entered: {CurrencyManager.Instance.currencySymbol}{enteredAmount}</color>\n" +
            $"<color=green>Expected: {CurrencyManager.Instance.currencySymbol}{correctChange}</color>";

        ShakeInputDisplay();
        currentInput = "";
        Invoke(nameof(ClearInput), errorDisplayDuration);
    }

    private void ShowError(string message)
    {
        inputDisplayText.text = $"<color=red>❌ {message}</color>";
        ShakeInputDisplay();
        currentInput = "";
        Invoke(nameof(ClearInput), errorDisplayDuration);
    }

    private void ShakeInputDisplay()
    {
        LeanTween.cancel(inputDisplayText.gameObject);
        LeanTween.moveX(inputDisplayText.gameObject, shakeIntensity, 0.1f)
            .setEaseShake()
            .setLoopPingPong(3);
    }

    private void UpdateDisplay()
    {
        inputDisplayText.text = string.IsNullOrEmpty(currentInput)
            ? $"{CurrencyManager.Instance.currencySymbol}0"
            : $"{CurrencyManager.Instance.currencySymbol}{currentInput}";
    }

    public void CloseUI()
    {
        Time.timeScale = 1f;
        if (panel != null) panel.SetActive(false);

        // Clean up references
        currentCustomer = null;
        currentProductGO = null;
        currentProductControls = null;

        ClearInput();
        transactionSubmitted = false;
    }

    private void Log(string message) { if (enableDebugLogs) Debug.Log($"[CashierUI] {message}"); }
    private void LogWarning(string message) { if (enableDebugLogs) Debug.LogWarning($"[CashierUI] {message}"); }
    private void LogError(string message) { if (enableDebugLogs) Debug.LogError($"[CashierUI] {message}"); }
}