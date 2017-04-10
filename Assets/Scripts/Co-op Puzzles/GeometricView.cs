using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GeometricView : MonoBehaviour
{
    public Image mainImage;
    public Sprite[] differentViewSprites;
    private int m_currentSprite;
	public int correctShape;
	public Light[] spotlights;
	public Color32[] lightColours;
	private DoorController m_doorController;
	public GameObject mainAICanvas;
	public GameObject puzzleAICanvas;

	void Start()
	{
		m_doorController = GetComponent<BinaryDecipher>().doorController;
	}

    public void ChangePicture(int direction)
    {
        m_currentSprite += direction;
        if (m_currentSprite == -1)
            m_currentSprite = differentViewSprites.Length - 1;

        mainImage.sprite = differentViewSprites[m_currentSprite % differentViewSprites.Length];
    }

	public void SelectedObject(int selectedNo)
	{
		if (selectedNo == correctShape) 
		{
			Correct ();
		} 
		else 
		{
			Incorrect ();
		}
	}
    public Button nextButton;
	void Correct()
	{
		for (int i = 0; i < 3; i++) 
		{
			spotlights [i].color = lightColours [1];
		}
		//m_doorController.Locking (0);
		//m_doorController.Locking (3);
        nextButton.interactable = true;
		//puzzleAICanvas.SetActive (false);
		//mainAICanvas.SetActive (true);
	}

	void Incorrect()
	{
		for (int i = 0; i < 3; i++) 
		{
			spotlights [i].color = lightColours [2];
		}
	}
}
