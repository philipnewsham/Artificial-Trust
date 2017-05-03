using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class RobotBody : MonoBehaviour {
    public int powerNeeded;
    private int m_powerNeeded { get { return powerNeeded; } }
    private float m_powerGiven;

    public GameObject robotBodyCanvas;
    public InputField passwordInputField;
    public Slider choosePowerSlider;
    public Button enterRobotButton;

    private int m_currentPower;
    private bool m_poweringUp = false;

    private bool m_poweredLock = false;
    private bool m_passwordLock = false;
    private bool m_switchLock = false;

    public Text poweredText;
    public Text passwordText;
    public Text switchesText;
    public Text poweredUpPercentage;

    public GameObject gameController;

    private PasswordGenerator m_passwordGeneratorScript;
    private string m_password;

    public GameObject robotBodyController;
    public GameObject aiCamera;

    public GameObject ai;
    private AIPower m_aiPower;
    private DoorController m_doorControllerScript;

	// Use this for initialization
	void Start () {
        m_passwordGeneratorScript = gameController.GetComponent<PasswordGenerator>();
        m_aiPower = ai.GetComponent<AIPower>();
        m_doorControllerScript = ai.GetComponent<DoorController>();
        Invoke("GetPassword", 1f);
        CheckLocks();
	}

    void GetPassword()
    {
        m_password = gameObject.GetComponent<ReceivePasswords>().password;
    }

    public void PoweringUp()
    {
        if(!m_poweringUp)
        {
            m_currentPower = m_aiPower.CurrentPower();
            m_aiPower.PowerExchange(-m_currentPower);
            m_poweringUp = true;
        }
        /*
        if (!m_poweringUp)
        {
            float percentage = Mathf.FloorToInt(choosePowerSlider.value);
            m_currentPower = Mathf.FloorToInt((m_aiPower.totalPower * 0.01f) * percentage);
            print(m_aiPower.totalPower * 0.01f);
            print(m_currentPower);
            if (m_aiPower.CheckPower(m_currentPower) == true)
            {
                m_poweringUp = true;
                m_aiPower.PowerExchange(-m_currentPower);
            }
            else
            {
                print("Not enough Power");
            }
            
        }
        */
    }

    public void StopPowering()
    {
        if (m_poweringUp)
        {
            m_poweringUp = false;
            m_aiPower.PowerExchange(m_currentPower);
        }
    }

    void Update()
    {
        if (m_poweringUp)
        {
            m_powerGiven += m_currentPower * Time.deltaTime;
            poweredUpPercentage.text = string.Format("{0}% Powered Up",Mathf.FloorToInt(m_powerGiven / powerNeeded * 100));
            if(m_powerGiven >= m_powerNeeded)
            {
                poweredUpPercentage.text = "100% Powered Up!";
                StopPowering();
                m_poweredLock = true;
                CheckLocks();
                print("Robot Powered Up!");
                
            }
        }
    }

    public void SwitchesSet()
    {
        m_switchLock = true;
        CheckLocks();
    }


    void CheckLocks()
    {
        if(m_poweredLock)
            poweredText.text = string.Format("<color=#00FF00>Lock Status (Power): Unlocked = {0}</color>", m_poweredLock);
        else
            poweredText.text = string.Format("<color=red>Lock Status (Power): Unlocked = {0}</color>", m_poweredLock);

        if(m_passwordLock)
            passwordText.text = string.Format("<color=#00FF00>Lock Status (Password): Unlocked = {0}</color>", m_passwordLock);
        else
            passwordText.text = string.Format("<color=red>Lock Status (Password): Unlocked = {0}</color>", m_passwordLock);

        if(m_switchLock)
            switchesText.text = string.Format("<color=#00FF00>Lock Status (Switches): Unlocked = {0}</color>", m_switchLock);
        else
            switchesText.text = string.Format("<color=red>Lock Status (Switches): Unlocked = {0}</color>", m_switchLock);

        if (m_poweredLock && m_passwordLock && m_switchLock)
        {
            enterRobotButton.interactable = true;
        }
    }

    public void CheckPassword()
    {
        if(passwordInputField.text == m_password)
        {
            m_passwordLock = true;
            passwordInputField.interactable = false;
            CheckLocks();
        }
        else
        {
            passwordInputField.text = "";
        }
    }

    public void SavedUnlocking()
    {
        m_passwordLock = true;
        m_poweredLock = true;
        m_switchLock = true;
        CheckLocks();
    }

    public void EnterRobot()
    {
        Invoke("SwapToBody", 1f);
        robotBodyCanvas.GetComponent<Animator>().SetTrigger("Enter");
        m_doorControllerScript.AllPowerOff();
    }

    void SwapToBody()
    {
        robotBodyCanvas.SetActive(false);
        robotBodyController.SetActive(true);
        aiCamera.SetActive(false);
    }
}
