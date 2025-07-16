using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    
    public List<ProductData> products = new();  // Allows access to Product Data pool

    public void AddProduct(ProductData product)
    {
        products.Add(product);
        Debug.Log("Added " + product.productName);
    }

    public void RemoveProduct(ProductData product)
    {
        products.Remove(product);
        Debug.Log("Removed " + product.productName);
    }
}
