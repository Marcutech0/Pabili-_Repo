using UnityEngine;

[CreateAssetMenu(fileName = "New Product", menuName = "Shop/Product")]
public class Product : ScriptableObject
{
    public string productName;
    public Sprite productImage;
    public int stock;
    public int restockAmount = 10;
}