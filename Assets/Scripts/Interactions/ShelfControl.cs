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

    public void OnProductDrop(ProductControls productCtrl)
    {
        // Check if we can add more of this product
        if (productCtrl.productData.spawnedCount >= productCtrl.productData.productMaxStack ||
            productCtrl.productData.spawnedCount >= productCtrl.productData.productStock)
        {
            Debug.Log("Cannot add product - max stack or stock limit reached");
            productCtrl.ResetToStartPosition();
            return;
        }

        // Add to shelf
        productCtrl.transform.SetParent(transform);
        stackedProducts.Add(productCtrl);
        productCtrl.productData.spawnedCount++;
        RestackItems();
    }

    public void RemoveProduct(ProductControls productCtrl)
    {
        if (stackedProducts.Contains(productCtrl))
        {
            productCtrl.productData.spawnedCount--;
            stackedProducts.Remove(productCtrl);
            RestackItems();
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
