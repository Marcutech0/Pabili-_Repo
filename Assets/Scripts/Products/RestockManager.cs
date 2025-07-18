using UnityEngine;
using System.Collections.Generic;

public class RestockManager : MonoBehaviour
{
    [Header("References")]
    public ProductLoader productLoader;
    public Transform shelfParent;

    [Header("Settings")]
    public float verticalOffset = 0.2f;
    public int maxProductsPerShelf = 5;

    private Dictionary<ProductData, Transform> productToShelfMap = new();

    void Start()
    {
        // Reset all product counts before initializing
        InitializeShelves();
    }

    void OnApplicationQuit()
    {
        ResetAllProducts();
    }

    void InitializeShelves()
    {
        productToShelfMap.Clear();

        if (shelfParent.childCount < productLoader.productPrefabs.Count)
        {
            Debug.LogError("Not enough shelves for all products!");
            return;
        }

        for (int i = 0; i < productLoader.productPrefabs.Count; i++)
        {
            var productData = productLoader.productPrefabs[i].GetComponent<ProductControls>().productData;
            productToShelfMap[productData] = shelfParent.GetChild(i);
            InitializeShelfStock(productData, shelfParent.GetChild(i));
        }
    }

    public void InitializeShelfStock(ProductData productData, Transform shelf)
    {
        // Clear existing children
        foreach (Transform child in shelf)
        {
            Destroy(child.gameObject);
        }
        productData.spawnedCount = 0; // Reset spawned count

        // Spawn products up to current stock or max shelf capacity
        int canSpawn = Mathf.Min(
            productData.productStock,
            productData.productMaxStack,
            maxProductsPerShelf
        );

        for (int i = 0; i < canSpawn; i++)
        {
            Vector3 spawnPos = CalculateSpawnPosition(shelf, i);
            Instantiate(GetPrefabForProduct(productData), spawnPos, Quaternion.identity, shelf);
            productData.spawnedCount++;
        }
    }

    Vector3 CalculateSpawnPosition(Transform shelf, int itemIndex)
    {
        return shelf.position + new Vector3(0, itemIndex * verticalOffset, 0);
    }

    GameObject GetPrefabForProduct(ProductData productData)
    {
        foreach (var prefab in productLoader.productPrefabs)
        {
            if (prefab.GetComponent<ProductControls>().productData == productData)
            {
                return prefab;
            }
        }
        return null;
    }

    public bool CanRestock(ProductData productData)
    {
        return productData.productStock < productData.productMaxStack;
    }

    public void RestockProduct(ProductData productData)
    {
        // Check if we can restock first
        if (!CanRestock(productData))
        {
            Debug.Log($"Cannot restock {productData.productName} - already at max capacity");
            return;
        }

        if (!CurrencyManager.Instance.SpendFunds(productData.restockPrice))
            return;

        // Calculate how many we can add without exceeding max
        int canAdd = Mathf.Min(
            productData.productRestockAmount,
            productData.productMaxStack - productData.productStock
        );

        // Update the stock count
        productData.productStock += canAdd;

        // Only spawn up to shelf capacity
        if (productToShelfMap.TryGetValue(productData, out Transform shelfSlot))
        {
            int currentShelfCount = shelfSlot.childCount;
            int canSpawn = Mathf.Min(canAdd, maxProductsPerShelf - currentShelfCount);

            for (int i = 0; i < canSpawn; i++)
            {
                Vector3 spawnPos = CalculateSpawnPosition(shelfSlot, currentShelfCount + i);
                Instantiate(GetPrefabForProduct(productData), spawnPos, Quaternion.identity, shelfSlot);
                productData.spawnedCount++;
            }
        }
    }

    public void ResetAllProducts()
    {
        foreach (var prefab in productLoader.productPrefabs)
        {
            var productData = prefab.GetComponent<ProductControls>().productData;
            productData.FullReset();

            // Clear shelf visuals
            if (productToShelfMap.TryGetValue(productData, out Transform shelf))
            {
                foreach (Transform child in shelf)
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }
}