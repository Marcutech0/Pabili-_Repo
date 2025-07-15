using UnityEngine;

[CreateAssetMenu(fileName = "NewProducts", menuName = "Shop/Product")]
public class Products : ScriptableObject
{
    public string productName;
    public Sprite productImage;
    public string description;
    public float price;
    public int maxStack = 1;
}
