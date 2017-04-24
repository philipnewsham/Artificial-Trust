using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public float maxTime;
    public bool isTesting;

    private float m_maxTime = 545f;
    private float m_count = 0f;
    private float m_multiplier = 1f;
    private bool m_isCountingDown;
    public AIWin aiWinScript;
    public AgentWin agentWinScript;
    public UpdatedElevator elevatorScript;

    public Text[] timerTextboxes;

    void Start()
    {
        if(maxTime != 0f)
            m_maxTime = maxTime;
        
        if (isTesting)
            BeginCountdown();
    }

    public void BeginCountdown()
    {
        m_isCountingDown = true;
    }

    public void ChangeMultiplier(float newMult)
    {
        m_multiplier = newMult;
    }

    void Update()
    {
        if(m_isCountingDown)
        {
            m_count += Time.deltaTime * m_multiplier;
            for (int i = 0; i < timerTextboxes.Length; i++)
            {
                timerTextboxes[i].text = string.Format("{0}:{1}", Mathf.Floor((m_maxTime - m_count)/60f), Mathf.Floor((m_maxTime - m_count) % 60f)/*, Mathf.Floor((m_maxTime - m_count) % 10f)*/);
            }
            
            if(m_count >= m_maxTime)
            {
                m_isCountingDown = false;
                for (int i = 0; i < timerTextboxes.Length; i++)
                {
                    timerTextboxes[i].text = string.Format("00:00");
                }
                GameOver();
            }
        }
    }
    public GameObject aiEnd;
    public GameObject aiLoses;
    public GameObject aiWins;

    public GameObject agentEnd;
    public GameObject agentWins;
    public GameObject agentLoses;
    void GameOver()
    {
        agentEnd.SetActive(true);
        aiEnd.SetActive(true);
        if (!elevatorScript.agentEscaped)
        {
            if(aiWinScript.mainObjective == 1)
            {
                aiWins.SetActive(true);
            }
            agentLoses.SetActive(true);
        }
        else
        {
            agentWins.SetActive(true);
        }

        if(!elevatorScript.aiEscaped && aiWinScript.mainObjective == 0)
        {
            aiLoses.SetActive(true);
        }
        else if (elevatorScript.aiEscaped && aiWinScript.mainObjective == 0)
        {
            aiWins.SetActive(true);
        }
    }
    /*
    public int[] possibleTimes;
    private int[] m_possibleTimes { get { return possibleTimes; } }
    private float m_currentTime;
    private float m_timerSpeed = 1f;
    public bool countingDown;

    public GameObject scientistLosesCanvas;
    public GameObject aiLosesCanvas;
	// Use this for initialization
	void Start ()
    {
        //m_currentTime = m_possibleTimes[(Random.Range(0, m_possibleTimes.Length))];
        //print(m_currentTime);
	}
	
	// Update is called once per frame
	void Update () {
        if (countingDown)
        {
            m_currentTime -= m_timerSpeed * Time.deltaTime;
            if (m_currentTime <= 0)
            {
                print("gameover");
                countingDown = false;
                ShutDown();
            }
        }
	}

    public void ChangeTime(int newTime)
    {
        newTime *= 60;
        print(newTime + " seconds");
        m_currentTime = newTime;
    }

    public void ChangeTimeSpeed(float newSpeed)
    {
        m_timerSpeed = newSpeed;
    }

    void ShutDown()
    {
        scientistLosesCanvas.SetActive(true);
        aiLosesCanvas.SetActive(true);
        Invoke("MainMenu", 10f);
    }

    void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    */
}