using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgentObjectiveText : MonoBehaviour 
{
	int[] m_sizes = new int[3]{24,20,14}; //current, next, completed
	int m_currentObjective;
	public Text[] objectives;

	void Start()
	{
		objectives [m_currentObjective].fontSize = m_sizes [0];
	}

	public void CompletedTask()
	{
		objectives [m_currentObjective].fontSize = m_sizes [2];
		objectives [m_currentObjective].fontStyle = FontStyle.Italic;
		m_currentObjective += 1;
		if (m_currentObjective < objectives.Length) 
		{
			objectives [m_currentObjective].fontSize = m_sizes [0];
		}
	}
}
