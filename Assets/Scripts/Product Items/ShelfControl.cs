using UnityEngine;
using System.Collections.Generic;

public class ShelfControl : MonoBehaviour, ProductDropZone
{
    private List<ProductControls> stackedProducts = new List<ProductControls>();

    public float stackSpacing = 0.5f;        // Vertical space between items
    public int maxStackSize = 5;

    public void OnProductDrop(ProductControls _product)
    {
        if (stackedProducts.Count >= maxStackSize)
        {
            Debug.Log("Shelf is full! Item returned to original position.");
            _product.ResetToStartPosition();
            return;
        }

        stackedProducts.Add(_product);
        _product.transform.SetParent(transform);

        RestackItems();
        Debug.Log("Product stacked on shelf. Count: " + stackedProducts.Count);
    }

    public void RemoveProduct(ProductControls _product)
    {
        if (stackedProducts.Contains(_product))
        {
            stackedProducts.Remove(_product);
            _product.transform.SetParent(null);
            RestackItems();
            Debug.Log("Product removed. Remaining: " + stackedProducts.Count);
        }
    }

    private void RestackItems()
    {
        for (int i = 0; i < stackedProducts.Count; i++)
        {
            float zOffset = -0.01f * i; // ensures clickable order
            Vector3 stackedPosition = transform.position + new Vector3(0, stackSpacing * (i + 1), zOffset);
            stackedProducts[i].transform.position = stackedPosition;
        }
    }
}
