using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] private CustomerConfiguration customerConfiguration;
    [SerializeField] private float despawnCustomerPointOnX = 11f;

    private readonly List<Product> _desiredProducts = new();

    private Product _reservedProduct;
    private bool _isCarryingProduct;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * (customerConfiguration.WalkSpeed * Time.deltaTime));

        if (ShouldStartCarryingProduct())
        {
            ProductGrid.Instance.RemoveProduct(_reservedProduct);
            _reservedProduct.StartFollowingTargetWorldSpace(transform);
            _isCarryingProduct = true;
        }

        if (transform.localPosition.x > despawnCustomerPointOnX)
        {
            Despawn();
        }
    }

    private void OnDisable()
    {
        transform.position = new Vector3(-10f, transform.position.y);
        _desiredProducts.Clear();
        _reservedProduct = null;
        _isCarryingProduct = false;
    }

    public void ReserveProduct()
    {
        foreach (var product in ProductGrid.Instance.Products)
        {
            if (product.Category == customerConfiguration.DesiredCategory && !product.IsReserved)
            {
                _desiredProducts.Add(product);
            }
        }
        if (_desiredProducts.Count <= 0)
        {
            return;
        }
        var randomIndex = Random.Range(0, _desiredProducts.Count);
        _desiredProducts[randomIndex].IsReserved = true;
        _reservedProduct = _desiredProducts[randomIndex];
    }

    private bool ShouldStartCarryingProduct()
    {
        return !_isCarryingProduct &&
               _reservedProduct is not null &&
               _reservedProduct.WorldPosition.x < transform.position.x;
    }

    private void Despawn()
    {
        if (_reservedProduct is not null)
        {
            _reservedProduct.Deactivate();
        }
        gameObject.SetActive(false);
    }
}