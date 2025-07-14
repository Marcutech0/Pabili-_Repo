using UnityEngine;

[CreateAssetMenu(fileName = "NewProduct", menuName = "Shop/Product")]
public class Product : ScriptableObject
{
    public string productName;
    public Sprite productImage;
    public string description;
    public float price;
    public int maxStack = 1;
}
