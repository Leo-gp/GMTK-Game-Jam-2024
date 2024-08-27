using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Customer : MonoBehaviour
{
    [SerializeField] private CustomerConfiguration customerConfiguration;
    [SerializeField] private float despawnCustomerPointOnX = 11f;
    [SerializeField] private GameObject avaliableProducts;

    private Product[] products;
    private List<Product> desiredProducts = new();
    private Product productBought;
    private float productXPosition;
    private float productPrice;
    private CustomerConfiguration.CustomerCategories desiredCategory;
    private SpriteRenderer spriteRenderer;

    public bool IsHoldingProduct = false;

    private void Start()
    {
        gameObject.SetActive(false);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = customerConfiguration.CustomerColor;
    }

    private void Update()
    {
        transform.Translate(Vector2.right * (customerConfiguration.WalkSpeed * Time.deltaTime));

        if (transform.localPosition.x > despawnCustomerPointOnX)
        {
            if (productBought != null)
            {
                PayPlayer();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        GetProductFromGrid();      

    }

    public void ReserveProduct()
    {

        products = avaliableProducts.GetComponentsInChildren<Product>();
        desiredCategory = customerConfiguration.DesiredCategory;

        if (products == null)
        {
            Debug.Log("There is no product available");
            return;
        }

        for (var i = 0; i < products.Length; i++)
        {
            // verifies if there is products from his category available
            if (products[i].Category.ToString() == desiredCategory.ToString() && !products[i]._isSelled)
            {
                desiredProducts.Add(products[i]);
            }
        }

        if (desiredProducts.Count <= 0)
        {
            Debug.Log("No product available for: " + gameObject.name);
        }
        else
        {
            int randomIndex = Random.Range(0, desiredProducts.Count);

            desiredProducts[randomIndex]._isSelled = true;
            productBought = desiredProducts[randomIndex];
            productPrice = desiredProducts[randomIndex].Price;
            productXPosition = desiredProducts[randomIndex].transform.position.x;

            Debug.Log(gameObject.name + ", type: "+ desiredCategory + ", reserved: " + productBought + " - "+ productBought.Category+ ", for: $" + productPrice * 2);
        }

    }

    private void GetProductFromGrid()
    {
        // when customer walks by product, make the product disappear from grid and show on customer
        if (productBought && transform.position.x >= productBought.WorldPosition.x)
        {
            IsHoldingProduct = true;

            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            productBought.GetComponent<RectTransform>().position = screenPosition; ;
        }
    }

    private void PayPlayer()
    {
        // when custormer is gone, pay the product price to player
        Debug.Log($"player won $ {productPrice * 2}");
        IsHoldingProduct = false;

        // return product to pool
        productBought._isSelled = false;
        productBought.gameObject.SetActive(false);

        gameObject.SetActive(false);

    }

    private void OnDisable()
    {
        transform.position = new(-10f, transform.position.y);
        desiredProducts.Clear();
        productBought = null;
        productPrice = 0;
    }
}
