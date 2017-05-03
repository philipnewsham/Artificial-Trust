using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class AIPower : MonoBehaviour
{
    public int lightPower;
    private int m_lightPower;

    public int cameraPower;
    private int m_cameraPower;

    public int doorLockedPower;
    private int m_doorLockedPower;

    public int doorUnlockedPower;
    private int m_doorUnlockedPower;

    public int aiSwitchButtonPower;
    private int m_aiSwitchButtonPower;

    public int startingPower;
    private int m_startingPower { get { return startingPower; } }

    public int totalPower;
    private int m_totalPower;

    public Text[] powerText;

    private HackingDocuments m_hackingDocScript;

    public LightToggleInstantiate m_lightToggleInstantiateScript;
    
    public CameraToggleInstantiate m_cameraToggleInstantiateScript;
    public DoorToggleInstantiate m_doorToggleInstantiateScript;
    public RobotBodyPasswordButton robotBodyPasswordButtonScript;
    public Button aiSwitchButton;
	// Use this for initialization
	void Start ()
    {
        m_totalPower = totalPower;
        m_lightPower = lightPower;
        m_cameraPower = cameraPower;
        m_doorUnlockedPower = doorUnlockedPower;
        m_doorLockedPower = doorLockedPower;
        UpdatePowerText();
		UpdatePowerBar ();
        m_hackingDocScript = gameObject.GetComponent<HackingDocuments>();
        m_hackingDocScript.powerValue = m_totalPower;
        m_aiSwitchButtonPower = aiSwitchButtonPower;
	}

    public void PowerExchange(int power)
    {
        m_totalPower += power;
        UpdatePowerText();
        m_hackingDocScript.powerValue = m_totalPower;
        CheckLights();
        CheckCameras();
        CheckDoors();
        CheckButton();
        robotBodyPasswordButtonScript.CurrentPower(m_totalPower);
        totalPower = m_totalPower;
		UpdatePowerBar ();
    }

	public Image powerBar;
	private float m_maxpower = 240;


	void UpdatePowerBar()
	{
		float totalPow = m_totalPower;
		//print (m_totalPower + " / " + m_maxpower + " = " + totalPow / m_maxpower);
		powerBar.fillAmount = totalPow / m_maxpower;
	}

    public int CurrentPower()
    {
        return m_totalPower;
    }

    void CheckButton()
    {
        if(m_totalPower >= m_aiSwitchButtonPower)
        {
            aiSwitchButton.interactable = true;
        }
        else
        {
            aiSwitchButton.interactable = false;
        }
    }

    void CheckLights()
    {
        if (m_totalPower >= m_lightPower)
        {
            m_lightToggleInstantiateScript.EnoughPower();
            GetComponent<LightController>().EnoughPower();
        }
        else
        {
            m_lightToggleInstantiateScript.NotEnoughPower();
            GetComponent<LightController>().NoPower();
        }
    }

    void CheckCameras()
    {
        if (m_totalPower >= m_cameraPower)
        {
            m_cameraToggleInstantiateScript.EnoughPower();
            GetComponent<CameraController>().EnoughPower();
        }
        else
        {
            m_cameraToggleInstantiateScript.NotEnoughPower();
            GetComponent<CameraController>().NoPower();
        }
    }

    void CheckDoors()
    {
        if(m_totalPower < m_doorUnlockedPower)
        {
            GetComponent<DoorController>().NotEnoughPowerUnlocked();
        }
        else if (m_totalPower < m_doorLockedPower && m_totalPower > m_doorUnlockedPower)
        {
            GetComponent<DoorController>().NotEnoughPowerLocked();
        }
        else
        {
            GetComponent<DoorController>().EnoughPower();
        }
        /*
        if (m_totalPower >= m_doorLockedPower)
        {
            m_doorToggleInstantiateScript.EnoughPowerLocked();
        }
        else
        {
            m_doorToggleInstantiateScript.NotEnoughPowerLocked();
        }

        if (m_totalPower >= m_doorUnlockedPower)
        {
            m_doorToggleInstantiateScript.EnoughPower();
        }
        else
        {
            m_doorToggleInstantiateScript.NotEnoughPower();
        }
        */
    }

    void UpdatePowerText()
    {
        for (int i = 0; i < powerText.Length; i++)
        {
            powerText[i].text = string.Format("Current Power: {0}", m_totalPower);
        }
    }

    public bool CheckPower(int powerRequest)
    {
        if(m_totalPower - powerRequest >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ChangePowerValues(string name, int newPower)
    {
        //print("Yeezy yeezy what's good?");
        if(name == "Lights")
        {
            lightPower += newPower;
        }
        if(name == "Cameras")
        {
            cameraPower += newPower;
        }
        if(name == "Doors")
        {
            doorUnlockedPower += newPower;
        }

    }

    void OriginalPower(string name)
    {
        if(name == "Lights")
        {
            lightPower = m_lightPower;
        }
        if(name == "Cameras")
        {
            cameraPower = m_cameraPower;
        }
        if(name == "Doors")
        {
            doorUnlockedPower = m_doorUnlockedPower;
        }
    }
}
