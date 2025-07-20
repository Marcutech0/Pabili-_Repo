using UnityEngine;

public class CustomerDropZone : MonoBehaviour, ProductDropZone
{
    [Header("References")]
    public CustomerAI customer;

    public void OnProductDrop(ProductControls product)
    {
        if (customer == null || product == null || product.productData == null)
        {
            product.ReturnToShelf();
            return;
        }

        if (customer.ReceiveProduct(product.productData))
        {
            int productPrice = product.productData.productPrice;
            CashierUI.Instance.OpenUI(customer.moneyGiven, productPrice);
            CashierUI.Instance.currentCustomer = customer;
            CashierUI.Instance.currentProductGO = product.gameObject;
            product.gameObject.SetActive(false);
        }
        else
        {
            product.ReturnToShelf();
        }
    }
}