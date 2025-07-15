using UnityEngine;
using System.Collections.Generic;

public class CustomerAI : MonoBehaviour
{
    [Header("Requested Products")]
    public List<ProductData> desiredProducts = new List<ProductData>(); 
    public List<ProductData> receivedProducts = new List<ProductData>(); 

    [Header("Payment Info")]
    public float moneyGiven;
    public bool isServed;

    public void RequestProducts(List<ProductData> products)
    {
        desiredProducts = new List<ProductData>(products);
        receivedProducts.Clear();
        isServed = false;

        float total = 0f;
        foreach (var p in desiredProducts)
        {
            total += p.price;
        }

        moneyGiven = total + Random.Range(1, 5);

        string productList = string.Join(", ", desiredProducts.ConvertAll(p => p.productName));
        Debug.Log($"🧍 Customer wants: {productList} | Paid: {moneyGiven}");
    }

    public bool ReceiveProduct(ProductData product)
    {
        if (isServed)
        {
            Debug.Log("⚠️ Customer is already served.");
            return false;
        }

        if (desiredProducts.Contains(product))
        {
            if (receivedProducts.Contains(product))
            {
                Debug.Log($"⚠️ Customer already received: {product.productName}");
                return false;
            }

            receivedProducts.Add(product);
            Debug.Log($"✅ Received correct item: {product.productName}");

            if (receivedProducts.Count >= desiredProducts.Count)
            {
                isServed = true;
                Debug.Log("🎉 All requested items received. Customer is satisfied!");
            }

            return true;
        }
        else
        {
            Debug.Log($"❌ Wrong item: {product.productName} not in requested list.");
            return false;
        }
    }
}
