using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Debug")]
    public bool enableDebugLogs = true; // Toggle in Inspector

    public List<ProductData> products = new();  // allows access to Product Data pool

    // Allows debug logs for non-game breaking errors
    private void Log(string message)
    {
        if (enableDebugLogs) Debug.Log(message);
    }

    public void AddProduct(ProductData product)
    {
        products.Add(product);
        Log("Added " + product.productName);
    }

    public void RemoveProduct(ProductData product)
    {
        products.Remove(product);
        Log("Removed " + product.productName);
    }
}
