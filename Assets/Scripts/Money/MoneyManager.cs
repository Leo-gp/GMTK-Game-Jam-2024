using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private CustomerSpawnManager customerSpawnManager;
    [SerializeField] private MoneyConfiguration moneyConfiguration;
    [SerializeField] private TextMeshProUGUI totalMoneyText;

    private float _totalMoney;

    public static MoneyManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        UpdateTotalMoney(moneyConfiguration.InitialMoney);
    }

    private void OnEnable()
    {
        customerSpawnManager.CustomerLeftStore += OnCustomerLeftStore;
    }

    private void OnDisable()
    {
        customerSpawnManager.CustomerLeftStore -= OnCustomerLeftStore;
    }

    public bool CanPurchaseProduct()
    {
        return moneyConfiguration.ProductPurchaseCost <= _totalMoney;
    }

    public void PurchaseProduct()
    {
        var moneyLeft = _totalMoney - moneyConfiguration.ProductPurchaseCost;
        UpdateTotalMoney(moneyLeft);
    }

    private void UpdateTotalMoney(float money)
    {
        _totalMoney = money;
        SetTotalMoneyText(money);
    }

    private void SetTotalMoneyText(float money)
    {
        totalMoneyText.text = $"TOTAL: ${money:F}";
    }

    private void OnCustomerLeftStore(Customer customer)
    {
        if (!customer.HasBoughtProduct)
        {
            return;
        }
        var moneyLeft = _totalMoney + moneyConfiguration.ProductSellingPrice;
        UpdateTotalMoney(moneyLeft);
    }
}