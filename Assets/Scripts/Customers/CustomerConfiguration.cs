using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CustomerConfiguration", menuName = "ScriptableObjects/CustomerConfiguration", order = 1)]

public class CustomerConfiguration : ScriptableObject
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float spawnFrequencyTime;
    [SerializeField] private float spawnCustomerHeightPositionOnScreen = -3.33f;

    public float WalkSpeed => walkSpeed;
    public float SpawnFrequencyTime => spawnFrequencyTime;
    public float SpawnCustomerHeightPositionOnScreen => spawnCustomerHeightPositionOnScreen;
}

