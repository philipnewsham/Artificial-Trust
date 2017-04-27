using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
public class Safe : MonoBehaviour
{
    private string m_safePassword;
    private ReceivePasswords m_receivePasswordScript;

    public Canvas safeCanvas;
    public GameObject safeCanvasGO;
    public Button openSafeButton;
    public Image starSignImage;
    public Sprite[] starSigns;
    public InputField passwordInputField;
    public Button[] starSignButtons;
    public GameObject lockedOutPanel;
    public Text lockedOutText;

    private int m_currentStarSignNo = 0;

    private int[] m_rankOrder = new int[4];

    private int m_pressOrder = 1;
    private int m_correctStarSign;

    private bool m_buttonLock = false;
    private bool m_passwordLock = false;
    private bool m_starsignLock = false;

    private bool m_lockedOut = false;
    private int m_lockedOutTime = 10;
    private float m_countingDown;

    public Text sequenceText;
    public Text passwordText;
    public Text starsignText;

    public Animator doorPivot;

    public GameObject gameController;

    private bool m_isSearching = false;

    public GameObject scientist;
    private FirstPersonController m_firstPersonControllerScript;
    private FreezeControls m_freezeControls;
    private ScientistWin m_scientistWinScript;

    public GameObject ai;
    private HackingDocuments m_hackingDocumentScript;

    private bool m_isOpen = false;
    private bool m_isEmpty = false;

    private bool m_isActive = false;

    public GameObject safeLock;
    private SafeLocks m_safeLockScript;

    private string[] shapeNames = new string[4] { "Square", "Pentagon", "Circle", "Triangle" };
    public ScientistComputer scientistComputerScript;
    
    // Use this for initialization
    void Start () {
        m_safeLockScript = safeLock.GetComponent<SafeLocks>();
        m_firstPersonControllerScript = scientist.GetComponent<FirstPersonController>();
        m_freezeControls = gameController.GetComponent<FreezeControls>();
        m_scientistWinScript = scientist.GetComponent<ScientistWin>();
        m_hackingDocumentScript = ai.GetComponent<HackingDocuments>();
        m_receivePasswordScript = gameObject.GetComponent<ReceivePasswords>();

        m_countingDown = m_lockedOutTime;
        
        Invoke("GetPassword", 1f);
        starSignImage.sprite = starSigns[m_currentStarSignNo];
        RandomiseButtonOrder();
        m_correctStarSign = Random.Range(0, 12);
        
        //print("correct star sign is: " + m_correctStarSign);
        CheckLocks();
    }


    void Update()
    {
        if (m_lockedOut)
        {
            m_countingDown -= 1 * Time.deltaTime;
            lockedOutText.text = string.Format("PLEASE WAIT {0} SECONDS BEFORE TRYING AGAIN", Mathf.FloorToInt(m_countingDown));
            if(m_countingDown <= 0f)
            {
                lockedOutPanel.SetActive(false);
                m_lockedOut = false;
                m_countingDown = m_lockedOutTime;
            }
        }
    }

    public void Interact()
    {
        if (!m_isOpen)
        {
            m_isSearching = !m_isSearching;
            //Cursor.lockState = CursorLockMode.Confined;
            m_freezeControls.FirstPersonControllerEnabled(!m_isSearching);
            safeCanvasGO.SetActive(m_isSearching);
        }
        else
        {
            if (!m_isEmpty)
            {
                EmptySafe();
            }
            else
            {
                //do nothing?
            }
        }
    }

    void EmptySafe()
    {
        m_scientistWinScript.CheckWinCondition(0);
    }

    public void LeaveCanvas()
    {
        safeCanvasGO.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        gameController.GetComponent<FreezeControls>().FirstPersonControllerEnabled(true);
    }
	
    public void ChangeStarSignUp()
    {
        if(m_currentStarSignNo == starSigns.Length - 1)
        {
            m_currentStarSignNo = 0;
        }
        else
        {
            m_currentStarSignNo += 1;
        }
        starSignImage.sprite = starSigns[m_currentStarSignNo];
    }

