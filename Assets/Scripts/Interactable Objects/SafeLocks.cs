using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SafeLocks : MonoBehaviour
{
    private int m_lockLevel = 0;
    private bool m_dPadPressed;
    public GameObject[] lockPanels;

    public Image starsignImage;
    public Sprite[] starsigns;
    private string[] starsignNames = new string[12] { "Aries", "Taurus", "Gemini", "Cancer", "Leo", "Virgo", "Libra", "Scorpio", "Sagittarius", "Capricorn", "Aquarius", "Pisces" };
    private int m_currentStarsign = 0;
    private bool m_isActive = false;
    public string[] passwords = new string[4];
    public Text[] passwordText;
    public string actualPassword;
    public InputField passwordField;

    private bool m_passwordLock = false;
    private bool m_sequenceLock = false;
    private bool m_starsignLock = false;
    private bool m_allLocks = false;

    private int m_correctStarSign;

    public int[] sequenceOrder = new int[4];
    private int m_pressOrder;

    public GameObject safeCanvas;

    public GameObject safe;
    private Animator m_safeAnim;
    private AudioSource m_safeAS;
    public AudioClip[] audioClips;

    private bool m_lockedOut;
    public GameObject lockedOutPanel;
    public Text lockedOutText;
    private float m_countingDown;
    public float lockedOutTime;
    private float m_lockedOutTime;

    public GameObject ai;
    private HackingDocuments m_hackingDocuments;

    public GameObject scientist;
    private ScientistWin m_scientistWinScript;

    public GameObject gameController;
    private FreezeControls m_freezeControls;

    public Text locksText;
    public Button unlockButton;

    private int m_locksUnlocked = 0;

    public Image lockOneLight;
    public Image lockTwoLight;
    public Image lockThreeLight;
    public Sprite[] lockLights; //0 = off, 1 = on
    //bool should stop player automatically choosing A option
    private bool m_justPressed;

    public ScientistComputer scientistComputerScript;
    public Button[] documentButtons;
    void Start ()
    {
        //print("Start");
        m_scientistWinScript = scientist.GetComponent<ScientistWin>();
        m_safeAS = safe.GetComponent<AudioSource>();
        m_freezeControls = gameController.GetComponent<FreezeControls>();
        m_hackingDocuments = ai.GetComponent<HackingDocuments>();
        m_safeAnim = safe.GetComponent<Animator>();
        UpdatePanels();
        m_lockedOutTime = lockedOutTime;
        m_countingDown = m_lockedOutTime;
        locksText.text = string.Format("({0}/3) Locks Unlocked", m_locksUnlocked);
        m_correctStarSign = Random.Range(0, 12);
        starsignImage.sprite = starsigns[m_currentStarsign];
        string starsignMessage = string.Format("////CONFIDENTIAL////\n---For Authorised Personnel Only---\nThe star sign that unlocks the safe is {0}", starsignNames[m_correctStarSign]);
        documentButtons[0].GetComponent<DocumentButton>().documentText = string.Format("The star sign that unlocks the safe is {0}", starsignNames[m_correctStarSign]);
        scientistComputerScript.ReceiveStarsign(starsignMessage);

        m_hackingDocuments.RecieveDocumentMessages(starsignMessage, 4);
        safeCanvas.SetActive(false);

	}

    // Update is called once per frame
    void Update()
    {
        if (m_lockedOut)
        {
            m_countingDown -= 1 * Time.deltaTime;
            lockedOutText.text = string.Format("PLEASE WAIT {0} SECONDS BEFORE TRYING AGAIN", Mathf.FloorToInt(m_countingDown));
            if (m_countingDown <= 0f)
            {
                lockedOutPanel.SetActive(false);
                m_lockedOut = false;
                m_countingDown = m_lockedOutTime;
            }
        }
        if (m_isActive)
        {
            if (Input.GetButtonDown("ControllerBack"))
            {
                LeaveSafe();
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
                    MenuNavigation(true);
                }
                if (Input.GetAxisRaw("DpadY") < 0)
                {
                    print("down");
                    m_dPadPressed = true;
                    MenuNavigation(false);
                }
            }

            if ((Input.GetAxisRaw("DpadY") == 0) && (Input.GetAxisRaw("DpadX") == 0) && (m_dPadPressed))
            {
                m_dPadPressed = false;
                //print("release");
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
            if (Input.GetButtonDown("ControllerB"))
            {
                GetButtonB();
            }
            if (Input.GetButtonDown("ControllerX"))
            {
                GetButtonX();
            }
            if (Input.GetButtonDown("ControllerY"))
            {
                GetButtonY();
            }
        }
    }

    public void UpdatePasswords()
    {
        for (int i = 0; i < 4; i++)
        {
            passwordText[i].text = passwords[i];
        }
    }

    void GetButtonA()
    {
        if(m_lockLevel == 0)
        {
            if (m_allLocks)
            {
                Invoke("OpenSafe",.5f);
                m_safeAS.clip = audioClips[1];
                m_safeAS.Play();
            }
            else
            {
                m_safeAS.clip = audioClips[0];
                m_safeAS.Play();
            }
        }
        if(m_lockLevel == 2)
        {
            CheckPasswords(passwords[2]);
        }
        if(m_lockLevel == 1)
        {
            ButtonSequence(4); 
        }
        if(m_lockLevel == 3)
        {
            if(!m_starsignLock)
            ChangeStarSign(-1);
        }
    }
    void GetButtonB()
    {
        if (m_lockLevel == 0)
        {
            //Nothing
        }
        if (m_lockLevel == 2)
        {
            CheckPasswords(passwords[1]);
        }
        if (m_lockLevel == 1)
        {
            ButtonSequence(3);
        }
        if (m_lockLevel == 3)
        {
            if (!m_starsignLock)
                CheckStarsign();
            //starsign select
        }
    }
    void GetButtonX()
    {
        if (m_lockLevel == 0)
        {
            //nothing
        }
        if (m_lockLevel == 2)
        {
            CheckPasswords(passwords[3]);
        }
        if (m_lockLevel == 1)
        {
            ButtonSequence(1);
        }
        if (m_lockLevel == 3)
        {
            //nothing
        }
    }
    void GetButtonY()
    {
        if (m_lockLevel == 0)
        {
            //nothing
        }
        if (m_lockLevel == 2)
        {
            CheckPasswords(passwords[0]);
        }
        if (m_lockLevel == 1)
        {
            ButtonSequence(2);
        }
        if (m_lockLevel == 3)
        {
            if (!m_starsignLock)
                ChangeStarSign(1);
        }
    }

    public void Interact()
    {
        if (!m_isActive)
        {
            m_isActive = true;
            safeCanvas.SetActive(true);
            m_freezeControls.FirstPersonControllerEnabled(false);
        }
    }
    void LeaveSafe()
    {
        print("LeaveSafe");
        safeCanvas.SetActive(false);
        m_justPressed = false;
        m_isActive = false;
        m_freezeControls.FirstPersonControllerEnabled(true);
    }
    void MenuNavigation(bool moveUp)
    {
        if (!moveUp)
        {
            if (m_lockLevel < lockPanels.Length - 1)
            {
                m_lockLevel += 1;
                NavigationSound();
            }
        }
        else
        {
            if(m_lockLevel > 0)
            {
                m_lockLevel -= 1;
                NavigationSound();
            }
        }
        UpdatePanels();
    }
    void NavigationSound()
    {
        m_safeAS.clip = audioClips[6];
        m_safeAS.Play();
    }

    void UpdatePanels()
    {
        for (int i = 0; i < lockPanels.Length; i++)
        {
            if (i == m_lockLevel)
            {
                lockPanels[i].SetActive(true);
            }
            else
            {
                lockPanels[i].SetActive(false);
            }
        }
    }

    void ChangeStarSign(int changeDirection)
    {
        m_currentStarsign += changeDirection;
        if(m_currentStarsign > starsigns.Length - 1)
        {
            m_currentStarsign = 0;
        }
        if(m_currentStarsign < 0)
        {
            m_currentStarsign = starsigns.Length - 1;
        }
        starsignImage.sprite = starsigns[m_currentStarsign];
        m_safeAS.clip = audioClips[5];
        m_safeAS.Play();
    }

    void CheckStarsign()
    {
        if (!m_starsignLock)
        {
            if (m_currentStarsign == m_correctStarSign)
            {
                m_starsignLock = true;
                lockThreeLight.sprite = lockLights[1];
                CheckLocks();
            }
            else
            {
                LockedOut();
            }
        }
    }

    void CheckPasswords(string password)
    {
        if (!m_passwordLock)
        {
            if (password == actualPassword)
            {
                passwordField.text = actualPassword;
                m_passwordLock = true;
                lockOneLight.sprite = lockLights[1];
                CheckLocks();
            }
            else
            {
                LockedOut();
            }
        }
    }

    void CheckLocks()
    {
        m_safeAS.clip = audioClips[4];
        m_safeAS.Play();
        m_locksUnlocked += 1;
        if(m_passwordLock && m_sequenceLock && m_starsignLock)
        {
            m_allLocks = true;
            unlockButton.interactable = true;
        }
        locksText.text = string.Format("({0}/3) Locks Unlocked", m_locksUnlocked);
    }

    void ButtonSequence(int currentButton)
    {
        if (!m_sequenceLock)
        {
            if (sequenceOrder[m_pressOrder] == currentButton)
            {
                //correct
                m_pressOrder += 1;
                m_safeAS.clip = audioClips[3];
                m_safeAS.pitch = (1 + (.1f * m_pressOrder));
                m_safeAS.Play();
                print("correct");
                if (m_pressOrder == sequenceOrder.Length)
                {
                    m_sequenceLock = true;
                    lockTwoLight.sprite = lockLights[1];
                    CheckLocks();
                    //unlocked
                }
            }
            else
            {
                m_pressOrder = 0;
                print("Wrong!");
                LockedOut();
                //reset
            }
        }
    }

    void OpenSafe()
    {
        m_safeAS.clip = audioClips[7];
        m_safeAS.Play();
        m_safeAnim.SetTrigger("Unlocked");
        m_scientistWinScript.CheckWinCondition(0);
        print("win");
        LeaveSafe();
    }

    void LockedOut()
    {
        m_safeAS.clip = audioClips[2];
        m_safeAS.Play();
        m_lockedOut = true;
        lockedOutPanel.SetActive(true);
    }
}
