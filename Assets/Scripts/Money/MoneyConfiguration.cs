using UnityEngine;

[CreateAssetMenu(fileName = "MoneyConfiguration", menuName = "ScriptableObjects/MoneyConfiguration")]
public class MoneyConfiguration : ScriptableObject
{
    [SerializeField] private float initialMoney;
    [SerializeField] private float productPurchaseCost;
    [SerializeField] private float productSellingPrice;

    public float InitialMoney => initialMoney;

    public float ProductPurchaseCost => productPurchaseCost;

    public float ProductSellingPrice => productSellingPrice;
}