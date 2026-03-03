using UnityEngine.UI;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(UnityEngine.UI.GridLayoutGroup))]
public class AdjustGridCellSize : MonoBehaviour
{
    
    [SerializeField] private GameObject[] allChild;
    [SerializeField] private int colSize = 3;
    [SerializeField] private int rowSize = 2;
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
    void Start()
    {
        UpdateCellSize();
    }
    private void OnValidate()
    {
        UpdateCellSize();
    }
    private void UpdateCellSize()
    {
        float spaceForSpacing = (colSize - 1) * gridlayout.spacing.y;
        float width = layoutRect.rect.width - spaceForSpacing;
        float xSizePerCell = width / colSize;

        float height = layoutRect.rect.height - spaceForSpacing;
        float ySizePerCell = height / rowSize;
        gridlayout.cellSize = new Vector2(ySizePerCell, ySizePerCell);

        var rect = layoutRect.rect;
        rect.width = (ySizePerCell * colSize) + spaceForSpacing;
        layoutRect.sizeDelta = new Vector2(rect.width, rect.height);


        int totalCount = 0;
        for (int i = 0; i < allChild.Length; i++)
        {
            allChild[i].SetActive((totalCount < colSize * rowSize));
            totalCount++;
        }
        
    }
}
