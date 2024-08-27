using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProductData", menuName = "ScriptableObjects/ProductData", order = 1)]

public class ProductData : ScriptableObject
{
    public enum ProductCategories
    {
        Surrealism,
        Cubism,
        Impressionism
    }

    [SerializeField] private float price;
    [SerializeField] private ProductCategories category;

    public float Price => price;
    public ProductCategories Category => category;
}
