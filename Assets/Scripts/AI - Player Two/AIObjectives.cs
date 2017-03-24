using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIObjectives : MonoBehaviour
{
    private int[] m_subGoals = new int[3];
    private List<int> m_goalTypes = new List<int>() { 0 };
    private int m_randomNo;

    private string[] m_goalTexts = new string[3];
    private bool[] m_goalComplete = new bool[3];

    void Start()
    {
        for (int i = 0; i < m_subGoals.Length; i++)
        {
            m_randomNo = Random.Range(0, 10 - i);
            m_subGoals[i] = m_goalTypes[m_randomNo];
            m_goalTypes.RemoveAt(m_randomNo);
        }
        
    }

    void ChoosingGoals()
    {
        for (int i = 0; i < m_subGoals.Length; i++)
        {
            switch (m_subGoals[i])
            {
                case 0:
				//do thing
				break;

            }
        }
    }

    //how many lights should be on
    int m_lightAmount;
    int m_lightOnGoalNo;
    bool m_isLightObjectective;
    public void LightsOnObjective(int goalNo, int lightsOn, bool isComplete)
    {
        if (!isComplete)
        {
            m_lightOnGoalNo = goalNo;
            m_isLightObjectective = true;
            m_lightAmount = Random.Range(0, 8);
            m_goalTexts[goalNo] = string.Format("Have {0} lights on at the same time", m_lightAmount);
        }
        else
        {
            if (m_isLightObjectective)
            {
                if (lightsOn == m_lightAmount)
                    m_goalComplete[m_lightOnGoalNo] = true;
                else
                    m_goalComplete[m_lightOnGoalNo] = false;

               //CheckObjectives();
            }
        }
    }
	/*
    void CheckObjectives()
    {
        int goalsDone = 0;
        for (int i = 0; i < m_goalAmount; i++)
        {
            if (m_goalComplete[i])
                goalsDone += 1;
            else
                break;
        }

        if (goalsDone == m_goalAmount)
            print("Sub Objectives Completed!");
    }
    */
}
