using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GeometricView : MonoBehaviour
{

    public Image mainImage;
    public Sprite[] differentViewSprites;
    private int m_currentSprite;
    public void ChangePicture(int direction)
    {
        m_currentSprite += direction;
        if (m_currentSprite == -1)
            m_currentSprite = differentViewSprites.Length - 1;

        mainImage.sprite = differentViewSprites[m_currentSprite % differentViewSprites.Length];
    }
}
