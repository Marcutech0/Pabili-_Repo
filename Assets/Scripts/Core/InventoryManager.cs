using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [System.Serializable]
    public class InventoryItem
    {
        public ProductData product;
        public int quantity;
    }

    public List<InventoryItem> inventory = new();

    public void AddProduct(ProductData product, int quantity = 1)
    {
        var existing = inventory.Find(i => i.product == product);
        if (existing != null)
        {
            existing.quantity += quantity;
        }
        else
        {
            inventory.Add(new InventoryItem { product = product, quantity = quantity });
        }
        Debug.Log($"Added {quantity}x {product.productName}. Total: {existing?.quantity ?? quantity}");
    }

    public bool RemoveProduct(ProductData product, int quantity = 1)
    {
        var item = inventory.Find(i => i.product == product);
        if (item != null && item.quantity >= quantity)
        {
            item.quantity -= quantity;
            if (item.quantity <= 0)
            {
                inventory.Remove(item);
            }
            Debug.Log($"Removed {quantity}x {product.productName}");
            return true;
        }
        Debug.LogWarning($"Couldn't remove {quantity}x {product.productName}");
        return false;
    }
}