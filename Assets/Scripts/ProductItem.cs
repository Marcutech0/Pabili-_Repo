using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProductItem : MonoBehaviour
{
    public Product productData;
    public Image productImage;
    public TMP_Text productNameText;
    public TMP_Text priceText;

    private void Start()
    {
        if (productData != null)
            UpdateUI();
    }

    public void UpdateUI()
    {
        if (productImage != null)
            productImage.sprite = productData.productImage;

        if (productNameText != null)
            productNameText.text = productData.productName;

        if (priceText != null)
            priceText.text = "$" + productData.price.ToString("F2");
    }
}
