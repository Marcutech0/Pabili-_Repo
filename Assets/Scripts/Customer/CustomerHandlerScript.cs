using UnityEngine;

public class TestCustomerTrigger : MonoBehaviour
{
    [Header("Assign the customer to trigger")]
    public CustomerAI customer;

    [Header("List of available products")]
    public ProductData[] products;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (customer == null)
            {
                Debug.LogWarning("⚠️ No customer assigned!");
                return;
            }

            if (products == null || products.Length == 0)
            {
                Debug.LogError("❌ No products available!");
                return;
            }

            // Add this missing logic:
            ProductData selectedProduct = products[Random.Range(0, products.Length)];
            customer.RequestProduct(selectedProduct);
            Debug.Log($"📦 Customer requested: {selectedProduct.productName}");
        }
    }
}