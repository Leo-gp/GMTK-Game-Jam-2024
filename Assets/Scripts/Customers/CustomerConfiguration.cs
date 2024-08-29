using UnityEngine;

[CreateAssetMenu(fileName = "CustomerConfiguration", menuName = "ScriptableObjects/CustomerConfiguration", order = 1)]
public class CustomerConfiguration : ScriptableObject
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float spawnFrequencyTime;
    [SerializeField] private float spawnCustomerHeightPositionOnScreen = -3.33f;
    [SerializeField] private ProductCategory desiredCategory;

    public float WalkSpeed => walkSpeed;

    public float SpawnFrequencyTime => spawnFrequencyTime;

    public float SpawnCustomerHeightPositionOnScreen => spawnCustomerHeightPositionOnScreen;

    public ProductCategory DesiredCategory => desiredCategory;
}