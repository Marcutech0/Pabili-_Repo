using TMPro;
using UnityEngine;

public class CashierUI : MonoBehaviour
{
    public static CashierUI Instance;

    public GameObject panel;
    public TMP_Text expectedChangeText;
    [SerializeField] public TMP_Text inputDisplayText;

    public float correctChange;

    [HideInInspector] public CustomerAI currentCustomer;
    [HideInInspector] public GameObject currentProductGO;

    private string currentInput = "";
    private float submittedAmount;
    private float customerPaid;

    private bool transactionSubmitted = false;

    void Awake()
    {
        Instance = this;
    }

    public void OpenUI(float moneyGiven, float productPrice)
    {
        panel.SetActive(true);
        Time.timeScale = 0f;

        customerPaid = moneyGiven;
        correctChange = moneyGiven - productPrice;

        expectedChangeText.text = $"Change for ₱{moneyGiven:F2}";
        currentInput = "";
        inputDisplayText.text = "₱0.00";
        transactionSubmitted = false;

        Debug.Log($"🧾 Cashier UI opened. Expecting change: ₱{correctChange:F2}");
    }

    public void AddDigit(string digit)
    {
        if (transactionSubmitted)
        {
            Debug.Log("⚠️ Transaction already submitted.");
            return;
        }

        if (currentInput.Length < 7)
        {
            currentInput += digit;
            Debug.Log($"🔢 Added digit: {digit} → Current input: {currentInput}");
            UpdateDisplay();
        }
    }

    public void ClearInput()
    {
        if (transactionSubmitted)
        {
            Debug.Log("⚠️ Cannot clear input. Transaction already submitted.");
            return;
        }

        currentInput = "";
        Debug.Log("🧽 Input cleared.");
        UpdateDisplay();
    }

    public void SubmitChange()
    {
        if (transactionSubmitted)
        {
            Debug.Log("⚠️ Already submitted.");
            return;
        }

        if (float.TryParse(currentInput, out submittedAmount))
        {
            if (Mathf.Approximately(submittedAmount, correctChange))
            {
                Debug.Log($"✅ Correct change submitted: ₱{submittedAmount:F2}");

                inputDisplayText.text = $"✅ Correct! ₱{submittedAmount:F2}";

                // Finalize the transaction
                if (currentCustomer != null)
                {
                    currentCustomer.isServed = true;
                    Debug.Log("🎉 Customer marked as served.");
                }

                if (currentProductGO != null)
                {
                    Destroy(currentProductGO);
                }

                transactionSubmitted = true;
                Invoke(nameof(CloseUI), 1.2f);
            }
            else
            {
                Debug.Log($"❌ Incorrect change. Submitted: ₱{submittedAmount:F2} | Expected: ₱{correctChange:F2}");
                inputDisplayText.text = $"❌ Incorrect! You gave ₱{submittedAmount:F2}";
            }
        }
        else
        {
            Debug.Log("❌ Invalid input, could not parse number.");
            inputDisplayText.text = "❌ Invalid Input!";
        }
    }

    private void UpdateDisplay()
    {
        if (!string.IsNullOrEmpty(currentInput) && float.TryParse(currentInput, out float val))
            inputDisplayText.text = $"₱{val:F2}";
        else
            inputDisplayText.text = "₱0.00";

        Debug.Log("🖋 UI Updated: Change: " + inputDisplayText.text);
    }

    public void CloseUI()
    {
        Time.timeScale = 1f;
        panel.SetActive(false);

        Debug.Log("📦 Cashier UI closed.");

        // Reset state
        currentCustomer = null;
        currentProductGO = null;
        currentInput = "";
        transactionSubmitted = false;
    }
}
