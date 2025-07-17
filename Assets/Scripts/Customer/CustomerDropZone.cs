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

    private void LogError(string message)
    {
        if (enableDebugLogs) Debug.LogError(message);
    }


    public void OnProductDrop(ProductControls product)
    {
        if (customer == null || product == null || product.productData == null)
        {
            LogWarning("Missing customer or product data.");
            return;
        }

        if (customer.desiredProducts.Contains(product.productData))
        {
            Log("Customer accepted item. Waiting for correct change...");

            if (CashierUI.Instance != null)
            {
                // Only pass the values, don't add money yet
                CashierUI.Instance.OpenUI(
                    customer.moneyGiven,
                    product.productData.productPrice
                );
                CashierUI.Instance.currentCustomer = customer;
                CashierUI.Instance.currentProductGO = product.gameObject;
            }
        }
        else
        {
            product.ResetToStartPosition();
        }
    }
}