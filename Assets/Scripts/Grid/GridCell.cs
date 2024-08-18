using UnityEngine;
using UnityEngine.UI;

public class GridCell : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Color unhighlightedColor;
    [SerializeField] private Color highlightedColor;

    public ProductTile AttachedProductTile { get; set; }

    public void Highlight()
    {
        image.color = highlightedColor;
    }

    public void Unhighlight()
    {
        image.color = unhighlightedColor;
    }
}