using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
public class ProductShop : MonoBehaviour
{
    public InventoryManager inventoryManager;   // Callout of "Inventory Manager"
    public GameObject productShopUI;    // Turns script into a GameObject

    [System.Serializable]

    //Add ShopProductUI into its own group for list generation
    public class ShopProductUI
    {
        public ProductData product;
        public Button buyButton;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI priceText;
        public Image iconImage;
    }

    public List<ShopProductUI> shopProducts = new();

    private void Start()
    {
        // Allows for buying products
        foreach (var shopItem in shopProducts)
        {
            shopItem.buyButton.onClick.AddListener(() => BuyProduct(shopItem.product));
        }
    }

    /*void UpdateUI(ShopProductUI item)
    {
        item.nameText.text = item.product.productName;
        item.priceText.text = "$" + item.product.price.ToString();
        if (item.iconImage != null && item.product.productImage != null)
            item.iconImage.sprite = item.product.productImage;
    } */

    void BuyProduct(ProductData product)
    {
        // Expand this logic later (e.g., check currency, stack limits)
        Debug.Log("Buying: " + product.productName);
        inventoryManager.AddProduct(product);
    }

    public void OpenProductShopUI()
    {
        productShopUI.SetActive(true);
    }
    public void CloseProductShopUI()
    {
        productShopUI.SetActive(false);
    }
}