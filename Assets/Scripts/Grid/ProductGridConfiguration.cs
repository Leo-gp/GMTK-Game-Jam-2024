using UnityEngine;

[CreateAssetMenu(fileName = "ProductGridConfiguration", menuName = "ScriptableObjects/ProductGridConfiguration")]
public class ProductGridConfiguration : ScriptableObject
{
    [SerializeField] private Vector2Int initialGridSize;
    [SerializeField] private Vector2 cellSize;

    public Vector2Int InitialGridSize => initialGridSize;

    public Vector2 CellSize => cellSize;
}