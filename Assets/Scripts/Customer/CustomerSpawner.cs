using UnityEngine;
using System.Collections;

public class CustomerSpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject[] customerPrefabs; // Put both prefabs here
    public Transform[] spawnPoints;
    public ProductData[] availableProducts;

    private CustomerAI currentCustomer;

    void Start()
    {
        SpawnCustomer();
    }

    public void SpawnCustomer()
    {
        if (currentCustomer != null) return;

        // Pick a random prefab and spawn point
        GameObject randomPrefab = customerPrefabs[Random.Range(0, customerPrefabs.Length)];
        Transform randomSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject newCustomer = Instantiate(randomPrefab, randomSpawn.position, Quaternion.identity);
        currentCustomer = newCustomer.GetComponent<CustomerAI>();

        if (currentCustomer != null && availableProducts.Length > 0)
        {
            ProductData randomProduct = availableProducts[Random.Range(0, availableProducts.Length)];
            currentCustomer.RequestProduct(randomProduct);

            StartCoroutine(WaitUntilCustomerIsServed());
        }
    }

    private IEnumerator WaitUntilCustomerIsServed()
    {
        yield return new WaitUntil(() => currentCustomer.isServed);
        yield return new WaitForSeconds(1.5f);

        Destroy(currentCustomer.gameObject);
        currentCustomer = null;

        yield return new WaitForSeconds(1f); // Delay before spawning the next one
        SpawnCustomer();
    }
}