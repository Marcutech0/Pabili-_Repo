using UnityEngine;

public class CustomerDropZone2 : MonoBehaviour, ProductDropZone
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

        if (customer.ReceiveProduct(product.productData))
        {
            Debug.Log("🎉 Transaction successful!");
            Destroy(product.gameObject);

            if (CashierUI.Instance != null)
            {
                CashierUI.Instance.OpenUI(customer.moneyGiven, product.productData.price);
            }
            else
            {
                Debug.LogWarning("Cashier UI is not assigned or not in scene.");
            }
        }
        else
        {
            Debug.Log("⚠️ Transaction failed. Customer didn't want this item.");
            product.ResetToStartPosition();
        }
    }
}
