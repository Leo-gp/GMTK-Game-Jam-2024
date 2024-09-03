using System;
using UnityEngine;

public class CustomerSpawnManager : MonoBehaviour
{
    [SerializeField] private Customer[] customersList;
    [SerializeField] private CustomerConfiguration customerConfiguration;

    private int _remainingCustomersToSpawn;
    private int _activeCustomersAmount;

    public Action<Customer> CustomerLeftStore { get; set; }

    public Action AllCustomersHaveLeftStore { get; set; }

    private void OnEnable()
    {
        foreach (var customer in customersList)
        {
            customer.LeftStore += OnCustomerLeftStore;
        }
    }

    private void OnDisable()
    {
        foreach (var customer in customersList)
        {
            customer.LeftStore -= OnCustomerLeftStore;
        }
    }

    public void StartSpawner()
    {
        _remainingCustomersToSpawn = ProductGrid.Instance.ColumnsCount;
        _activeCustomersAmount = 0;
        InvokeRepeating
        (
            nameof(SpawnCustomer),
            customerConfiguration.SpawnFrequencyTime,
            customerConfiguration.SpawnFrequencyTime
        );
    }

    private void SpawnCustomer()
    {
        if (_remainingCustomersToSpawn <= 0)
        {
            CancelInvoke(nameof(SpawnCustomer));
            return;
        }
        for (var i = 0; i < customersList.Length; i++)
        {
            var customer = customersList[i];
            if (!customer.gameObject.activeInHierarchy)
            {
                customer.gameObject.SetActive(true);
                customer.transform.localPosition = new Vector3
                (
                    customer.transform.localPosition.x,
                    customerConfiguration.SpawnCustomerHeightPositionOnScreen
                );
                customer.ReserveProduct();
                _remainingCustomersToSpawn--;
                _activeCustomersAmount++;
                return;
            }
        }
        Debug.LogWarning("All customers are active.");
    }

    private void OnCustomerLeftStore(Customer customer)
    {
        CustomerLeftStore?.Invoke(customer);
        _activeCustomersAmount--;
        if (_remainingCustomersToSpawn > 0 || _activeCustomersAmount > 0)
        {
            return;
        }
        AllCustomersHaveLeftStore?.Invoke();
    }
}