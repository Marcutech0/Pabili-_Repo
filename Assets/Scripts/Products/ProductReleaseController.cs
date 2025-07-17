using UnityEngine;

public class ProductReleaseController : MonoBehaviour, ProductDropZone
{
    public void OnProductDrop(ProductControls _product)
    {
        Destroy(_product.gameObject);
        Debug.Log("Product Sold To Customer");
    }
}