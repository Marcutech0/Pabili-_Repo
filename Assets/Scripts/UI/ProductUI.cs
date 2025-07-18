using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProductUI : MonoBehaviour
{
    public ProductData product;
    public RestockManager restockManager;

    [Header("UI References")]
    public Image productImage;
    public TMP_Text productNameText;
    public TMP_Text stockText;
    public Button buyButton;

    void Start()
    {
        UpdateUI();
        buyButton.onClick.AddListener(RestockProduct);
    }

    void UpdateUI()
    {
        productImage.sprite = product.productIcon;
        productNameText.text = product.productName;
        stockText.text = $"In Stock: {product.productStock}/{product.productMaxStack}";
        buyButton.interactable = product.productStock < product.productMaxStack;
    }

    void RestockProduct()
    {
        if (restockManager != null)
        {
            restockManager.RestockProduct(product);
            UpdateUI();
        }
    }
}