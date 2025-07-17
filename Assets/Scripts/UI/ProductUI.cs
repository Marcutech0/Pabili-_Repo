using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProductUI : MonoBehaviour
{
    public ProductData product; // Link products here

    // Product UI Variables
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
        stockText.text = "Stock: " + product.productStock;
    }

    void RestockProduct()
    {
        float restockCost = product.productPrice * product.productRestockAmount * 0.8f; // 20% discount for bulk
        if (CurrencyManager.Instance.SpendFunds(restockCost))
        {
            product.productStock += product.productRestockAmount;
            UpdateUI();
        }
    }
}