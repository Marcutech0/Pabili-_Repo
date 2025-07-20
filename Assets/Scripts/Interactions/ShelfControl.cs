using UnityEngine;

public class ShelfControl : MonoBehaviour, ProductDropZone
{
    public void OnProductDrop(ProductControls productCtrl)
    {
        // Update the shelf display when a product is returned
        ProductDisplay display = GetComponentInChildren<ProductDisplay>();
        if (display != null)
        {
            display.UpdateStack(productCtrl.productData.productStock);
        }
    }
}