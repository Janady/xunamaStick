using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AutoRejustGridLayoutCellSize : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GridLayoutGroup grid = transform.GetComponent<GridLayoutGroup>();
        float width = GetComponent<RectTransform>().rect.width * 0.95f;
        grid.cellSize = new Vector2(width, grid.cellSize.y);
    }
}
