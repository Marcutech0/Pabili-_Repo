using UnityEngine;
using System.Linq; 

public class CustomerDropZone : MonoBehaviour, ProductDropZone
{
    [Header("Customer who is waiting for the item")]
    public CustomerAI customer;

    public void OnProductDrop(ProductControls product)
    {
        if (customer == null || product == null || product.productData == null)
        {
            Debug.LogWarning("Missing customer or product data.");
            return;
        }

        if (customer.desiredProducts.Contains(product.productData))
        {
            Debug.Log("🕒 Customer accepted item. Waiting for correct change...");

            if (CashierUI.Instance != null)
            {
                CashierUI.Instance.OpenUI(customer.moneyGiven, product.productData.productPrice);
                CashierUI.Instance.currentCustomer = customer;
                CashierUI.Instance.currentProductGO = product.gameObject;
            }
            else
            {
                Debug.LogWarning("Cashier UI is not assigned or not in scene.");
            }
        }
        else
        {
            Debug.Log("❌ Wrong item delivered!");
            product.ResetToStartPosition();
        }
    }
}
