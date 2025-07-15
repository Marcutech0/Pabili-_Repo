using UnityEngine;

public class ProductManager : MonoBehaviour
{
    public Product[] products; // Assign all ScriptableObjects
    public GameObject productCardPrefab;
    public Transform contentParent;

    void Start()
    {
        foreach (var product in products)
        {
            GameObject card = Instantiate(productCardPrefab, contentParent);
            ProductUI ui = card.GetComponent<ProductUI>();
            ui.product = product;
        }
    }
}