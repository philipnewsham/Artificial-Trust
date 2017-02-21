using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AIInformation : MonoBehaviour
{
    private bool[] m_hasSeen;
    public string[] informationString;
    public GameObject infoCanvas;
    public Text infoText;

    void Start()
    {
        int length = informationString.Length;
        m_hasSeen = new bool[length];
        for (int i = 0; i < length; i++)
        {
            m_hasSeen[i] = false;
        }
    }

    public void SelectedPage(int pageNo)
    {
        if (!m_hasSeen[pageNo])
        {
            m_hasSeen[pageNo] = true;
            infoText.text = informationString[pageNo];
            infoCanvas.SetActive(true);
        }
    }
}
