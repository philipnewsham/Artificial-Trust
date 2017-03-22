using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HackingDocuments : MonoBehaviour {

    private int m_numberOfDocuments;
    private bool[] m_docUnlocked;
    private bool m_isHacking;
    private float[] m_hackingValue;
    private float[] m_startingValues;
    private bool[] m_isHacked;
    private string[] m_docText;
    private int m_currentDoc;

    public Text documentText;
    public int powerValue;
    private Text[] m_percentages;
    public Button[] documentButtons;
    private string[] m_docName;
    private string[] m_documentMessage;
    private Image[] m_percentageImage;
    private int m_documentMessageNo = 0;
    //how many documents are unlocked for hacking after the current doc is hacked
    public int[] docUnlockedNumber;

    void Awake()
    {
        m_numberOfDocuments = documentButtons.Length;
        m_docUnlocked = new bool[m_numberOfDocuments];
        m_startingValues = new float[m_numberOfDocuments];
        m_isHacked = new bool[m_numberOfDocuments];
        m_percentages = new Text[m_numberOfDocuments];
        m_docName = new string[m_numberOfDocuments];
        m_percentageImage = new Image[m_numberOfDocuments];
        m_hackingValue = new float[m_numberOfDocuments];
        m_documentMessage = new string[m_numberOfDocuments];

        for (int i = 0; i < m_numberOfDocuments; i++)
        {
            if (docUnlockedNumber[i] == 0)
            {
                documentButtons[i].interactable = false;
            }
            else
            {
                documentButtons[i].interactable = true;
            }
            m_docUnlocked[i] = false;
            m_hackingValue[i] = 100 + (i * 100);
            m_startingValues[i] = m_hackingValue[i];
            Text currentText = documentButtons[i].GetComponentInChildren<Text>();
            m_percentages[i] = currentText;
            m_docName[i] = currentText.text;
            Image[] images = documentButtons[i].GetComponentsInChildren<Image>();
            m_percentageImage[i] = images[1];
        }
        documentButtons[0].interactable = true;
        m_documentMessage[0] = string.Format("Hacking doesn't cost you any power, but the more power you have, the faster it will be completed. Also, you can leave this page and the document will still be hacking.");
    }

    public void ClickedOnDocument(int docNo)
    {
        if (m_currentDoc == docNo)
        {
            if (m_docUnlocked[docNo])
            {
                ShowText();
            }
            else
            {
                m_isHacking = !m_isHacking;
            }
        }
        else
        {
            m_currentDoc = docNo;
            if (m_docUnlocked[docNo])
            {
                ShowText();
            }
            if (!m_isHacking)
            {
                m_isHacking = true;
            }
        }
    }

    public void RecieveDocumentMessages(string message, int order)
    {
        if (order < 3)
        {
            m_documentMessage[order + 1] = message;
        }
        else if(order < 5)
        {
            m_documentMessage[order + 2] = message;
        }
        else
        {
            m_documentMessage[order + 3] = message;
        }
    }

    public void PowerChange(int powValue)
    {
        powerValue = powValue;
    }

    void Update()
    {
        if (m_isHacking)
        {
            m_hackingValue[m_currentDoc] -= powerValue * Time.deltaTime;
            m_percentages[m_currentDoc].text = string.Format("{0}%", 100 - Mathf.FloorToInt((m_hackingValue[m_currentDoc] / m_startingValues[m_currentDoc])* 100f));
            m_percentageImage[m_currentDoc].fillAmount = 1 - (m_hackingValue[m_currentDoc] / m_startingValues[m_currentDoc]);
            if (m_hackingValue[m_currentDoc]<= 0f)
            {
                m_percentages[m_currentDoc].text = "100%";
                m_isHacking = false;
                print("Completed hacking!");
                ShowText();
                SubDocumentsInteractable(docUnlockedNumber[m_currentDoc]);
            }
        }
    }

    void ShowText()
    {
        documentText.text = m_documentMessage[m_currentDoc];
            //m_docText[m_currentDoc];
    }

    void SubDocumentsInteractable(int subDocuments)
    {
        if(subDocuments > 0)
        {
            for (int i = m_currentDoc + 1; i < m_currentDoc + subDocuments + 1; i++)
            {
                documentButtons[i].interactable = true;
            }
        }
    }
}
