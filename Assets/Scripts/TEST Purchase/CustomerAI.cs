using UnityEngine;

public class CustomerAI : MonoBehaviour
{
    public ProductData desiredProduct;
    public float moneyGiven;
    public bool isServed;

    public void RequestProduct(ProductData product)
    {
        desiredProduct = product;
        moneyGiven = product.price + Random.Range(1, 5);
        Debug.Log($"🧍 Customer wants: {desiredProduct.productName} | Paid: {moneyGiven}");
    }

    public bool ReceiveProduct(ProductData product)
    {
        if (isServed) return false;

        if (product == desiredProduct)
        {
            isServed = true;
            Debug.Log("✅ Customer received the correct item.");
            return true;
        }
        else
        {
            Debug.Log("❌ Wrong item delivered!");
            return false;
        }
    }
}
