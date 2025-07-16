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
        product.productStock += product.productRestockAmount;
        UpdateUI();
    }
}