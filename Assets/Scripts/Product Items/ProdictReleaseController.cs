using UnityEngine;

public class ProdictReleaseController : MonoBehaviour, ProductDropZone
{
    public void OnProductDrop(ProductControls _product)
    {
        Debug.Log("Product Sold To Customer");
    }
}
