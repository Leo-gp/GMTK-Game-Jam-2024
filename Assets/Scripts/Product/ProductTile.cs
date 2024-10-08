using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ProductTile : MonoBehaviour
{
    [SerializeField] private Image image;

    public Vector3 OriginalLocalPosition { get; private set; }

    private void Start()
    {
        OriginalLocalPosition = transform.localPosition;
    }

    public GridCell GetGridCellHitAtCenter()
    {
        var pointerEventData = new PointerEventData(null)
        {
            position = transform.position
        };
        var raycastResults = new List<RaycastResult>();
        ProductGrid.Instance.GraphicRaycaster.Raycast(pointerEventData, raycastResults);
        foreach (var raycastResult in raycastResults)
        {
            if (raycastResult.gameObject.TryGetComponent<GridCell>(out var gridCell))
            {
                return gridCell;
            }
        }
        return null;
    }

    public void EnableRaycastTarget()
    {
        image.raycastTarget = true;
    }

    public void DisableRaycastTarget()
    {
        image.raycastTarget = false;
    }
}