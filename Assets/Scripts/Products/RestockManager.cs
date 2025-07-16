using UnityEngine;

public class RestockManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject coffeePrefab;
    public GameObject candyPrefab;

    [Header("Shelf Slots")]
    public Transform coffeeSlot;  // Assign Slot1
    public Transform candySlot;   // Assign Slot2

    [Header("Settings")]
    public int maxPerSlot = 5;
    public float verticalOffset = 0.2f;

    public void RestockCoffee()
    {
        if (coffeeSlot.childCount >= maxPerSlot)
        {
            Debug.Log("Coffee shelf is full!");
            return;
        }

        Vector3 spawnPos = coffeeSlot.position + new Vector3(0, coffeeSlot.childCount * verticalOffset, 0);
        Instantiate(coffeePrefab, spawnPos, Quaternion.identity, coffeeSlot);
    }

    public void RestockCandy()
    {
        if (candySlot.childCount >= maxPerSlot)
        {
            Debug.Log("Candy shelf is full!");
            return;
        }

        Vector3 spawnPos = candySlot.position + new Vector3(0, candySlot.childCount * verticalOffset, 0);
        Instantiate(candyPrefab, spawnPos, Quaternion.identity, candySlot);
    }
}