using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] private CustomerConfiguration customerConfiguration;
    [SerializeField] private float despawnCustomerPointOnX = 11f;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * (customerConfiguration.WalkSpeed * Time.deltaTime));

        if (transform.localPosition.x > despawnCustomerPointOnX)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        transform.position = new(-10f, transform.position.y);
    }
}
