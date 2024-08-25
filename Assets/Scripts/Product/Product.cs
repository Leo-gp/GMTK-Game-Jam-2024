using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Product : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [SerializeField] private ProductTile[] productTiles;
    [SerializeField] private ProductCategory category;

    private readonly Dictionary<ProductTile, GridCell> _placeableCellsAux = new();

    private bool _isDragging;

    public Dictionary<ProductTile, GridCell> PlaceableCells { get; } = new();

    private void Update()
    {
        if (!_isDragging || GameManager.Instance.IsPaused)
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

    public void OnDrag(PointerEventData eventData)
    {
        if (_isDragging || GameManager.Instance.IsPaused)
        {
            return;
        }
        _isDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!_isDragging || GameManager.Instance.IsPaused)
        {
            return;
        }
        Drop();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.Instance.IsPaused)
        {
            return;
        }
        if (_isDragging)
        {
            Drop();
        }
        else
        {
            _isDragging = true;
        }
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

    private void Drop()
    {
        _isDragging = false;
        if (PlaceableCells.Count is 0)
        {
            ResetPosition();
            return;
        }
        ProductGrid.Instance.PlaceProduct(this);
        foreach (var gridCell in PlaceableCells.Values)
        {
            gridCell.Unhighlight();
        }
        DisableRaycastTarget();
    }

    private void ResetPosition()
    {
        transform.localPosition = Vector3.zero;
    }

    private void DisableRaycastTarget()
    {
        foreach (var productTile in productTiles)
        {
            productTile.DisableRaycastTarget();
        }
    }
}