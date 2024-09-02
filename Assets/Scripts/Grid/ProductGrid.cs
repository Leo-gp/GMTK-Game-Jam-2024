using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ProductGrid : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    [SerializeField] private GridCell gridCellPrefab;
    [SerializeField] private ProductGridConfiguration productGridConfiguration;
    [SerializeField] private GraphicRaycaster graphicRaycaster;

    public GraphicRaycaster GraphicRaycaster => graphicRaycaster;

    public int ColumnsCount { get; private set; }

    public static ProductGrid Instance { get; private set; }

    public List<Product> Products { get; } = new();

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
        Initialize();
    }

    public void PlaceProduct(Product product)
    {
        foreach (var (productTile, gridCell) in product.PlaceableCells)
        {
            gridCell.AttachedProductTile = productTile;
            productTile.transform.position = gridCell.transform.position;
        }
        var firstTile = product.PlaceableCells.First().Key;
        var diff = firstTile.transform.localPosition - firstTile.OriginalLocalPosition;
        product.transform.localPosition += diff;
        foreach (var productTile in product.PlaceableCells.Keys)
        {
            productTile.transform.localPosition = productTile.OriginalLocalPosition;
        }
        Products.Add(product);
    }

    public void RemoveProduct(Product product)
    {
        foreach (var gridCell in product.PlaceableCells.Values)
        {
            gridCell.AttachedProductTile = null;
        }
        Products.Remove(product);
    }

    private void Initialize()
    {
        gridLayoutGroup.cellSize = productGridConfiguration.CellSize;
        gridLayoutGroup.constraintCount = productGridConfiguration.InitialGridSize.y;
        ColumnsCount = productGridConfiguration.InitialGridSize.x;
        for (var y = 0; y < productGridConfiguration.InitialGridSize.y; y++)
        {
            for (var x = 0; x < productGridConfiguration.InitialGridSize.x; x++)
            {
                var cell = Instantiate(gridCellPrefab, transform);
                cell.name = $"GridCell [{x}, {y}]";
            }
        }
    }
}