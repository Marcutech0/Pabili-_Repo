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
            // Validate the product list
            if (products == null || products.Length == 0)
            {
                Debug.LogError("❌ No products assigned to the product list.");
                return;
            }

            // Select a random product
            ProductData selectedProduct = products[Random.Range(0, products.Length)];

            if (customer != null)
            {
                customer.RequestProduct(selectedProduct);

                // 🧾 Console feedback
                Debug.Log($"📦 Sent request: Customer now wants \"{selectedProduct.productName}\" and paid ₱{customer.moneyGiven:F2}");
            }
            else
            {
                Debug.LogWarning("⚠️ No customer GameObject assigned in the Inspector.");
            }
        }
    }
}
