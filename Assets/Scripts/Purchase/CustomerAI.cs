using System.Linq;
using UnityEngine;

public class CustomerAI : MonoBehaviour
{
    public ProductData[] desiredProducts;
    public float moneyGiven;
    public bool isServed;

    public void RequestProduct(ProductData product)
    {
        desiredProducts = new ProductData[] { product };
        moneyGiven = product.price + Random.Range(1, 5);
        isServed = false;

        Debug.Log($"🧍 Customer wants: {product.productName} | Paid: ₱{moneyGiven:F2}");
    }

    public bool ReceiveProduct(ProductData product)
    {
        if (isServed) return false;
        return desiredProducts.Contains(product);
    }
}
