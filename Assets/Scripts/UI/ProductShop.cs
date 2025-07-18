using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
public class ProductShop : MonoBehaviour
{
    [Header("Debug")]
    public bool enableDebugLogs = true; // Toggle in Inspector

    public InventoryManager inventoryManager;   // Callout of "Inventory Manager"
    public GameObject productShopUI;    // Turns script into a GameObject

    // Allows debug logs for non-game breaking errors
    private void Log(string message)
    {
        if (enableDebugLogs) Debug.Log(message);
    }

    private void LogWarning(string message)
    {
        if (enableDebugLogs) Debug.LogWarning(message);
    }

    private void LogError(string message)
    {
        if (enableDebugLogs) Debug.LogError(message);
    }

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

    void BuyProduct(ProductData product)
    {
        if (CurrencyManager.Instance != null)
        {
            // Use product.productPrice instead of any other field
            if (CurrencyManager.Instance.SpendFunds(product.productPrice))
            {
                inventoryManager.AddProduct(product);
                Log($"Purchased: {product.productName} for {CurrencyManager.Instance.currencySymbol}{product.productPrice:F2}");

                // Optional: Spawn physical product if needed
                // Instantiate(productPrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                LogWarning($"Not enough money to buy {product.productName} (Cost: {product.productPrice}, Have: {CurrencyManager.Instance.GetCurrentBalance()})");
            }
        }
        else
        {
            LogError("CurrencyManager instance not found!");
        }
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