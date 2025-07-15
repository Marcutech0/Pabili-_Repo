using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProductUI : MonoBehaviour
{
    public Product product; // Link ScriptableObject here

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
        productImage.sprite = product.productImage;
        productNameText.text = product.productName;
        stockText.text = "Stock: " + product.stock;
    }

    void RestockProduct()
    {
        product.stock += product.restockAmount;
        UpdateUI();
    }
}