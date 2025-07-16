using UnityEngine;

public class ProductManager : MonoBehaviour
{
    public ProductData[] products;  // Assigns all products
    public GameObject productCardPrefab;    // Makes a gameobject of the products
    public Transform contentParent;     // Parents content to gameobject

    void Start()
    {
        //Apply for all product instances
        foreach (var product in products)
        {
            GameObject card = Instantiate(productCardPrefab, contentParent);
            ProductUI ui = card.GetComponent<ProductUI>();
            ui.product = product;
        }
    }
}