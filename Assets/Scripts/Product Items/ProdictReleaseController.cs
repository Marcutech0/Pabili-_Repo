using UnityEngine;

public class ProdictReleaseController : MonoBehaviour, ProductDropZone
{
    public void OnProductDrop(ProductControls _product)
    {
        Destroy(_product.gameObject);
        Debug.Log("Product Sold To Customer");
    }
}
