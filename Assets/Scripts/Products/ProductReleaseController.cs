using UnityEngine;

public class ProductReleaseController : MonoBehaviour, ProductDropZone
{
    public void OnProductDrop(ProductControls product)
    {
        product.productData.spawnedCount--;
        Destroy(product.gameObject);
    }
}