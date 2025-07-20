using UnityEngine;
using System.Collections.Generic;

public class RestockManager : MonoBehaviour
{
    [Header("References")]
    public ProductLoader productLoader;
    public Transform shelfParent;

    private Dictionary<ProductData, Transform> productToShelfMap = new();
    private Dictionary<ProductData, ProductControls> shelfProducts = new();

    void Start()
    {
        InitializeShelves();
    }

    void InitializeShelves()
    {
        productToShelfMap.Clear();
        shelfProducts.Clear();

        if (shelfParent.childCount >= productLoader.productPrefabs.Count)
        {
            for (int i = 0; i < productLoader.productPrefabs.Count; i++)
            {
                var prefab = productLoader.productPrefabs[i];
                var productData = prefab.GetComponent<ProductControls>().productData;
                Transform shelf = shelfParent.GetChild(i);

                productToShelfMap[productData] = shelf;

                // Initialize with existing stock
                if (productData.productStock > 0)
                {
                    SpawnProductOnShelf(productData, shelf);
                }
            }
        }
        else
        {
            Debug.LogError($"Not enough shelves! Need {productLoader.productPrefabs.Count}, has {shelfParent.childCount}");
        }
    }

    public void RestockProduct(ProductData productData)
    {
        if (productData == null) return;

        if (productData.productStock >= productData.productMaxStack)
        {
            Debug.Log($"{productData.productName} already at max stock!");
            return;
        }

        if (!CurrencyManager.Instance.SpendFunds(productData.restockPrice))
            return;

        // Calculate how much we can add
        int canAdd = Mathf.Min(
            productData.productRestockAmount,
            productData.productMaxStack - productData.productStock
        );

        // Apply stock change
        productData.ModifyStock(canAdd);

        // Handle physical product
        if (productToShelfMap.TryGetValue(productData, out Transform shelf))
        {
            if (!shelfProducts.ContainsKey(productData) || shelfProducts[productData] == null)
            {
                SpawnProductOnShelf(productData, shelf);
            }
        }
    }

    void SpawnProductOnShelf(ProductData productData, Transform shelf)
    {
        // Find the correct prefab
        GameObject prefab = GetPrefabForProduct(productData);
        if (prefab == null)
        {
            Debug.LogError($"No prefab found for {productData.productName}");
            return;
        }

        // Create instance
        GameObject productObj = Instantiate(prefab, shelf.position, Quaternion.identity, shelf);
        ProductControls productControls = productObj.GetComponent<ProductControls>();

        if (productControls != null)
        {
            productControls.productData = productData;
            shelfProducts[productData] = productControls;

            // Initialize display to show current stock
            productControls.GetComponent<ProductDisplay>()?.UpdateStack(productData.productStock);

            Debug.Log($"Spawned {productData.productName} on shelf (Stock: {productData.productStock})");
        }
    }

    GameObject GetPrefabForProduct(ProductData productData)
    {
        foreach (var prefab in productLoader.productPrefabs)
        {
            var controls = prefab.GetComponent<ProductControls>();
            if (controls != null && controls.productData == productData)
            {
                return prefab;
            }
        }
        return null;
    }
}