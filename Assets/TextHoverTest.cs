using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextHoverTest : MonoBehaviour {
    public Color32[] imageColours;
    public Color32[] textColours;
    public Text text;
    public Image image;
	public void HoverOver (bool isHovering)
    {
		if(isHovering)
        {
            image.color = imageColours[1];
            text.color = textColours[1];
        }
        else
        {
            image.color = imageColours[0];
            text.color = textColours[0];
        }
	}
}
