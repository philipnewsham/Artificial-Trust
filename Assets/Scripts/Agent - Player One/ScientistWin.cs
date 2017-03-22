using UnityEngine;
using System.Collections;

public class ScientistWin : MonoBehaviour {
    public string[] winConditions;
    private int m_winCondition;
    private bool m_completedTask = false;
    public bool completedTask;
    public GameObject aiLoseCanvas;
    private bool m_aiFateChosen = false;
    public bool enteredElevator = false;
	// Use this for initialization
	void Start () {
        m_winCondition = Random.Range(0, winConditions.Length);
        completedTask = m_completedTask;
	}
	
    public void CheckWinCondition(int taskNo)
    {
        if(taskNo == m_winCondition)
        {
            m_completedTask = true;
            completedTask = m_completedTask;
        }
    }

    void Update()
    {
        if (!m_aiFateChosen && enteredElevator)
        {
            if (Input.GetButtonDown("ControllerA"))
            {
                if (m_completedTask)
                {
                    AISaved();
                    m_aiFateChosen = true;
                }
            }
            if (Input.GetButtonDown("ControllerB"))
            {
                if (m_completedTask)
                {
                    AIDeleted();
                    m_aiFateChosen = true;
                }
            }
        }
    }

    void AISaved()
    {
        
    }

    void AIDeleted()
    {
        aiLoseCanvas.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
       
    }
}
