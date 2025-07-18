using System.Linq;
using UnityEngine;

public class CustomerAI : MonoBehaviour
{
    [Header("Payment Settings")]
    public int maxExtraPayment = 5; // Make this configurable in Inspector

    public ProductData[] desiredProducts;
    public int moneyGiven;
    public bool isServed;

    public void RequestProduct(ProductData product)
    {
        if (product == null)
        {
            Debug.LogError("Cannot request null product!");
            return;
        }

        desiredProducts = new ProductData[] { product };
        moneyGiven = Mathf.Max(
            product.productPrice,
            product.productPrice + Random.Range(0, maxExtraPayment + 1)
        );
        isServed = false;
        Debug.Log($"🧍 Customer wants: {product.productName} | Paid: ₱{moneyGiven:F2}");
    }

    public bool ReceiveProduct(ProductData product)
    {
        if (isServed) return false;
        return desiredProducts.Contains(product);
    }
}