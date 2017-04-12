using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextHoverTest : MonoBehaviour {
    public Color32[] imageColours;
    public Color32[] textColours;
    private Text m_text;
    private Image m_image;

    void Start()
    {
        m_text = GetComponentInChildren<Text>();
        m_image = GetComponentInChildren<Image>();
    }
	public void HoverOver (bool isHovering)
    {
		if(isHovering)
        {
            m_image.color = imageColours[1];
            m_text.color = textColours[1];
        }
        else
        {
            m_image.color = imageColours[0];
            m_text.color = textColours[0];
        }
	}
}
