using UnityEngine;

[CreateAssetMenu(menuName = "ShopSimulator/Product")]
public class ProductData : ScriptableObject
{
    public enum ProductCategory
    {
        Fish_and_Meat,       // Tuyo, Egg, etc.
        Fruits_and_Vegetables,       // Onion, Tomato, etc.
        Condiments_and_Seasoning,        // Soy sauce, Vinegar, etc.
        Instant_Food_and_Snacks,       // Pancit Canton, Cream-O, etc.
        Drinks,    // Soda, Fruit Juice, etc.
        Alcohol,   // Red Hourse, Gin, etc.
        Desserts,   // Ice candy, Ice cream, etc.
        Hygiene,   // Toothbrush, Bar soap, etc.
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

    [Header("Inventory")]
    public int productStock;
    public int productRestockAmount;
    public int initialStock = 0;
    public int productMaxStack = 5;

    [System.NonSerialized] public int spawnedCount = 0;

    public void FullReset()
    {
        spawnedCount = 0;
        productStock = initialStock;  // Reset to initial value
    #if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);  // Mark as dirty to save changes
    #endif
    }

    public bool CanAddToStock(int amountToAdd)
    {
        // Optional: Add this method to validate before restocking
        return productStock + amountToAdd <= productMaxStack;
    }

    public bool IsAvailable()
    {
        return (productStock > spawnedCount) && (spawnedCount < productMaxStack);
    }

    public bool CanSpawnMore()
    {
        return spawnedCount < productStock && spawnedCount < productMaxStack;
    }

    public bool CanSell()
    {
        return productStock > 0;
    }

    public System.Action OnStockChanged;

    public void ModifyStock(int amount)
    {
        productStock = Mathf.Clamp(productStock + amount, 0, productMaxStack);
        OnStockChanged?.Invoke();
    }
}