using UnityEngine;

[CreateAssetMenu(menuName = "ShopSimulator/Product")]
public class ProductData : ScriptableObject
{
    public enum ProductCategory
    {
        Fish_and_Meat,
        Fruits_and_Vegetables,
        Condiments_and_Seasoning,
        Instant_Food_and_Snacks,
        Drinks,
        Alcohol,
        Desserts,
        Hygiene,
    }

    [Header("Basic Info")]
    public string productName;
    public string productDescription;
    public Sprite productIcon;

    [Header("Category")]
    public ProductCategory category;

    [Header("Pricing")]
    public int productPrice;
    public int restockPrice;

    [Header("Inventory Settings")]
    public int productRestockAmount = 1;
    public int initialStock = 0;
    public int productMaxStack = 5;

    [Header("Display Settings")]
    public Color priceColor = Color.white;
    public Color nameColor = Color.white;
    [Range(0.5f, 2f)] public float textScale = 1f;

    [System.NonSerialized] public int spawnedCount = 0;
    public System.Action OnStockChanged;

    private int _productStock;
    public int productStock
    {
        get => _productStock;
        private set
        {
            int newValue = Mathf.Clamp(value, 0, productMaxStack);
            if (_productStock != newValue)
            {
                _productStock = newValue;
                OnStockChanged?.Invoke();
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
#endif
            }
        }
    }

    void OnEnable()
    {
        _productStock = initialStock;
        spawnedCount = 0;
    }

    public void FullReset()
    {
        productStock = initialStock;
        spawnedCount = 0;
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }

    public void ModifyStock(int amount)
    {
        int newStock = Mathf.Clamp(productStock + amount, 0, productMaxStack);
        if (productStock != newStock)
        {
            productStock = newStock;
            OnStockChanged?.Invoke();
        }
    }
    public bool CanAddToStock(int amountToAdd)
    {
        return productStock + amountToAdd <= productMaxStack;
    }

    public bool IsAvailable()
    {
        return productStock > 0;
    }
}