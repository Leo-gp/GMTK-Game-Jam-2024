using UnityEngine;

public class CustomerSpawnManager : MonoBehaviour
{
    [SerializeField] private Customer[] customersList;
    [SerializeField] private CustomerConfiguration customerConfiguration;

    public void CallSpawnCustomer()
    {
        InvokeRepeating(nameof(SpawnCustomer), customerConfiguration.SpawnFrequencyTime,
            customerConfiguration.SpawnFrequencyTime);
    }

    private void SpawnCustomer()
    {
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
                return;
            }
        }

        Debug.LogWarning("All customers are active.");
    }
}