using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Product : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [SerializeField] private ProductTile[] productTiles;
    [SerializeField] private ProductCategory category;

    private bool _isDragging;
    private Dictionary<ProductTile, GridCell> _placeableCellsAux;

    public bool _isSelled = false;
    public Vector3 WorldPosition;
    
    public Dictionary<ProductTile, GridCell> PlaceableCells { get; private set; }

    private void Start()
    {
        _isDragging = false;
        _placeableCellsAux = new Dictionary<ProductTile, GridCell>();
        PlaceableCells = new Dictionary<ProductTile, GridCell>();
        EnableRaycastTarget();
    }

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

    private void GetProductInfo()
    {
        Vector3 screePosition = GetComponent<RectTransform>().position;
        WorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screePosition.x, screePosition.y, Camera.main.nearClipPlane));
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
        _isPurchasedByPlayer = true;
        GetProductInfo();
        Purchase();
    }

    private void ResetPosition()
    {
        transform.localPosition = Vector3.zero;
    }

    private void Purchase()
    {
        ProductGrid.Instance.PlaceProduct(this);
        foreach (var gridCell in PlaceableCells.Values)
        {
            gridCell.Unhighlight();
        }
        DisableRaycastTarget();
        Restock();
    }

    private void EnableRaycastTarget()
    {
        foreach (var productTile in productTiles)
        {
            productTile.EnableRaycastTarget();
        }
    }

    private void DisableRaycastTarget()
    {
        foreach (var productTile in productTiles)
        {
            productTile.DisableRaycastTarget();
        }
    }

    private void Restock()
    {
        var newProduct = Instantiate(gameObject, transform.parent);
        transform.SetParent(ProductGrid.Instance.transform.parent);
        newProduct.transform.localPosition = Vector3.zero;
        newProduct.name = name;
    }
    
    private bool IsInteractable()
    {
        return !GameManager.Instance.IsPaused && !_isPurchasedByPlayer;
    }
}