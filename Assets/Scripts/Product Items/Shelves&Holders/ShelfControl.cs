using UnityEngine;
using System.Collections.Generic;

public class ShelfControl : MonoBehaviour, ProductDropZone
{
    private List<ProductControls> stackedProducts = new List<ProductControls>();

    [Header("Stack Settings")]
    public float stackSpacing = 0.3f;        // Space between stacked items vertically
    public int maxStackSize = 5;            // Maximum items allowed on the shelf

    public void OnProductDrop(ProductControls _product)
    {
        // Remove any null (destroyed) products from the list
        stackedProducts.RemoveAll(p => p == null);
        Debug.Log("Cleaned shelf. Current count: " + stackedProducts.Count);


        // Prevent duplicates
        if (stackedProducts.Contains(_product))
        {
            Debug.Log("This product is already on the shelf.");
            return;
        }

        // Shelf full check
        if (stackedProducts.Count >= maxStackSize)
        {
            Debug.Log("Shelf is full! Returning item.");
            _product.ResetToStartPosition();
            return;
        }

        // Add product
        stackedProducts.Add(_product);
        _product.transform.SetParent(transform);

        RestackItems();
        Debug.Log("Product stacked on shelf. Total: " + stackedProducts.Count);
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
            float zOffset = -0.01f * i; // ensures clickable layering
            Vector3 stackedPosition = transform.position + new Vector3(0, stackSpacing * (i + 1), zOffset);
            stackedProducts[i].transform.position = stackedPosition;
        }
    }
}
