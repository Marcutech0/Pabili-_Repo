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
    public float restockPrice;
    public int productStock;
    public int productRestockAmount;
    public int productMaxStack = 1;
}