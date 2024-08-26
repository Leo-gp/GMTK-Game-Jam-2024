using UnityEngine;

[CreateAssetMenu(fileName = "ProductGridConfiguration", menuName = "ScriptableObjects/ProductGridConfiguration")]
public class ProductGridConfiguration : ScriptableObject
{
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private Vector2 cellSize;

    public Vector2Int GridSize => gridSize;

    public Vector2 CellSize => cellSize;
}