using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<Product> products = new List<Product>();

    public void AddProduct(Product product)
    {
        products.Add(product);
        Debug.Log("Added " + product.productName);
    }

    public void RemoveProduct(Product product)
    {
        products.Remove(product);
        Debug.Log("Removed " + product.productName);
    }
}
