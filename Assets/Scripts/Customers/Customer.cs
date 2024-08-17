using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _despawnCustomerPointOnX = 11f;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * (_walkSpeed * Time.deltaTime));

        if (transform.localPosition.x > _despawnCustomerPointOnX)
        {
            gameObject.SetActive(false);
        }
    }
}
