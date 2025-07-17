using UnityEngine;
using System.Collections.Generic;

public class ShelfControl : MonoBehaviour, ProductDropZone
{
    [Header("Debug")]
    public bool enableDebugLogs = true; // Toggle in Inspector

    private readonly List<ProductControls> stackedProducts = new();

    [Header("Stack Settings")]
    public float stackSpacing = 0.3f;       
    public int maxStackSize = 5;

    // Allows debug logs for non-game breaking errors
    private void Log(string message)
    {
        if (enableDebugLogs) Debug.Log(message);
    }

    private void LogWarning(string message)
    {
        if (enableDebugLogs) Debug.LogWarning(message);
    }

    public void OnProductDrop(ProductControls _product)
    {
        stackedProducts.RemoveAll(p => p == null);
        Log("Cleaned shelf. Current count: " + stackedProducts.Count);

        // Checks if shelves are full, if so, item is returned
        if (stackedProducts.Contains(_product))
        {
            Log("This product is already on the shelf.");
            return;
        }

        if (stackedProducts.Count >= maxStackSize)
        {
            Log("Shelf is full! Returning item.");
            _product.ResetToStartPosition();
            return;
        }

        stackedProducts.Add(_product);
        _product.transform.SetParent(transform);

        // Returns list of stacked items
        RestackItems();
        Log("Product stacked on shelf. Total: " + stackedProducts.Count);
    }

    public void RemoveProduct(ProductControls _product)
    {
        if (stackedProducts.Contains(_product))
        {
            stackedProducts.Remove(_product);
            _product.transform.SetParent(null);
            RestackItems();
            Log("Product removed. Remaining: " + stackedProducts.Count);
        }
    }

    private void RestackItems()
    {
        for (int i = 0; i < stackedProducts.Count; i++)
        {
            float zOffset = -0.01f * i;
            Vector3 stackedPosition = transform.position + new Vector3(0, stackSpacing * (i + 1), zOffset);
            stackedProducts[i].transform.position = stackedPosition;
        }
    }
}
