using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentObjectives : MonoBehaviour
{
    private int[] m_subGoals = new int[3];
    private bool[] m_goalComplete = new bool[3];
    private List<int> m_goalTypes = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    private int m_randomNo;
    private int m_goalAmount;
    void Start()
    {
        m_goalAmount = m_subGoals.Length;
        for (int i = 0; i < m_goalAmount; i++)
        {
            m_randomNo = Random.Range(0, 10 - i);
            m_subGoals[i] = m_goalTypes[m_randomNo];
            m_goalTypes.RemoveAt(m_randomNo);
        }
    }

    void ChoosingGoals()
    {
        for (int i = 0; i < m_goalAmount; i++)
        {
           switch(m_subGoals[i])
            {
                case 0:
                    LightsOnObjective(i, 8, false);
                    break;
                case 1:
                    CamerasOnObjective(i, 8, false);
                    break;
                case 2:
                    //LightSwitchSequenceObjective();
                    break;
                case 3:
                    WaitInRoomObjective();
                    break;
                default:
                    print("Error");
                    break;
            }
        }
    }

    //how many lights should be on
    int m_lightAmount;
    int m_lightOnGoalNo;
    public void LightsOnObjective(int goalNo, int lightsOn, bool isComplete)
    {
        if(!isComplete)
        {
            m_lightOnGoalNo = goalNo;
            m_lightAmount = Random.Range(0, 8);
        }
        else
        {
            if (lightsOn == m_lightAmount)
                m_goalComplete[goalNo] = true;
            else
                m_goalComplete[goalNo] = false;

            CheckObjectives();
        }
    }

    //how many cameras should be on
    int m_cameraAmount;
    int cooGoalNo;
    void CamerasOnObjective(int goalNo, int camerasOn, bool isComplete)
    {
        if (!isComplete)
        {
            cooGoalNo = goalNo;
            m_cameraAmount = Random.Range(0, 8);
        }
        else
        {
            if (camerasOn == m_cameraAmount)
                m_goalComplete[cooGoalNo] = true;
            else
                m_goalComplete[cooGoalNo] = false;

            CheckObjectives();
        }
    }
    //switch two lights in order within the time
    int m_firstLight;
    int m_secondLight;
    int lssoGoalNo;
    float m_seconds;
    public LightController lightController;
    void LightSwitchSequenceObjective(int goalNo, bool isComplete)
    {
        if(!isComplete)
        {
            lssoGoalNo = goalNo;
            //switch
            m_firstLight = Random.Range(0, 8);
            //then
            m_secondLight = Random.Range(0, 8);
            //within
            m_seconds = Random.Range(30, 60);
            lightController.LightSwitchObjectiveOrder(m_firstLight, m_secondLight, m_seconds);
        }
        else
        {
            m_goalComplete[lssoGoalNo] = true;
            CheckObjectives();
        }

    }
    //wait inside a room for a certain amount of time
    void WaitInRoomObjective()
    {

    }

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
}
