using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GridMapSize : MonoBehaviour
{
    GridLayoutGroup m_gridLayoutGroup;
	void Start ()
    {
        float width = (Screen.width * 0.6f)/12;
        float height = (Screen.height * 0.7f)/9;
        m_gridLayoutGroup = GetComponent<GridLayoutGroup>();
        m_gridLayoutGroup.cellSize = new Vector2(width, height);
	}

}
