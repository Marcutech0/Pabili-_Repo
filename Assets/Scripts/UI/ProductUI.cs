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
        // Use restockPrice instead of calculating it
        float restockCost = product.restockPrice;

        if (CurrencyManager.Instance.SpendFunds(restockCost))
        {
            product.productStock += product.productRestockAmount;
            UpdateUI();
            Debug.Log($"Restocked {product.productRestockAmount} {product.productName} for {restockCost}");
        }
        else
        {
            Debug.LogWarning($"Not enough money to restock {product.productName}");
        }
    }
}