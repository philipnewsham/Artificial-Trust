using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BinaryDecipher : MonoBehaviour 
{
	private string[] m_binaryAlphabet = new string[27] 
	{
		"0110 0001", //a
		"0110 0010", //b
		"0110 0011", //c
		"0110 0100", //d
		"0110 0101", //e
		"0110 0110", //f
		"0110 0111", //g
		"0110 1000", //h
		"0110 1001", //i
		"0110 1010", //j
		"0110 1011", //k
		"0110 1100", //l
		"0110 1101", //m
		"0110 1110", //n
		"0110 1111", //o
		"0111 0000", //p
		"0111 0001", //q
		"0111 0010", //r
		"0111 0011", //s
		"0111 0100", //t
		"0111 0101", //u
		"0111 0110", //v
		"0111 0111", //w
		"0111 1000", //x
		"0111 1001", //y
		"0111 1010", //z
        "0010 0000" // 
	};

	private string[] m_alphabet = new string[27] 
	{
		"a", 
		"b", 
		"c",
		"d",
		"e",
		"f",
		"g",
		"h",
		"i",
		"j",
		"k",
		"l",
		"m",
		"n",
		"o",
		"p",
		"q",
		"r",
		"s",
		"t",
		"u",
		"v",
		"w",
		"x",
		"y",
		"z",
        " "
	};

    public Button[] buttons;
	public GameObject[] letterCubes;
	private int m_currentLetter;
	public Material[] letterCubeMaterials;
    public GameObject lockOutPanel;

	public void ClickedButton(string text)
	{
		//check which letter it is (i.e 0110 0001 == a)
		for (int i = 0; i < m_binaryAlphabet.Length; i++) 
		{
			if (text == m_binaryAlphabet [i]) 
			{
				//Debug.LogFormat ("This is the {0} letter of the alphabet", i + 1);
				m_currentLetter = i;
			}
		}

		//change materials to light up current letter
		for (int i = 0; i < letterCubes.Length; i++) 
		{
			if (m_currentLetter == i) 
			{
				letterCubes [i].GetComponent<Renderer> ().material = letterCubeMaterials [1];
			} 
			else 
			{	
				letterCubes [i].GetComponent<Renderer> ().material = letterCubeMaterials [0];
			}
		}

	}

	void Update()
	{
		if (Input.anyKeyDown) 
		{
			CheckKeyPress (Input.inputString);
		}
	}
    public Text showLetterText;
    private bool m_correctLetter;
	void CheckKeyPress(string currentKey)
	{
		for (int i = 0; i < m_alphabet.Length; i++) 
		{
			if(Input.inputString == m_alphabet[i])
			{
                showLetterText.text = m_alphabet[i];
				if (Input.inputString == m_alphabet [m_currentLetter]) 
				{
					//print ("right letter!");
                    m_correctLetter = true;
					//KeyCorrect ();
				} 
				else 
				{
					print ("wrong letter!");
                    m_correctLetter = false;
					//KeyWrong ();
				}
			}
		}
	}

    public void CheckLetter()
    {
        if(m_correctLetter)
        {
            KeyCorrect();
        }
        else
        {
            KeyWrong();
        }
        showLetterText.text = "";
    }
    int m_lettersCorrect = 25;
	void KeyCorrect()
	{
        
        for (int i = 0; i < buttons.Length; i++)
        {
            if(buttons[i].GetComponentInChildren<Text>().text == m_binaryAlphabet[m_currentLetter])
            {
                buttons[i].GetComponentInChildren<Text>().text = m_alphabet[m_currentLetter];
                buttons[i].interactable = false;
            }
        }
        m_lettersCorrect += 1;
        if(m_lettersCorrect == 26)
        {
            UnlockNextPuzzle();
        }
	}
    public DoorController doorController;
    public Button nextPuzzleButton;
    void UnlockNextPuzzle()
    {
        //print("unlocknextpuzzle");
        doorController.Locking(5);
        doorController.Locking(1);
        nextPuzzleButton.interactable = true;
    }

	void KeyWrong()
	{
        lockOutPanel.SetActive(true);
        Invoke("LockOutDisabled", 5f);
	}

    void LockOutDisabled()
    {
        lockOutPanel.SetActive(false);
    }
}

