using UnityEngine;
using System.Collections.Generic;

public class TestCustomerTrigger : MonoBehaviour
{
    public CustomerAI customer;
    public ProductData[] products;
    public int numberOfItemsToRequest = 2; // How many random items the customer should request

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (products == null || products.Length == 0)
            {
                Debug.LogError("❌ No products assigned to TestCustomerTrigger!");
                return;
            }

            // Clamp the requested amount to not exceed available products
            int count = Mathf.Clamp(numberOfItemsToRequest, 1, products.Length);

            // Generate a random selection of products (no duplicates)
            List<ProductData> selected = new List<ProductData>();
            List<int> usedIndices = new List<int>();

            while (selected.Count < count)
            {
                int index = Random.Range(0, products.Length);
                if (!usedIndices.Contains(index))
                {
                    usedIndices.Add(index);
                    selected.Add(products[index]);
                }
            }

            customer.RequestProducts(selected);
        }
    }
}
