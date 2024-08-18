using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Product : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private ProductGrid productGrid;
    [SerializeField] private ProductTile[] productTiles;

    private readonly Dictionary<ProductTile, GridCell> _placeableCellsAux = new();

    private bool _isPurchased;

    public Dictionary<ProductTile, GridCell> PlaceableCells { get; } = new();

    public void OnDrag(PointerEventData eventData)
    {
        if (_isPurchased)
        {
            return;
        }
        transform.position = Input.mousePosition;
        _placeableCellsAux.Clear();
        foreach (var productTile in productTiles)
        {
            var gridCellHit = productTile.GetGridCellHitAtCenter();
            if (gridCellHit is null || gridCellHit.AttachedProductTile is not null)
            {
                _placeableCellsAux.Clear();
                break;
            }
            _placeableCellsAux.Add(productTile, gridCellHit);
        }
        UpdatePlaceableCells();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_isPurchased || PlaceableCells.Count is 0)
        {
            return;
        }
        productGrid.PlaceProduct(this);
        foreach (var gridCell in PlaceableCells.Values)
        {
            gridCell.Unhighlight();
        }
        _isPurchased = true;
    }

    private void UpdatePlaceableCells()
    {
        if (PlaceableCells.SequenceEqual(_placeableCellsAux))
        {
            return;
        }
        foreach (var gridCell in PlaceableCells.Values)
        {
            gridCell.Unhighlight();
        }
        PlaceableCells.Clear();
        foreach (var (productTile, gridCell) in _placeableCellsAux)
        {
            PlaceableCells.Add(productTile, gridCell);
            gridCell.Highlight();
        }
    }
}