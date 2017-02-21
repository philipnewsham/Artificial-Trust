using UnityEngine;
using System.Collections;

public class AIWin : MonoBehaviour {
    public string[] winConditions;
    private int m_winCondition;
    private bool m_completedTask = true;
    public bool completedTask;
    public GameObject scientistLoseCanvas;
    // Use this for initialization
    void Start()
    {
        m_winCondition = Random.Range(0, winConditions.Length);
        completedTask = m_completedTask;
    }

    public void CheckWinCondition(int taskNo)
    {
        if (taskNo == m_winCondition)
        {
            m_completedTask = true;
            completedTask = m_completedTask;
        }
    }

    public void SaveScientist()
    {

    }

    /*
    public void TrapScientist()
    {
        scientistLoseCanvas.SetActive(true);
    }
    */
}
