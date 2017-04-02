using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DocumentButton : MonoBehaviour
{
    public float powerNeeded;
    private float m_downloadedPower;
    float m_currentPower;
    public AIPower aiPower;

    public string hoverText;
    public string documentText;
    public int passwordNo;
    public Text hoverTextbox;
    public Text informationTextbox;

    public Button[] unlockedButtons;

    bool m_isCompleted;

    private Image m_fillImage;

	void Start ()
    {
        Image[] images = GetComponentsInChildren<Image>();
        m_fillImage = images[1];
	}
    bool isDownloading;
    public void Clicked()
    {
        if(!m_isCompleted)
            isDownloading = !isDownloading;

        m_currentPower = aiPower.CurrentPower();
        hoverTextbox.text = hoverText;
    }

    public void HoverText(bool isHover)
    {
        if(isHover)
        {
            hoverTextbox.text = hoverText;
        }
        else
        {

        }
    }
    float percentage;
	void Update ()
    {
		if(isDownloading)
        {
            m_downloadedPower += m_currentPower * Time.deltaTime;
            percentage = (m_downloadedPower / powerNeeded);
            m_fillImage.fillAmount = percentage;
            if (m_downloadedPower >= powerNeeded)
            {
                DownloadComplete();
            }
        }
	}

    void DownloadComplete()
    {
        isDownloading = false;
        m_isCompleted = true;
        informationTextbox.text = documentText;
        for (int i = 0; i < unlockedButtons.Length; i++)
        {
            unlockedButtons[i].interactable = true;
        }
    }
}
