using UnityEngine;
using System.Linq; 

public class CustomerDropZone : MonoBehaviour, ProductDropZone
{
    [Header("Debug")]
    public bool enableDebugLogs = true; // Toggle in Inspector

    [Header("Customer who is waiting for the item")]
    public CustomerAI customer;

    // Allows debug logs for non-game breaking errors
    private void Log(string message)
    {
        if (enableDebugLogs) Debug.Log(message);
    }

    private void LogWarning(string message)
    {
        if (enableDebugLogs) Debug.LogWarning(message);
    }

    public void OnProductDrop(ProductControls product)
    {
        // Checks for product data
        if (customer == null || product == null || product.productData == null)
        {
            LogWarning("Missing customer or product data.");
            return;
        }

        // Once correct product is placed, customer will wait for change
        if (customer.desiredProducts.Contains(product.productData))
        {
            Log("🕒 Customer accepted item. Waiting for correct change...");

            if (CashierUI.Instance != null)
            {
                CashierUI.Instance.OpenUI(customer.moneyGiven, product.productData.productPrice);
                CashierUI.Instance.currentCustomer = customer;
                CashierUI.Instance.currentProductGO = product.gameObject;
            }
            else
            {
                LogWarning("Cashier UI is not assigned or not in scene.");
            }
        }
        else
        {
            Log("❌ Wrong item delivered!");
            product.ResetToStartPosition();
        }
    }
}
