using UnityEngine;
using System.Collections.Generic;

public class ProductManager : MonoBehaviour
{
    public ProductLoader productLoader;
    public GameObject productCardPrefab;
    public Transform contentParent;
    public RestockManager restockManager;

    void Start()
    {
        CreateProductUI();
    }

    public void ShowCategory(ProductData.ProductCategory category)
    {
        foreach (Transform child in contentParent)
        {
            var ui = child.GetComponent<ProductUI>();
            child.gameObject.SetActive(ui.product.category == category);
        }
    }

    void CreateProductUI()
    {
        foreach (var prefab in productLoader.productPrefabs)
        {
            var productData = prefab.GetComponent<ProductControls>().productData;
            GameObject card = Instantiate(productCardPrefab, contentParent);
            ProductUI ui = card.GetComponent<ProductUI>();
            ui.product = productData;
            ui.restockManager = restockManager;
        }
    }
}