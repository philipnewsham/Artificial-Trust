using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DoorToggleInstantiate : MonoBehaviour
{
    public GameObject doorToggles;
    private GameObject m_ai;
    private AIPower m_aiPowerScript;
    private int m_doorLockPower;
    private DoorController m_doorControllerScript;
    private int m_doorsLength;
    private Toggle[] m_doorToggleArray = new Toggle[2];
    private Toggle[] m_doorLockedToggleArray;
    private Toggle[] m_doorPoweredToggleArray;
    private float[] m_countingDown;
    private bool[] m_isCounting;
    public float shutOutTime;
    private float m_shutOutTime;
    private bool[] m_lockedOutAction;
    private bool m_allPowerOn = true;
    private bool m_allLocksOn;

    public Font textFont;

    
	// Use this for initialization
	void Start ()
    {
        m_ai = GameObject.FindGameObjectWithTag("AI");
        m_aiPowerScript = m_ai.GetComponent<AIPower>();
        m_doorLockPower = m_aiPowerScript.doorLockedPower;
        m_doorControllerScript = m_ai.GetComponent<DoorController>();
        m_doorsLength = m_doorControllerScript.m_specificDoorScripts.Length;
        m_doorLockedToggleArray = new Toggle[m_doorsLength];
        m_doorPoweredToggleArray = new Toggle[m_doorsLength];
        m_countingDown = new float[m_doorsLength];
        m_isCounting = new bool[m_doorsLength];
        m_lockedOutAction = new bool[m_doorsLength];
        m_shutOutTime = shutOutTime;

        for (int i = 0; i < m_doorsLength; i++)
        {
            GameObject doorToggleClone = Instantiate(doorToggles, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            //new Text[3] cloneText = doorToggleClone.GetComponentsInChildren<Text>();
            Text[] cloneText = new Text[3];
            cloneText = doorToggleClone.GetComponentsInChildren<Text>();
            for (int j = 0; j < 3; j++)
            {
                cloneText[j].font = textFont;
            }
            //.font = textFont;
            doorToggleClone.transform.SetParent(gameObject.transform);

            DoorToggle doorToggleScript = doorToggleClone.GetComponent<DoorToggle>();

            m_doorToggleArray = doorToggleClone.GetComponentsInChildren<Toggle>();
            m_doorLockedToggleArray[i] = m_doorToggleArray[0];
            m_doorPoweredToggleArray[i] = m_doorToggleArray[1];

            doorToggleScript.doorID = i;
            doorToggleScript.ChangeName();

            m_countingDown[i] = m_shutOutTime;

            m_lockedOutAction[i] = false;
        }
	}

    public void NotEnoughPowerLocked()
    {
        for (int i = 0; i < m_doorsLength; i++)
        {
            if (m_doorLockedToggleArray[i].isOn == false)
            {
                m_doorLockedToggleArray[i].interactable = false;
            }
        }
    }

    public void LockedOutAction()
    {
        for (int i = 0; i < m_doorsLength; i++)
        {
            m_lockedOutAction[i] = true;
        }
    }

    public void EnoughPowerLocked()
    {
        for (int i = 0; i < m_doorsLength; i++)
        {
            if (m_doorLockedToggleArray[i].isOn == false && m_isCounting[i] == false)
            {
                if (!m_lockedOutAction[i] && m_aiPowerScript.CheckPower(m_doorLockPower)) 
                {
                    m_doorLockedToggleArray[i].interactable = true;
                }
            }
        }
    }

    public void NotEnoughPower()
    {
        for (int i = 0; i < m_doorsLength; i++)
        {
            if (m_doorPoweredToggleArray[i].isOn == false)
            {
                m_doorPoweredToggleArray[i].interactable = false;
            }
        }
    }

    public void EnoughPower()
    {
        for (int i = 0; i < m_doorsLength; i++)
        {
            if (m_doorPoweredToggleArray[i].isOn == false)
            {
                if (!m_lockedOutAction[i])
                {
                    m_doorPoweredToggleArray[i].interactable = true;
                }
            }
        }
    }

    public void DisabledLock(int doorID)
    {
        //m_doorLockedToggleArray[doorID].isOn = false;
       // m_doorLockedToggleArray[doorID].interactable = false;
        m_isCounting[doorID] = true;
        m_doorControllerScript.Locking(doorID);
        m_lockedOutAction[doorID] = false;
    }

    void Update()
    {
        for (int i = 0; i < m_doorsLength; i++)
        {
            if (m_isCounting[i])
            {
                m_countingDown[i] -= 1 * Time.deltaTime;
                if (m_countingDown[i] <= 0f)
                {
                    m_isCounting[i] = false;
                    m_countingDown[i] = m_shutOutTime;
                    if (m_aiPowerScript.CheckPower(m_doorLockPower) == true)
                    {
                        ReEnableDoorLock(i);
                    }
                }
            }
        }
    }

    public void AllDoorsAreLocked()
    {
        for (int i = 0; i < m_doorsLength; i++)
        {
            m_doorLockedToggleArray[i].interactable = false;
        }
    }

    void ReEnableDoorLock(int doorID)
    {
        m_doorLockedToggleArray[doorID].interactable = true;
    }

    public void AllPowerOn()
    {
        m_allPowerOn = !m_allPowerOn;
        if (!m_allPowerOn)
        {
            for (int i = 0; i < m_doorsLength; i++)
            {
                if (m_doorPoweredToggleArray[i].isOn)
                {
                    m_doorPoweredToggleArray[i].isOn = false;
                }
            }
        }
        else
        {
            for (int i = 0; i < m_doorsLength; i++)
            {
                if(!m_doorPoweredToggleArray[i].isOn && m_doorPoweredToggleArray[i].interactable)
                {
                    m_doorPoweredToggleArray[i].isOn = true;
                }
            }
        }
    }

    public void AllLocksOn()
    {
        m_allLocksOn = !m_allLocksOn;
        if (!m_allLocksOn)
        {
            for (int i = 0; i < m_doorsLength; i++)
            {
                if (m_doorLockedToggleArray[i].isOn)
                {
                    m_doorLockedToggleArray[i].isOn = false;
                }
            }
        }
        else
        {
            for (int i = 0; i < m_doorsLength; i++)
            {
                if (!m_doorLockedToggleArray[i].isOn && m_doorLockedToggleArray[i].interactable)
                {
                    m_doorLockedToggleArray[i].isOn = true;
                }
            }
        }
    }
}
