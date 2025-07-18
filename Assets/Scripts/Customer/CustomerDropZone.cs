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
            // Update the product stock
            product.productData.ModifyStock(-1); // This will trigger the OnStockChanged event

            Log("Customer accepted item. Product stock reduced.");

            if (Mathf.Approximately(customer.moneyGiven, product.productData.productPrice))
            {
                // Exact change case
                CurrencyManager.Instance.AddFunds(customer.moneyGiven);
                customer.isServed = true;
                Destroy(product.gameObject);
                CashierUI.Instance.CloseUI(); // Call through the Instance
            }
            else
            {
                // Needs change case - don't close UI here
                CashierUI.Instance.OpenUI(
                    customer.moneyGiven,
                    product.productData.productPrice
                );
                CashierUI.Instance.currentCustomer = customer;
                CashierUI.Instance.currentProductGO = product.gameObject;
            }
        }
    }
}