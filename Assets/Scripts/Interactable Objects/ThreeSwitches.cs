﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class ThreeSwitches : MonoBehaviour {
    private bool m_switchOne;
    private bool m_switchTwo;
    private bool m_switchThree;

    private int[] m_switchPostionInt = new int[3];

    public GameObject robotBody;
    private RobotBody m_robotBodyScript;

    private int[] m_actionNumber = new int[8];
    private List<int> m_numberList = new List<int>();
    private int m_currentNumber;

    public GameObject gameController;
    private Timer m_timerScript;

    public GameObject blackoutCanvas;
    private Blackout m_blackoutScript;

    public GameObject ai;
    private AIPower m_aiPowerScript;
    private DoorController m_doorControllerScript;
    private LightController m_lightControllerScript;
    private CameraController m_cameraController;
    private HackingDocuments m_hackingDocumentScript;

    private int m_powerSent;
    private string[] m_poweredObjects = new string[3] { "Lights", "Cameras", "Doors" };
    private int[] m_powerIncreased = new int[3] { 5, 10, 15};
    private int m_randObjects;
    private int m_randPower;

    public string[] m_switchPositions = new string[8];
    public string[] m_actions = new string[8];
    private string[] m_actionMessages = new string[8];

    public Text scientistSwitchInfo;

    public GameObject switchesButton;
    private AudioSource m_buttonAudioSource;
    private Animator m_buttonAnimator;

    public DoorToggleInstantiate doorToggleInstantiateScript;
    public SwitchToggles switchToggleScript;
    // Use this for initialization
    void Start ()
    {
        m_blackoutScript = blackoutCanvas.GetComponent<Blackout>();
        m_doorControllerScript = ai.GetComponent<DoorController>();
        m_lightControllerScript = ai.GetComponent<LightController>();
        m_cameraController = ai.GetComponent<CameraController>();
        m_aiPowerScript = ai.GetComponent<AIPower>();
        m_robotBodyScript = robotBody.GetComponent<RobotBody>();
        m_timerScript = gameController.GetComponent<Timer>();
        m_hackingDocumentScript = ai.GetComponent<HackingDocuments>();
        m_buttonAudioSource = switchesButton.GetComponent<AudioSource>();
        m_buttonAnimator = switchesButton.GetComponent<Animator>();
        m_randObjects = Random.Range(0, 3);
        m_randPower = Random.Range(0, 3);

        for (int i = 0; i < m_actionNumber.Length; i++)
        {
            m_numberList.Add(i);
        }

        for (int i = 0; i < m_actionNumber.Length; i++)
        {
            int randomNo = Random.Range(0, m_numberList.Count);
            m_actionNumber[i] = m_numberList[randomNo];

            m_numberList.Remove(m_numberList[randomNo]);
            if (m_actionNumber[i] != 7)
            {
                m_actionMessages[i] = string.Format("Switches on {0} will {1}", m_switchPositions[i], m_actions[m_actionNumber[i]]);
            }
            else
            {
                m_actionMessages[i] = string.Format("Switches on {0} will increase {1} by {2} power",m_switchPositions[i], m_poweredObjects[m_randObjects], m_powerIncreased[m_randPower]);
            }
            if(i == 0)
            {
                m_currentNumber = m_actionNumber[i];
                //print(m_currentNumber);
            }
            if(m_actionNumber[i] == 3)
            {
                robotBodySwitches.text = string.Format("Switches are required to be at: {0}", m_switchPositions[i]);
            }
        }
        ScientistSwitchInfo();
        AISwitchInfo();
    }
    public Text robotBodySwitches;
	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Switches();
        }
    }
    private int[] m_goalSwitchPositions = new int[3];
    public AgentObjectives agentObjectives;
    public void AgentSwitchPositions(int posOne, int posTwo, int posThree)
    {
        m_goalSwitchPositions[0] = posOne;
        m_goalSwitchPositions[1] = posTwo;
        m_goalSwitchPositions[2] = posThree;
		//print(string.Format("{0} {1} {2}",posOne,posTwo,posThree));
    }

    public void SwitchOne()
    {
        m_switchOne = !m_switchOne;

		m_switchPostionInt [0] = (m_switchPostionInt [0] + 1) % 2;

        Switches();
        switchToggleScript.SwitchFlipped(0, m_switchOne);
    }

    public void SwitchTwo()
    {
        m_switchTwo = !m_switchTwo;
		m_switchPostionInt [1] = (m_switchPostionInt [1] + 1) % 2;
        Switches();
        switchToggleScript.SwitchFlipped(1, m_switchTwo);
    }

    public void SwitchThree()
    {
        m_switchThree = !m_switchThree;
		m_switchPostionInt [2] = (m_switchPostionInt [2] + 1) % 2;
        Switches();
        switchToggleScript.SwitchFlipped(2, m_switchThree);
    }
    public Text currentSwitchesText;
    void Switches()
    {
        if (m_switchPostionInt[0] == 0 && m_switchPostionInt[1] == 0 && m_switchPostionInt[2] == 0) { m_currentNumber = m_actionNumber[0]; }
        if (m_switchPostionInt[0] == 0 && m_switchPostionInt[1] == 0 && m_switchPostionInt[2] == 1) { m_currentNumber = m_actionNumber[1]; }
        if (m_switchPostionInt[0] == 0 && m_switchPostionInt[1] == 1 && m_switchPostionInt[2] == 0) { m_currentNumber = m_actionNumber[2]; }
        if (m_switchPostionInt[0] == 0 && m_switchPostionInt[1] == 1 && m_switchPostionInt[2] == 1) { m_currentNumber = m_actionNumber[3]; }
        if (m_switchPostionInt[0] == 1 && m_switchPostionInt[1] == 0 && m_switchPostionInt[2] == 0) { m_currentNumber = m_actionNumber[4]; }
        if (m_switchPostionInt[0] == 1 && m_switchPostionInt[1] == 0 && m_switchPostionInt[2] == 1) { m_currentNumber = m_actionNumber[5]; }
        if (m_switchPostionInt[0] == 1 && m_switchPostionInt[1] == 1 && m_switchPostionInt[2] == 0) { m_currentNumber = m_actionNumber[6]; }
        if (m_switchPostionInt[0] == 1 && m_switchPostionInt[1] == 1 && m_switchPostionInt[2] == 1) { m_currentNumber = m_actionNumber[7]; }
        currentSwitchesText.text = string.Format("Switches are currently at: {0},{1},{2}", m_switchPostionInt[0], m_switchPostionInt[1], m_switchPostionInt[2]);
        }
    /*
    void Switches()
    {
        if (m_switchOne)
        {
            SwitchOneOn();
        }
        else
        {
            SwitchOneOff();
        }
    }
  
    void SwitchOneOn()
    {
        if (m_switchTwo)
        {
            if (m_switchThree)
            {
                m_currentNumber = m_actionNumber[0];
            }
            else
            {
                m_currentNumber = m_actionNumber[1];
            }
        }
        else
        {
            if (m_switchThree)
            {
                m_currentNumber = m_actionNumber[2];
            }
            else
            {
                m_currentNumber = m_actionNumber[3];
            }
        }
        print(m_currentNumber);
    }

    void SwitchOneOff()
    {
        if (m_switchTwo)
        {
            if (m_switchThree)
            {
                m_currentNumber = m_actionNumber[4];
            }
            else
            {
                m_currentNumber = m_actionNumber[5];
            }
        }
        else
        {
            if (m_switchThree)
            {
                m_currentNumber = m_actionNumber[6];
            }
            else
            {
                m_currentNumber = m_actionNumber[7];
            }
        }
        print(m_currentNumber);
    }
    */

    public void Interact()
    {
        m_buttonAudioSource.Play();
        m_buttonAnimator.SetTrigger("Pressed");
        if(m_currentNumber == 0)
        {
            print("do nothing");
        }
        else if(m_currentNumber == 1)
        {
            //m_timerScript.ChangeTime(5);
            //m_timerScript.countingDown = true;
        }
        else if(m_currentNumber == 2)
        {
            m_blackoutScript.EnterBlackout(10);
        }
        else if (m_currentNumber == 3)
        {
            m_robotBodyScript.SwitchesSet();
        }
        else if (m_currentNumber == 4)
        {
            m_powerSent = 20;
            PowerExchange();
            m_powerSent = -20;
            Invoke("PowerExchange", 10f);
        }
        else if (m_currentNumber == 5)
        {
            m_powerSent = -20;
            PowerExchange();
            m_powerSent = 20;
            Invoke("PowerExchange", 10f);
        }
        else if (m_currentNumber == 6)
        {
            doorToggleInstantiateScript.AllDoorsAreLocked();
            doorToggleInstantiateScript.LockedOutAction();
            m_doorControllerScript.LockAllDoors();
        }
        else if (m_currentNumber == 7)
        {
            m_aiPowerScript.ChangePowerValues(m_poweredObjects[m_randObjects], m_powerIncreased[m_randPower]);
            if(m_poweredObjects[m_randObjects] == "Lights")
            {
                m_lightControllerScript.CurrentLightPower(m_powerIncreased[m_randPower]);
            }
            else if(m_poweredObjects[m_randObjects] == "Doors")
            {
                m_doorControllerScript.CurrentDoorPower(m_powerIncreased[m_randPower]);
            }
            else if(m_poweredObjects[m_randObjects] == "Cameras")
            {
                m_cameraController.CurrentCameraPower(m_powerIncreased[m_randPower]);
            }
        }

		if (m_switchPostionInt[0] == m_goalSwitchPositions[0] && m_switchPostionInt[1] == m_goalSwitchPositions[1] && m_switchPostionInt[2] == m_goalSwitchPositions[2]) { agentObjectives.SwitchPositions(0, true); }
    }

    void PowerExchange()
    {
        m_aiPowerScript.PowerExchange(m_powerSent);
    }

    void ScientistSwitchInfo()
    {
        scientistSwitchInfo.text = string.Format("Switches: \n{0} \n{1} \n{2} \n{3}", m_actionMessages[0], m_actionMessages[1], m_actionMessages[2], m_actionMessages[3]);
    }
    public Button[] documentButtons;
    void AISwitchInfo()
    {
        for (int i = 6; i < 9; i++)
        {
            m_hackingDocumentScript.RecieveDocumentMessages(m_actionMessages[i - 1], i);
            documentButtons[i - 6].GetComponent<DocumentButton>().documentText = m_actionMessages[i - 1];
        }
    }
}
