using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class RobotBodyPasswordButton : MonoBehaviour {
    public AIPower aiPowerScript;
    private bool m_isDownloading = false;
    public int loadingValue;
    private float m_currentLoadValue;
    private Button m_thisButton;
    private string m_password;
    public Image buttonDownloadBackground;
    private int m_currentPower;
    void Start()
    {
        m_thisButton = gameObject.GetComponent<Button>();
        m_currentLoadValue = loadingValue;
    }
	// Use this for initialization
    public void DownloadingData()
    {
        m_isDownloading = !m_isDownloading;
    }

    public void ReceivePassword(string password)
    {
        m_password = password;
    }

    public void CurrentPower(int power)
    {
        m_currentPower = power;
    }

    void Update()
    {
        if (m_isDownloading)
        {
            m_currentLoadValue -= m_currentPower * Time.deltaTime;
            float percentagedone = (100 - ((m_currentLoadValue/loadingValue)*100));
            print(percentagedone);
            buttonDownloadBackground.fillAmount = percentagedone/100;
            if(m_currentLoadValue <= 0)
            {
                m_isDownloading = false;
                m_thisButton.interactable = false;
                m_thisButton.GetComponentInChildren<Text>().text = m_password;
            }
        }
    }
}
