using UnityEngine;

[CreateAssetMenu(fileName = "New Product", menuName = "Shop/Product")]
public class Product : ScriptableObject
{
    public string productName;
    public Sprite productImage;

    [Header("Stock Settings")]
    public int startingStock = 0;      
    [HideInInspector] public int stock; 

    public int restockAmount = 10;

    
    public void ResetStock()
    {
        stock = startingStock;
    }
}