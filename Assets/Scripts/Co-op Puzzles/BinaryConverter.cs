using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class BinaryConverter : MonoBehaviour
{
    private string[] letters = new string[27] { " ", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
    private string[] binary = new string[27] { "0010 0000", "0110 0001", "0110 0010", "0110 0011", "0110 0100", "0110 0101", "0110 0110", "0110 0111", "0110 1000", "0110 1001", "0110 1010", "0110 1011", "0110 1100", "0110 1101", "0110 1110", "0110 1111", "0111 0000", "0111 0001", "0111 0010", "0111 0011", "0111 0100", "0111 0101", "0111 0110", "0111 0111", "0111 1000", "0111 1001", "0111 1010" };

    public string word;
    private string m_binaryWord;
	void Start ()
    {
	    char[] wordToChar = word.ToCharArray();
        for (int i = 0; i < wordToChar.Length; i++)
        {
            for (int j = 0; j < letters.Length; j++)
            {
                if(wordToChar[i].ToString() == letters[j])
                {
                    m_binaryWord += binary[j];
                    m_binaryWord += "  ";
                }
            }
        }

        //print(m_binaryWord);
		GetComponent<Text> ().text = m_binaryWord;
	}

}
