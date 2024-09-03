using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Product : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [SerializeField] private ProductTile[] productTiles;
    [SerializeField] private ProductCategory category;

    private bool _isDragging;
    private Transform _targetToFollow;
    private Dictionary<ProductTile, GridCell> _placeableCellsAux;

    public Dictionary<ProductTile, GridCell> PlaceableCells { get; private set; }

    public bool IsReserved { get; set; }

    public ProductCategory Category => category;

    public Vector2 WorldPosition => CameraManager.Instance.Camera.ScreenToWorldPoint(transform.position);

    private void Start()
    {
        _isDragging = false;
        _targetToFollow = null;
        IsReserved = false;
        _placeableCellsAux = new Dictionary<ProductTile, GridCell>();
        PlaceableCells = new Dictionary<ProductTile, GridCell>();
        EnableRaycastTarget();
    }

    private void Update()
    {
        if (GameManager.Instance.IsPaused)
        {
            return;
        }
        if (!IsReserved && _isDragging && MoneyManager.Instance.CanPurchaseProduct())
        {
            UpdateDrag();
        }
        else if (_targetToFollow is not null)
        {
            FollowTarget();
        }
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

    public void StartFollowingTargetWorldSpace(Transform target)
    {
        _targetToFollow = target;
    }

    public void Deactivate()
    {
        IsReserved = false;
        gameObject.SetActive(false);
    }

    private void UpdateDrag()
    {
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
        MoneyManager.Instance.PurchaseProduct();
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

    private void FollowTarget()
    {
        var targetScreenPosition = CameraManager.Instance.Camera.WorldToScreenPoint(_targetToFollow.position);
        transform.position = targetScreenPosition;
    }

    private void Restock()
    {
        var newProduct = Instantiate(gameObject, transform.parent);
        transform.SetParent(ProductGrid.Instance.transform.parent);
        newProduct.transform.localPosition = Vector3.zero;
        newProduct.name = name;
    }
}