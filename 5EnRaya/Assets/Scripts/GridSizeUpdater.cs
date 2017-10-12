using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode]
public class GridSizeUpdater : MonoBehaviour
{
    public GridLayoutGroup grid;
    public RectTransform gridRectTransform;
    public HorizontalLayoutGroup buttons;

	void Update ()
    {
        float w = gridRectTransform.rect.width;
        float h = gridRectTransform.rect.height;

        float rows = grid.constraintCount;
        float s = h / rows;
        grid.cellSize = new Vector2(s, s);

        float cols = grid.transform.childCount / rows;
        buttons.padding.left = (int)((w - (s * cols) + buttons.spacing) / 2);
        buttons.padding.right = buttons.padding.left;
	}
}
