using UnityEngine;

public class RestockManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject coffeePrefab;
    public GameObject candyPrefab;

    [Header("Shelf Slots")]
    public Transform coffeeSlot;  // assign Slot 1
    public Transform candySlot;   // assign Slot 2

    [Header("Settings")]
    public int maxPerSlot = 5;
    public float verticalOffset = 0.2f;

    public void RestockCoffee()
    {
        ProductData coffeeData = coffeePrefab.GetComponent<ProductControls>().productData;
        // Use the restockPrice from ProductData instead of hardcoded 10
        float cost = coffeeData.restockPrice;

        if (CurrencyManager.Instance.SpendFunds(cost))
        {
            for (int i = 0; i < coffeeData.productRestockAmount; i++)
            {
                Vector3 spawnPos = coffeeSlot.position + new Vector3(0, coffeeSlot.childCount * verticalOffset, 0);
                Instantiate(coffeePrefab, spawnPos, Quaternion.identity, coffeeSlot);
            }
        }
    }

    public void RestockCandy()
    {
        ProductData candyData = candyPrefab.GetComponent<ProductControls>().productData;
        float cost = candyData.restockPrice;

        if (candySlot.childCount >= maxPerSlot)
        {
            Debug.Log("Candy shelf is full!");
            return;
        }

        if (CurrencyManager.Instance.SpendFunds(cost))
        {
            for (int i = 0; i < candyData.productRestockAmount; i++)
            {
                Vector3 spawnPos = candySlot.position + new Vector3(0, candySlot.childCount * verticalOffset, 0);
                Instantiate(candyPrefab, spawnPos, Quaternion.identity, candySlot);
            }
        }
    }
}