using UnityEngine;
using UnityEngine.UI;

public class ProductGrid : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    [SerializeField] private GridCell gridCellPrefab;
    [SerializeField] private ProductGridConfiguration productGridConfiguration;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        gridLayoutGroup.cellSize = productGridConfiguration.CellSize;
        gridLayoutGroup.spacing = productGridConfiguration.CellGap;
        gridLayoutGroup.constraintCount = productGridConfiguration.GridSize.y;

        for (var y = 0; y < productGridConfiguration.GridSize.y; y++)
        {
            for (var x = 0; x < productGridConfiguration.GridSize.x; x++)
            {
                var cell = Instantiate(gridCellPrefab, transform);
                cell.name = $"GridCell [{x}, {y}]";
            }
        }
    }
}