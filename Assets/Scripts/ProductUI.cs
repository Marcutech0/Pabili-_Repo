using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProductUI : MonoBehaviour
{
    public Product product;

    public Image productImage;
    public TMP_Text productNameText;
    public TMP_Text stockText;
    public Button buyButton;

    void Start()
    {
        product.ResetStock(); 
        UpdateUI();
        buyButton.onClick.AddListener(RestockProduct);
    }

    void UpdateUI()
    {
        productNameText.text = product.productName;
        productImage.sprite = product.productImage;
        stockText.text = "Stock: " + product.stock;
    }

    void RestockProduct()
    {
        product.stock += product.restockAmount;
        UpdateUI();
    }
}