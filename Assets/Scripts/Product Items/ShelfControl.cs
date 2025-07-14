using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class ShelfControl : MonoBehaviour, ProductDropZone
{
    public void OnProductDrop(ProductControls _product)
    {
        _product.transform.position = transform.position;
        Debug.Log("Product is on the shelf");
    }
}
