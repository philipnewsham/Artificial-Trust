using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
public class Elevator : MonoBehaviour
{
    public bool firstElevator;
    //public GameObject otherElevator;
    public Elevator otherElevatorScript;

    public GameObject scientist;
    private ScientistWin m_scientistWinScript;
    public GameObject scientistDecidesCanvas;
    public GameObject scientistSavedCanvas;
    public GameObject scientistSavedElevatorCanvas;
    public GameObject scientistLosesCanvas;
    public GameObject scientistWinsCanvas;
    private bool m_scientistChosen;

    public GameObject ai;
    private AIWin m_aiWinScript;
    public GameObject aiWinsCanvas;
    public GameObject aiDecidesCanvas;
    public GameObject aiSavedCanvas;
    public GameObject aiSavedElevatorCanvas;
    public GameObject aiLosesCanvas;
    private bool m_scientistWins;
    private bool m_aiWins;

    public GameObject elevator;
    private Animator m_elevatorAnim;

    public GameObject robotBody;
    private RobotBody m_robotBodyScript;

    public FirstPersonController robotBodyController;

    private AudioSource[] m_audioSources;

    public int countdownTimer;
    private float m_countingDownTime;
    private bool m_isCountingDown;
    public GameObject scientistCountdownCanvas;
    private Text m_scientistCountdownText;
    public GameObject aiCountdownCanvas;
    private Text m_aiCountdownText;
    private bool m_secondPlayerIsScientist;
    void Start()
    {
        m_scientistWinScript = scientist.GetComponent<ScientistWin>();
        m_aiWinScript = ai.GetComponent<AIWin>();
        m_elevatorAnim = elevator.GetComponent<Animator>();
        m_robotBodyScript = robotBody.GetComponent<RobotBody>();
        m_audioSources = gameObject.GetComponents<AudioSource>();
        m_countingDownTime = countdownTimer;
        m_scientistCountdownText = scientistCountdownCanvas.GetComponentInChildren<Text>();
        m_aiCountdownText = aiCountdownCanvas.GetComponentInChildren<Text>();
        m_elevatorAnim.SetTrigger("Close");
        if(firstElevator)
        {
            Invoke("OpenElevator", 5f);
        }
        
        
    }

    void OpenElevator()
    {
        m_elevatorAnim.SetTrigger("Open");
        m_audioSources[1].Play();
    }

    void Update()
    {
        if (m_scientistWins && !m_scientistChosen)
        {
            if (Input.GetButtonDown("ControllerA"))
            {
                OtherPlayerSaved();
                aiSavedCanvas.SetActive(true);
                m_scientistChosen = true;
                scientistDecidesCanvas.SetActive(false);
                scientistWinsCanvas.SetActive(true);
            }
            if (Input.GetButtonDown("ControllerB"))
            {
                LeavePlayer("AI");
                m_scientistChosen = true;
                scientistDecidesCanvas.SetActive(false);
                scientistWinsCanvas.SetActive(true);
            }
        }

        if(m_isCountingDown)
        {
            m_countingDownTime -= 1 * Time.deltaTime;
            if (m_secondPlayerIsScientist)
            {
                m_scientistCountdownText.text = string.Format("Time remaining before building goes into lockdown: {0}s!", Mathf.FloorToInt(m_countingDownTime));
            }
            else
            {
                m_aiCountdownText.text = string.Format("Time remaining before building goes into lockdown: {0}s!", Mathf.FloorToInt(m_countingDownTime));
            }
            if(m_countingDownTime <= 0)
            {
                CountDownOver();
            }
        }
    }

    void CountDownStart(string otherPlayer)
    {
        m_isCountingDown = true;
        if(otherPlayer == "Scientist")
        {
            scientistCountdownCanvas.SetActive(true);
        }
        else
        {
            aiCountdownCanvas.SetActive(true);
        }
    }

    void CountDownOver()
    {
        scientistCountdownCanvas.SetActive(false);
        aiCountdownCanvas.SetActive(false);
        if (m_secondPlayerIsScientist)
        {
            scientistLosesCanvas.SetActive(true);
        }
        else
        {
            aiLosesCanvas.SetActive(true);
        }
        otherElevatorScript.SecondElevator("Close");
        Invoke("ExitToMainMenu", 5f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (firstElevator)
        {
            if (other.gameObject.tag == "Scientist")
            {
                print("Scientist");
                if (m_scientistWinScript.completedTask)
                {
                    ElevatorOn("Scientist");
                    m_scientistWins = true;
                    m_scientistWinScript.enteredElevator = true;
                    m_robotBodyScript.SavedUnlocking();
                    m_secondPlayerIsScientist = false;
                    CountDownStart("AI");
                }
            }
            else
            {
                print("AI");
                if (m_aiWinScript.completedTask)
                {
                    robotBodyController.UpdateCursorLock();
                    ElevatorOn("AI");
                    m_aiWins = true;
                    m_secondPlayerIsScientist = true;
                    CountDownStart("Scientist");
                }
            }
        }
        else
        {
            if(other.gameObject.tag == "Scientist")
            {
                ShowSavedCanvas("Scientist");
            }
            else
            {
                //robotBodyController.UpdateCursorLock();
                ShowSavedCanvas("AI");
            }
            otherElevatorScript.m_isCountingDown = false;
            
            Invoke("ExitToMainMenu", 10);
        }
    }

    void ElevatorOn(string player)
    {
        m_elevatorAnim.SetTrigger("Close");
        m_audioSources[1].Play();
        m_audioSources[0].Play();
        print("Elevator close");
        if(player == "Scientist")
        {
            scientistDecidesCanvas.SetActive(true);
        }
        else
        {
            aiDecidesCanvas.SetActive(true);
        }
    }

    public void OtherPlayerSaved()
    {
        if (firstElevator)
        {
            otherElevatorScript.SecondElevator("Open");
            //otherElevator.GetComponent<AudioSource>().Play();
            print("other elevator open");
            if (!m_scientistWins)
            {
                scientistSavedCanvas.SetActive(true);
            }
        }
    }

    public void SecondElevator(string trigger)
    {
        m_elevatorAnim.SetTrigger(trigger);
        m_audioSources[1].Play();
        m_audioSources[0].Play();
    }

    void ShowSavedCanvas(string player)
    {
        if(player == "Scientist")
        {
            scientistSavedElevatorCanvas.SetActive(true);
        }
        else
        {
            aiSavedElevatorCanvas.SetActive(true);
        }
        m_elevatorAnim.SetTrigger("Close");
    }

    public void LeavePlayer(string otherPlayer)
    {
        if(otherPlayer == "Scientist")
        {
            scientistLosesCanvas.SetActive(true);
        }
        else
        {
            aiLosesCanvas.SetActive(true);
        }
        Invoke("ExitToMainMenu", 10);
    }

    void ExitToMainMenu()
    {
        robotBodyController.UpdateCursorLock();
        SceneManager.LoadScene(0);
    }
}
