using UnityEngine;
using System.Linq;

public class CustomerDropZone : MonoBehaviour, ProductDropZone
{
    [Header("Debug")]
    public bool enableDebugLogs = true;

    [Header("Customer Reference")]
    public CustomerAI customer;

    private void Log(string message) => Debug.Log(message);
    private void LogWarning(string message) => Debug.LogWarning(message);
    private void LogError(string message) => Debug.LogError(message);

    public void OnProductDrop(ProductControls product)
    {
        // Basic null checks
        if (customer == null)
        {
            LogWarning("No customer assigned to drop zone!");
            product?.ResetToStartPosition();
            return;
        }

        if (product == null || product.productData == null)
        {
            LogWarning("Invalid product dropped!");
            return;
        }

        // Check if customer wants this product
        if (!customer.ReceiveProduct(product.productData))
        {
            LogWarning($"Customer doesn't want {product.productData.productName}!");
            product.ResetToStartPosition();
            return;
        }

        // Process valid product
        ProcessProductDrop(product);
    }

    private void ProcessProductDrop(ProductControls product)
    {
        // Reduce product stock
        product.productData.ModifyStock(-1);
        Log($"Accepted {product.productData.productName}");

        // Calculate transaction
        int changeNeeded = customer.moneyGiven - product.productData.productPrice;

        if (changeNeeded == 0)
        {
            // Exact payment
            CompleteTransaction(customer, product.gameObject);
        }
        else if (changeNeeded > 0)
        {
            // Needs change
            if (CashierUI.Instance != null)
            {
                CashierUI.Instance.OpenUI(customer.moneyGiven, product.productData.productPrice);
                CashierUI.Instance.currentCustomer = customer;
                CashierUI.Instance.currentProductGO = product.gameObject;
            }
            else
            {
                LogError("CashierUI missing! Completing transaction anyway.");
                CompleteTransaction(customer, product.gameObject);
            }
        }
        else
        {
            // Underpaid
            LogWarning($"Customer underpaid by {-changeNeeded}!");
            product.ResetToStartPosition();
        }
    }

    private void CompleteTransaction(CustomerAI customer, GameObject productObj)
    {
        CurrencyManager.Instance.AddFunds(customer.moneyGiven);
        customer.isServed = true;
        Destroy(productObj);
        Log("Transaction completed successfully");
    }
}