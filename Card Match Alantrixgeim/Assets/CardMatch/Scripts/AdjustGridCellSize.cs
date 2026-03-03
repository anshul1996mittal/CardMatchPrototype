using UnityEngine.UI;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(UnityEngine.UI.GridLayoutGroup))]
public class AdjustGridCellSize : MonoBehaviour
{
    GridLayoutGroup _gridlayout;
    private GridLayoutGroup gridlayout
    {
        get
        {
            if (!_gridlayout)
                _gridlayout = GetComponent<GridLayoutGroup>();
            return _gridlayout;
        }
    }

    RectTransform _layoutRect;
    RectTransform layoutRect { 
        get
        {
            if (!_layoutRect)
                _layoutRect = GetComponent<RectTransform>();
            return _layoutRect;
        }
}
    
    public void UpdateCellSize(int columnCount, int rowCount)
    {
        float spaceForSpacing = (columnCount - 1) * gridlayout.spacing.y;
        float width = layoutRect.rect.width - spaceForSpacing;
        float xSizePerCell = width / columnCount;

        float height = layoutRect.rect.height - spaceForSpacing;
        float ySizePerCell = height / rowCount;
        gridlayout.cellSize = new Vector2(ySizePerCell, ySizePerCell);

        var rect = layoutRect.rect;
        rect.width = (ySizePerCell * columnCount) + spaceForSpacing;
        layoutRect.sizeDelta = new Vector2(rect.width, rect.height);

         StartCoroutine(GameManager.Instance.ActionCallAfterTime(1, true, () => {
            _gridlayout.enabled = false;
        }));
    }
}
