using TMPro;
using UnityEngine;

public class ProductDisplay : MonoBehaviour
{
    [Header("References")]
    public GameObject stackTextPrefab;
    private TextMeshProUGUI stackText;
    public Vector3 offset = new Vector3(0, 0.5f, 0);
    public float textScale = 0.1f;

    void Start()
    {
        InitializeStackText();

        // Get initial stock count
        ProductControls productControls = GetComponentInParent<ProductControls>();
        int initialStock = (productControls != null && productControls.productData != null)
            ? productControls.productData.productStock
            : 0;

        UpdateStack(initialStock);
    }

    void InitializeStackText()
    {
        if (stackText != null) return;

        if (stackTextPrefab == null)
        {
            Debug.LogError("StackTextPrefab is not assigned!", this);
            return;
        }

        GameObject textObj = Instantiate(stackTextPrefab, transform);
        textObj.transform.localPosition = offset;
        textObj.transform.localScale = Vector3.one * textScale;

        stackText = textObj.GetComponentInChildren<TextMeshProUGUI>(true);
        if (stackText == null)
        {
            Debug.LogError("No TextMeshProUGUI component found!", this);
            return;
        }

        stackText.gameObject.SetActive(true);
    }

    public void UpdateStack(int count)
    {
        if (stackText == null) InitializeStackText();
        if (stackText == null) return;

        stackText.text = count > 1 ? count.ToString() : "";
    }
}