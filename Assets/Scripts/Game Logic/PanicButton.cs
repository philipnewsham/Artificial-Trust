using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PanicButton : MonoBehaviour {
    private int m_scientistButtonNo;
    private int m_aiButtonNo;
    private bool m_scientistPressed = false;
    private bool m_aiPressed = false;

    public GameObject robotBody;
    private RobotBody m_robotBodyScript;

    public GameObject gameController;
    private Timer m_timerScript;

    public GameObject blackoutCanvas;
    private Blackout m_blackoutScript;

    public GameObject ai;
    private AIPower m_aiPowerScript;
    private DoorController m_doorControllerScript;
    private HackingDocuments m_hackingDocumentScript;

    private int m_powerSent;
    private string[] m_poweredObjects = new string[3] { "Lights", "Cameras", "Doors" };
    private int[] m_powerIncreased = new int[3] { 5, 10, 15 };

    public string[] m_actions = new string[8];
    private string[] m_actionMessages = new string[8];

    public GameObject aiButtonPressedGO;
    private Text m_aiButtonPressedText;
    //this is what the ai will know about the scientist
    public Text aiKnowledgeText;

    public GameObject scientistButtonPressedGO;
    private Text m_scientistButtonPressedText;
    //this is what the scientist will know about the ai
    public Text scientistKnowledgeText;

    public DoorToggleInstantiate doorToggleInstantiateScript;
    // Use this for initialization
    void Start()
    {
        m_scientistButtonNo = Random.Range(0, 8);
        m_aiButtonNo = Random.Range(0, 8);
        m_blackoutScript = blackoutCanvas.GetComponent<Blackout>();
        m_doorControllerScript = ai.GetComponent<DoorController>();
        m_aiPowerScript = ai.GetComponent<AIPower>();
        m_robotBodyScript = robotBody.GetComponent<RobotBody>();
        m_timerScript = gameController.GetComponent<Timer>();
        m_hackingDocumentScript = ai.GetComponent<HackingDocuments>();
        m_aiButtonPressedText = aiButtonPressedGO.GetComponent<Text>();
        m_scientistButtonPressedText = scientistButtonPressedGO.GetComponentInChildren<Text>();
        TextWriteUp();
    }

    public void Interact()//scientist panic button
    {
        print("interacted with panic button - Scientist");
        if (!m_scientistPressed)
        {
            PerformAction(m_scientistButtonNo);
            scientistButtonPressedGO.SetActive(true);
            Invoke("SwitchOffScientistText", 6f);
            m_scientistPressed = true;
        }
    }
    //writing up what the buttons will do
    void TextWriteUp()
    {
        scientistKnowledgeText.text = string.Format("The AI's panic button will {0}", m_actions[m_aiButtonNo]);
        aiKnowledgeText.text = string.Format("The Scientist's panic button will {0}", m_actions[m_scientistButtonNo]);
        m_aiButtonPressedText.text = string.Format("Pressing this button meant that you will {0}", m_actions[m_aiButtonNo]);
        aiButtonPressedGO.SetActive(false);
        m_scientistButtonPressedText.text = string.Format("Pressing this button meant that you will {0}", m_actions[m_scientistButtonNo]);
        scientistButtonPressedGO.SetActive(false);

    }


    void SwitchOffScientistText()
    {
        scientistButtonPressedGO.SetActive(false);
    }

    void SwitchOffAIText()
    {
        aiButtonPressedGO.SetActive(false);
    }

    public void AIPanicButton()
    {
        print("Interacted with panic button - AI");
        if (!m_aiPressed)
        {
            PerformAction(m_aiButtonNo);
            aiButtonPressedGO.SetActive(true);
            Invoke("SwitchOffAIText", 6f);
            m_aiPressed = true;
        }
    }

    void PerformAction(int actionNo)
    {
        if (actionNo == 0)
        {
            print("do nothing");
        }
        else if (actionNo == 1)
        {
           // m_timerScript.ChangeTime(5);
           // m_timerScript.countingDown = true;
        }
        else if (actionNo == 2)
        {
            m_blackoutScript.EnterBlackout(10);
        }
        else if (actionNo == 3)
        {
            m_robotBodyScript.SwitchesSet();
        }
        else if (actionNo == 4)
        {
            m_powerSent = 20;
            PowerExchange();
            m_powerSent = -20;
            Invoke("PowerExchange", 10f);
        }
        else if (actionNo == 5)
        {
            m_powerSent = -20;
            PowerExchange();
            m_powerSent = 20;
            Invoke("PowerExchange", 10f);
        }
        else if (actionNo == 6)
        {
            doorToggleInstantiateScript.AllDoorsAreLocked();
            doorToggleInstantiateScript.LockedOutAction();
            m_doorControllerScript.LockAllDoors();
        }
        else if (actionNo == 7)
        {
            int randObj = Random.Range(0, m_poweredObjects.Length);
            int randPow = Random.Range(0, m_powerIncreased.Length);
            m_aiPowerScript.ChangePowerValues(m_poweredObjects[randObj], m_powerIncreased[randPow]);
        }
    }

    void PowerExchange()
    {
        m_aiPowerScript.PowerExchange(m_powerSent);
    }
}
