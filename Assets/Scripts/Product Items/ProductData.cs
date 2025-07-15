using UnityEngine;

[CreateAssetMenu(menuName = "ShopSimulator/Product")]
public class ProductData : ScriptableObject
{
    public string productName;
    public float price;
    public Sprite icon;
}
