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
            if (product != null)
            {
                product.ReturnToOriginalPosition();
            }
            return;
        }

        if (product == null || product.productData == null)
        {
            LogWarning("Invalid product dropped!");
            return;
        }

        // Process valid product drop
        if (customer.ReceiveProduct(product.productData))
        {
            // Successful transaction - open cashier UI
            int productPrice = product.productData.productPrice;
            CashierUI.Instance.OpenUI(customer.moneyGiven, productPrice);
            CashierUI.Instance.currentCustomer = customer;
            CashierUI.Instance.currentProductGO = product.gameObject;
        }
        else
        {
            // Wrong product - return to shelf
            product.ReturnToOriginalPosition();
            LogWarning("Customer didn't want this product!");
        }
    }
}