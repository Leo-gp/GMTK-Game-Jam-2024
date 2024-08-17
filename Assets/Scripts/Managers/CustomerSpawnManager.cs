using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _customersList;
    [SerializeField] private float _spawnFrequencyTime;
    [SerializeField] private float _spawnCustomerHeight = -3.33f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnCustomer), _spawnFrequencyTime, _spawnFrequencyTime);    
    }

    private void SpawnCustomer()
    {
        int attempts = 0; // Avoid infinty loop

        while (attempts < _customersList.Length)
        {
            int randomIndex = Random.Range(0, _customersList.Length);
            GameObject customer = _customersList[randomIndex];

            if (!customer.activeInHierarchy)
            {
                customer.SetActive(true);
                customer.transform.localPosition = new(customer.transform.localPosition.x, _spawnCustomerHeight);
                break;
            }

            attempts++;
        }

        if (attempts >= _customersList.Length)
        {
            Debug.LogWarning("All customers are active.");
        }
    }
}
