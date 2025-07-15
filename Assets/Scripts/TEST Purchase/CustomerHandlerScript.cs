using UnityEngine;

public class TestCustomerTrigger : MonoBehaviour
{
    public CustomerAI customer;
    public ProductData[] products;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (products == null || products.Length == 0)
            {
                Debug.LogError("❌ No products assigned to CustomerHandlerScript!");
                return;
            }

            ProductData product = products[Random.Range(0, products.Length)];
            customer.RequestProduct(product);
        }
    }
}