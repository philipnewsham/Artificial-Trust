using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class FilingCabinet : MonoBehaviour {
    private string m_password;
    //how long it takes to get the password
    private float m_searchingTime = 30f;
    private float m_percentageSearched;
    private bool m_isSearching = false;
    public Canvas cabinetCanvas;
    private Image m_percentageImage;
    private Text m_passwordText;
    public int passwordID;
    public GameObject gameController;
    private FreezeControls m_freezePlayerScript;

    public GameObject searchingCanvas;
    public Text filingCabinetText;
    public Text percentageCompleteText;

    private AudioSource m_audioSource;

    void Start()
    {
        m_audioSource = gameObject.GetComponent<AudioSource>();
        Invoke("GetPassword", 1f);
        m_percentageSearched = 0f;
        m_percentageImage = cabinetCanvas.GetComponentInChildren<Image>();
        m_passwordText = cabinetCanvas.GetComponentInChildren<Text>();
        m_freezePlayerScript = gameController.GetComponent<FreezeControls>();

    }

    void GetPassword()
    {
        m_password = gameObject.GetComponent<ReceivePasswords>().password;
    }

    public void Interact()
    {
        //print(m_password);
        m_isSearching = !m_isSearching;
        if (m_isSearching)
        {
            m_audioSource.Play();
        }
        else
        {
            m_audioSource.Stop();
        }
        m_freezePlayerScript.FirstPersonControllerEnabled(!m_isSearching);
        searchingCanvas.SetActive(m_isSearching);
        filingCabinetText.text = string.Format("Currently searching through filing cabinet {0}", passwordID);
    }

    void Update()
    {
        if (m_isSearching)
        {
            m_percentageSearched += 1 * Time.deltaTime;
            m_percentageImage.fillAmount = ((m_percentageSearched / m_searchingTime) * 100) / 100;
            percentageCompleteText.text = string.Format("Percentage Completed: {0}%", Mathf.FloorToInt((m_percentageSearched / m_searchingTime) * 100));
            if (m_percentageSearched >= m_searchingTime)
            {
                m_isSearching = false;
                //print("Done");
                ShowPassword();
                m_freezePlayerScript.FirstPersonControllerEnabled(true);
                searchingCanvas.SetActive(false);
                m_audioSource.Stop();
            }
        }
    }

    void ShowPassword()
    {
        //print("Show Password");
        m_percentageImage.fillAmount = 0f;
        m_passwordText.text = m_password;
        print(m_password);
    }
}
