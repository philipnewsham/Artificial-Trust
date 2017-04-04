using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChoosingMainObjectives : MonoBehaviour
{
    private int m_aiObjective;
    private int m_agentObjective;

    private string[] m_aiObjectiveString = new string[2]
    {
        "Main Objective:\nUnlock the Robot Body and escape the laboratory",
        "Main Objective:\nKeep the Agent busy until the timer runs out"
    };

    private string[] m_agentObjectiveString = new string[2]
    {
        "Main Objective:\nUnlock the safe and escape with the information inside",
        "Main Objective:\nBreak into the AI Hub and switch the AI off"
    };

    public Text[] objectiveTextboxes;
	// Use this for initialization
	void Start ()
    {
        m_aiObjective = Random.Range(0, 2);
        m_agentObjective = Random.Range(0, 2);
        objectiveTextboxes[0].text = m_aiObjectiveString[m_aiObjective];
        objectiveTextboxes[1].text = m_agentObjectiveString[m_agentObjective];
	}
}
