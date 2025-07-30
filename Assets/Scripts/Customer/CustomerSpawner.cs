using UnityEngine;
using System.Collections;

public class CustomerSpawner : MonoBehaviour
{
    [Header("Customer Prefabs")]
    public GameObject[] customerPrefabs;

    [Header("Other")]
    public Transform spawnPoint;
    public ProductData[] availableProducts;

    private CustomerAI currentCustomer;

    public void SpawnCustomer()
    {
        if (currentCustomer != null) return;

        GameObject prefabToSpawn = customerPrefabs[Random.Range(0, customerPrefabs.Length)];

        GameObject newCustomer = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);
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

        yield return new WaitForSeconds(1.5f); // Small delay before despawn

        Destroy(currentCustomer.gameObject);
        currentCustomer = null;

        yield return new WaitForSeconds(1f); // Delay before next customer
        SpawnCustomer();
    }

    public void SpawnCustomerButtnon()
    {
        SpawnCustomer();
    }   
}