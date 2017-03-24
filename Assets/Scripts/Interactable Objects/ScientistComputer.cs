using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ScientistComputer : MonoBehaviour {
    private ReceivePasswords m_receivePasswordScript;
    private string m_password;
    private string[] m_letters = new string[10] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
    private string m_firstLetter = "A";
    private string m_secondLetter = "A";
    private string m_thirdLetter = "A";
    //the specific letter (of the three) that is currently editable
    private int m_currentLetterNo = 0;
    //the current value of the three letters
    private int[] m_individualLetters = new int[3] { 0, 0, 0 };
    private bool m_isActive = false;
    private bool m_lockedOut = false;
    private bool m_dPadPressed = false;
    private bool m_justPressed = false;
    private int m_lengthNumber;

    public GameObject computerCanvas;
    public Text passwordText;

    private GameObject m_gameController;
    private FreezeControls m_freezeControlScript;

    private bool m_correctPassword = false;

    public GameObject passwordPanel;
    public GameObject informationPanel;

    public int downloadingTime;
    private int m_downloadingTime;
    private bool m_downloadingSequenceData;
    private bool m_downloadingStarsignData;
    private float m_sequenceTime;
    private float m_starsignTime;
    private string[] m_minuteText = new string[2] { "minutes", "minute" };
    private string[] m_secondText = new string[2] { "seconds", "second" };
    private string[] m_currentDocumentName = new string[2] { "Safe Sequence", "Safe Starsign" };
    private bool[] m_downloadsCompleted = new bool[2] { false, false };
    public Text downloadingDataText;
    public Slider downloadingDataSlider;
    public GameObject sequenceInformation;
    public GameObject starsignInformation;

    private AudioSource m_audioSource;
    public AudioClip[] audioClips;

    public GameObject[] letterSelectIcon;
    // Use this for initialization
    void Start ()
    {
        m_audioSource = gameObject.GetComponent<AudioSource>();
        m_downloadingTime = downloadingTime;
        m_sequenceTime = m_downloadingTime;
        m_starsignTime = m_downloadingTime;
        m_receivePasswordScript = gameObject.GetComponent<ReceivePasswords>();
        m_gameController = GameObject.FindGameObjectWithTag("GameController");
        m_freezeControlScript = m_gameController.GetComponent<FreezeControls>();
        m_lengthNumber = m_letters.Length;
        Invoke("RecievePassword", 1f);
	}


    public void ReceiveSequence(string message)
    {
        sequenceInformation.GetComponentInChildren<Text>().text = message;
    }

    public void ReceiveStarsign(string message)
    {
        starsignInformation.GetComponentInChildren<Text>().text = message;
    }

    void RecievePassword()
    {
        m_password = m_receivePasswordScript.password;
    }

    void Update()
    {
        if (m_isActive)
        {
            if (Input.GetButtonDown("ControllerBack"))
            {
                LeaveCanvas();
            }
        }

        if (m_isActive && !m_lockedOut)
        {
            if (!m_dPadPressed)
            {
                if (Input.GetAxisRaw("DpadY") > 0)
                {
                    print("up");
                    m_dPadPressed = true;
                    DPadUp();
                }
                if (Input.GetAxisRaw("DpadY") < 0)
                {
                    print("down");
                    m_dPadPressed = true;
                    DPadDown();
                }
                if (Input.GetAxisRaw("DpadX") > 0)
                {
                    print("right");
                    m_dPadPressed = true;
                    DPadRight();
                }
                if (Input.GetAxisRaw("DpadX") < 0)
                {
                    print("left");
                    m_dPadPressed = true;
                    DPadLeft();
                }
            }

            if (Input.GetButtonDown("ControllerA"))
            {
                if (m_justPressed)
                {
                    GetButtonA();
                }
                else
                {
                    m_justPressed = true;
                }
            }

            if (Input.GetButtonDown("ControllerX"))
            {
                GetButtonX();
            }

            if (Input.GetButtonDown("ControllerB"))
            {
                GetButtonB();
            }

            if ((Input.GetAxisRaw("DpadY") == 0) && (Input.GetAxisRaw("DpadX") == 0) && (m_dPadPressed))
            {
                m_dPadPressed = false;
            }
        }

        if (m_downloadingSequenceData)
        {
            m_sequenceTime -= 1 * Time.deltaTime;
            if (m_sequenceTime <= 0f)
            {
                m_downloadingSequenceData = false;
                m_downloadsCompleted[0] = true;
                downloadingDataText.text = string.Format("Finished Downloading Current Document: {0}", m_currentDocumentName[0]);
                sequenceInformation.SetActive(true);
                m_audioSource.clip = audioClips[3];
                m_audioSource.Play();
            }
            else
            {
                int percentageCompleted = Mathf.FloorToInt(100 - (m_sequenceTime / m_downloadingTime * 100));
                int minutesRemaining = Mathf.FloorToInt(m_sequenceTime / 60);
                int secondsRemaining = Mathf.FloorToInt(m_sequenceTime % 60);
                string currentMinute;
                string currentSecond;
                if (minutesRemaining == 1)
                {
                    currentMinute = m_minuteText[1];
                }
                else
                {
                    currentMinute = m_minuteText[0];
                }
                if (secondsRemaining == 1)
                {
                    currentSecond = m_secondText[1];
                }
                else
                {
                    currentSecond = m_secondText[0];
                }
                string downloadingDataMessage = string.Format("Current Document: {0} \nPercentage Completed: {1}% \nTime Remaining: {2} {3} and {4} {5}.", m_currentDocumentName[0], percentageCompleted, minutesRemaining, currentMinute, secondsRemaining, currentSecond);
                downloadingDataText.text = downloadingDataMessage;
                downloadingDataSlider.value = percentageCompleted / 100f;
                //print(percentageCompleted / 100);
            }
        }

        if (m_downloadingStarsignData)
        {
            m_starsignTime -= 1 * Time.deltaTime;
            if (m_starsignTime <= 0f)
            {
                m_downloadingSequenceData = false;
                m_downloadsCompleted[1] = true;
                downloadingDataText.text = string.Format("Finished Downloading Current Document: {0}", m_currentDocumentName[1]);
                starsignInformation.SetActive(true);
                m_audioSource.clip = audioClips[3];
                m_audioSource.Play();
            }
            else
            {
                int percentageCompleted = Mathf.FloorToInt(100 - (m_starsignTime / m_downloadingTime * 100));
                int minutesRemaining = Mathf.FloorToInt(m_starsignTime / 60);
                int secondsRemaining = Mathf.FloorToInt(m_starsignTime % 60);
                string currentMinute;
                string currentSecond;
                if (minutesRemaining == 1)
                {
                    currentMinute = m_minuteText[1];
                }
                else
                {
                    currentMinute = m_minuteText[0];
                }
                if (secondsRemaining == 1)
                {
                    currentSecond = m_secondText[1];
                }
                else
                {
                    currentSecond = m_secondText[0];
                }
                string downloadingDataMessage = string.Format("Current Document: {0} \nPercentage Completed: {1}% \nTime Remaining: {2} {3} and {4} {5}.", m_currentDocumentName[1], percentageCompleted, minutesRemaining, currentMinute, secondsRemaining, currentSecond);
                downloadingDataText.text = downloadingDataMessage;
                downloadingDataSlider.value = percentageCompleted / 100f;
                //print(percentageCompleted / 100);
            }
        }
    }

    void GetButtonA()
    {
        CheckPassword();
    }

    void GetButtonX()
    {
        if (m_correctPassword)
        {
            DownloadingInformation(true);
            m_audioSource.clip = audioClips[4];
            m_audioSource.Play();
        }
    }

    void GetButtonB()
    {
        if (m_correctPassword)
        {
            DownloadingInformation(false);
            m_audioSource.clip = audioClips[4];
            m_audioSource.Play();
        }
    }

    void DPadUp()
    {
        if (m_individualLetters[m_currentLetterNo] < m_lengthNumber - 1)
        {
            m_individualLetters[m_currentLetterNo] += 1;
        }
        else
        {
            m_individualLetters[m_currentLetterNo] = 0;
        }
        UpdateLetters();
    }
    void DPadDown()
    {
        if (m_individualLetters[m_currentLetterNo] > 0)
        {
            m_individualLetters[m_currentLetterNo] -= 1;
        }
        else
        {
            m_individualLetters[m_currentLetterNo] = m_lengthNumber - 1;
        }
        UpdateLetters();
    }
    void DPadLeft()
    {
        if (m_currentLetterNo > 0)
        {
            m_currentLetterNo -= 1;
        }
        else
        {
            m_currentLetterNo = 2;
        }
        IconMoveOverLetter();
    }
    void DPadRight()
    {
        if (m_currentLetterNo < 2)
        {
            m_currentLetterNo += 1;
        }
        else
        {
            m_currentLetterNo = 0;
        }
        IconMoveOverLetter();
    }

    void IconMoveOverLetter()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i != m_currentLetterNo)
            {
                letterSelectIcon[i].SetActive(false);
            }
            else
            {
                letterSelectIcon[i].SetActive(true);
            }
        }
        m_audioSource.clip = audioClips[0];
        m_audioSource.Play();
    }

    void DownloadingInformation(bool isSequence)
    {
        if (isSequence)
        {
            starsignInformation.SetActive(false);
            m_downloadingStarsignData = false;
            if (!m_downloadsCompleted[0])
            {
                m_downloadingSequenceData = !m_downloadingSequenceData;
            }
            else
            {
                sequenceInformation.SetActive(true);
            }
        }
        else
        {
            sequenceInformation.SetActive(false);
            m_downloadingSequenceData = false;
            if (!m_downloadsCompleted[1])
            {
                m_downloadingStarsignData = !m_downloadingStarsignData;
            }
            else
            {
                starsignInformation.SetActive(true);
            }
        }
    }

    void UpdateLetters()
    {
        m_firstLetter = m_letters[m_individualLetters[0]];
        m_secondLetter = m_letters[m_individualLetters[1]];
        m_thirdLetter = m_letters[m_individualLetters[2]];
        passwordText.text = string.Format("{0} - {1} - {2}", m_firstLetter, m_secondLetter, m_thirdLetter);
        m_audioSource.clip = audioClips[0];
        m_audioSource.Play();
    }

    public void Interact()
    {
        if (!m_isActive)
        {
            m_isActive = true;
            computerCanvas.SetActive(true);
            m_freezeControlScript.FirstPersonControllerEnabled(false);
            if (!m_correctPassword)
            {
                passwordPanel.SetActive(true);
            }
            else
            {
                informationPanel.SetActive(true);
            }
        }
    }

    void LeaveCanvas()
    {
        m_isActive = false;
        computerCanvas.SetActive(false);
        m_freezeControlScript.FirstPersonControllerEnabled(true);
        passwordPanel.SetActive(false);
        informationPanel.SetActive(false);
    }
    public AgentObjectives agentObjectives;
    void CheckPassword()
    {
        string currentPassword = string.Format("{0}{1}{2}", m_firstLetter, m_secondLetter, m_thirdLetter);
        if(currentPassword == m_password)
        {
            print("Password Accepted");
            m_correctPassword = true;
            agentObjectives.UnlockComputer(0, true);
            SwapToInformationPanel();
            m_audioSource.clip = audioClips[1];
        }
        else
        {
            print("Incorrect Password");
            m_audioSource.clip = audioClips[2];
        }
        m_audioSource.Play();
    }

    void SwapToInformationPanel()
    {
        passwordPanel.SetActive(false);
        informationPanel.SetActive(true);
    }
}
