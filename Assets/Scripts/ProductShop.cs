using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
public class ProductShop : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public GameObject productShopUI;
    [System.Serializable]
    public class ShopProductUI
    {
        public Product product;
        public Button buyButton;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI priceText;
        public Image iconImage;
    }

    public List<ShopProductUI> shopProducts = new List<ShopProductUI>();

    private void Start()
    {
        foreach (var shopItem in shopProducts)
        {
            //UpdateUI(shopItem);
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

    void BuyProduct(Product product)
    {
        // Expand this logic later (e.g., check currency, stack limits)
        Debug.Log("Buying: " + product.productName);
        inventoryManager.AddProduct(product);
    }

    public void CloseProductShopUI()
    {
        productShopUI.gameObject.SetActive(false);
    }

    public void OpenProductShopUI()
    {
        productShopUI.gameObject.SetActive(true);
    }
}

