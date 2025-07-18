using System.Linq;
using UnityEngine;

public class CustomerAI : MonoBehaviour
{
    //Customer Variables
    public ProductData[] desiredProducts;
    public int moneyGiven;
    public bool isServed;

    public void RequestProduct(ProductData product)
    {
        desiredProducts = new ProductData[] { product };
        moneyGiven = product.productPrice; // or keep your random addition: product.productPrice + Random.Range(1, 5);
        isServed = false;
        Debug.Log($"🧍 Customer wants: {product.productName} | Paid: ₱{moneyGiven:F2}");
    }

    public bool ReceiveProduct(ProductData product)
    {
        if (isServed) return false;
        return desiredProducts.Contains(product);
    }
}
