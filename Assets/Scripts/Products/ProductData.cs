using UnityEngine;

[CreateAssetMenu(menuName = "ShopSimulator/Product")]
public class ProductData : ScriptableObject
{
    //Product identifiers
    public string productName;
    public string productDescription;
    public Sprite productIcon;

    //Product variables
    public float productPrice;
    public int productStock;
    public int productMaxStack = 1;
    public int productRestockAmount = 10;
}