using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawnManager : MonoBehaviour
{
    [SerializeField] private Customer[] customersList;
    [SerializeField] private CustomerConfiguration customerConfiguration;

    private void Start()
    {

    }

    public void CallSpawnCustomer()
    {
        InvokeRepeating(nameof(SpawnCustomer), customerConfiguration.SpawnFrequencyTime, customerConfiguration.SpawnFrequencyTime);
    }

    private void SpawnCustomer()
    {
        for (int i = 0; i < customersList.Length; i++)
        {
            GameObject customer = customersList[i].gameObject;

            if (!customer.activeInHierarchy)
            {
                customer.SetActive(true);
                customer.transform.localPosition = new(customer.transform.localPosition.x, customerConfiguration.SpawnCustomerHeightPositionOnScreen);

                return;
            }
        }

        Debug.LogWarning("All customers are active.");
    }
}
