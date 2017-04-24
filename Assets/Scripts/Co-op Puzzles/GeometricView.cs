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

    public Button[] changeViewButtons;
    public void ChangePicture(int direction)
    {
        m_currentSprite += direction;
        for (int i = 0; i < 2; i++)
        {
            changeViewButtons[i].interactable = false;
        }
        m_currentRotation = new Vector3(artObjects[0].transform.eulerAngles.x, artObjects[0].transform.eulerAngles.y, artObjects[0].transform.eulerAngles.z);
        if (m_currentSprite == -1)
            m_currentSprite = differentViewSprites.Length - 1;
        StartCoroutine("SpinArt");
        mainImage.sprite = differentViewSprites[m_currentSprite % differentViewSprites.Length];
    }
    float count;
    public GameObject[] artObjects;
    private Vector3 m_currentRotation;
    private Vector3[] m_nextRotation = new Vector3[6]
    {
        new Vector3(0,0,0), new Vector3(0,90,0),new Vector3(0,180,0), new Vector3(0,270,0),new Vector3(90,0,0), new Vector3(-90,0,0)
    };
    IEnumerator SpinArt()
    {
        while (count < 50)
        {
            //print("Spin!");
            for (int i = 0; i < artObjects.Length; i++)
            {
                print(count / 50);
                artObjects[i].transform.eulerAngles = new Vector3(
                    Mathf.Lerp(m_currentRotation.x, m_nextRotation[m_currentSprite % differentViewSprites.Length].x, count / 50),
                    Mathf.Lerp(m_currentRotation.y, m_nextRotation[m_currentSprite % differentViewSprites.Length].y, count / 50),
                    Mathf.Lerp(m_currentRotation.z, m_nextRotation[m_currentSprite % differentViewSprites.Length].z, count / 50));
            }
            count += 1;
            yield return new WaitForFixedUpdate();
        }
        count = 0;
        for (int i = 0; i < 2; i++)
        {
            changeViewButtons[i].interactable = true;
        }
    }
    /*
    float countF;
    void Update()
    {
        countF += 1; 
        artObjects[0].transform.eulerAngles = new Vector3(
            Mathf.Lerp(m_currentRotation.x, m_nextRotation[m_currentSprite % differentViewSprites.Length].x, countF/100),
            Mathf.Lerp(m_currentRotation.y, m_nextRotation[m_currentSprite % differentViewSprites.Length].y, countF/100),
            Mathf.Lerp(m_currentRotation.z, m_nextRotation[m_currentSprite % differentViewSprites.Length].z, countF/100));
    }
    */
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