    public void ChangeStarSignDown()
    {
        if (m_currentStarSignNo == 0)
        {
            m_currentStarSignNo = starSigns.Length - 1;
        }
        else
        {
            m_currentStarSignNo -= 1;
        }
        starSignImage.sprite = starSigns[m_currentStarSignNo];
    }

    public void CheckStarSign()
    {
        if(m_currentStarSignNo == m_correctStarSign)
        {
            m_starsignLock = true;
            for (int i = 0; i < starSignButtons.Length; i++)
            {
                starSignButtons[i].interactable = false;
            }
            CheckLocks();
        }
        else
        {
            LockOut();
        }
    }

    void RandomiseButtonOrder()
    {
        int button1ID = Random.Range(0,100);
        int button2ID = Random.Range(0, 100);
        int button3ID = Random.Range(0, 100);
        int button4ID = Random.Range(0, 100);

        int currentRank = 1;
        for (int i = 0; i < 100; i++)
        {
            if (i == button1ID)
            {
                m_rankOrder[0] = currentRank;
                currentRank += 1;
            }
            if (i == button2ID)
            {
                m_rankOrder[1] = currentRank;
                currentRank += 1;
            }
            if (i == button3ID)
            {
                m_rankOrder[2] = currentRank;
                currentRank += 1;
            }
            if (i == button4ID)
            {
                m_rankOrder[3] = currentRank;
                currentRank += 1;
            }
        }

        /*for (int i = 0; i < m_rankOrder.Length; i++)
        {
            print(m_rankOrder[i]);
        }*/
        //print(string.Format("{0},{1},{2},{3}", m_rankOrder[0], m_rankOrder[1], m_rankOrder[2], m_rankOrder[3]));
        string message = string.Format("////CONFIDENTIAL////\n---For Authorised Personnel Only---\n The order of the buttons are: \n{0},{1},{2},{3}", shapeNames[m_rankOrder[0]-1], shapeNames[m_rankOrder[1]-1], shapeNames[m_rankOrder[2]-1], shapeNames[m_rankOrder[3]-1]);//all goes wrong, remove shapenames part
        m_hackingDocumentScript.RecieveDocumentMessages(message, 3);
        documentButton.GetComponent<DocumentButton>().documentText = string.Format("The order of the shapes are: \n{0}, {1}, {2}, {3}", shapeNames[m_rankOrder[0] - 1], shapeNames[m_rankOrder[1] - 1], shapeNames[m_rankOrder[2] - 1], shapeNames[m_rankOrder[3] - 1]);
        scientistComputerScript.ReceiveSequence(message);
        for (int i = 0; i < 4; i++)
        {
            m_safeLockScript.sequenceOrder[i] = m_rankOrder[i]; 
        }

    }
    public Button documentButton;
    public void ButtonSequence(int currentButton)
    {
        if(m_pressOrder == m_rankOrder[currentButton])
        {
            //correct
            m_pressOrder += 1;
            print("correct");
            if(m_pressOrder > 4)
            {
                m_buttonLock = true;
                CheckLocks();
                //unlocked
            }
        }
        else
        {
            m_pressOrder = 1;
            print("Wrong!");
            LockOut();
            //reset
        }
    }

    void ResetButtons()
    {

    }

    public void CheckPassword()
    {
        if (passwordInputField.text == m_safePassword)
        {
            m_passwordLock = true;
            passwordInputField.interactable = false;
            CheckLocks();
        }
        else
        {
            passwordInputField.text = "";
            LockOut();
        }
    }

    void CheckLocks()
    {
        sequenceText.text = string.Format("Lock Status (Button Sequence): Unlocked = {0}", m_buttonLock);
        passwordText.text = string.Format("Lock Status (Password): Unlocked = {0}", m_passwordLock);
        starsignText.text = string.Format("Lock Status (Starsign): Unlocked = {0}", m_starsignLock);
        if (m_buttonLock && m_passwordLock && m_starsignLock)
        {
            openSafeButton.interactable = true;
            
        }
    }

    void GetPassword()
    {
        m_safePassword = m_receivePasswordScript.password;
    }

    void LockOut()
    {
        lockedOutPanel.SetActive(true);
        m_lockedOut = true;
    }

    public void Unlocked()
    {
        doorPivot.SetTrigger("Unlocked");
        safeCanvas.enabled = false;
    }
}
