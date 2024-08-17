using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] customersList;
    [SerializeField] private CustomerConfiguration customerConfiguration;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnCustomer), customerConfiguration.SpawnFrequencyTime, customerConfiguration.SpawnFrequencyTime);    
    }

    private void SpawnCustomer()
    {
        int attempts = 0; // Avoid infinty loop

        while (attempts < customersList.Length)
        {
            int randomIndex = Random.Range(0, customersList.Length);
            GameObject customer = customersList[randomIndex];

            if (!customer.activeInHierarchy)
            {
                customer.SetActive(true);
                customer.transform.localPosition = new(customer.transform.localPosition.x, customerConfiguration.SpawnCustomerHeightPositionOnScreen);

                Debug.Log(customer.gameObject + " - transform: " + customer.transform.position);
                break;
            }

            attempts++;
        }

        if (attempts >= customersList.Length)
        {
            Debug.LogWarning("All customers are active.");
        }
    }
}
