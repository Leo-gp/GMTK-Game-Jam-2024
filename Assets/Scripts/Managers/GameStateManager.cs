using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private DayManager dayManager;
    [SerializeField] private CustomerSpawnManager customerSpawnManager;
    [SerializeField] private GameObject shopUICanvasGameObject;

    private void Start()
    {
        EnterBuyingState();
    }

    private void OnEnable()
    {
        customerSpawnManager.AllCustomersHaveLeft += OnAllCustomersHaveLeft;
    }

    private void OnDisable()
    {
        customerSpawnManager.AllCustomersHaveLeft -= OnAllCustomersHaveLeft;
    }

    public void EnterSellingState()
    {
        customerSpawnManager.StartSpawner();
        shopUICanvasGameObject.SetActive(false);
    }

    private void EnterBuyingState()
    {
        dayManager.IncrementDay();
        shopUICanvasGameObject.SetActive(true);
    }

    private void OnAllCustomersHaveLeft()
    {
        EnterBuyingState();
    }
}