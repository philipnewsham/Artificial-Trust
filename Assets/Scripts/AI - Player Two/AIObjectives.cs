using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AIObjectives : MonoBehaviour
{
    private int[] m_subGoals = new int[3];
    private List<int> m_goalTypes = new List<int>() { 0, 1, 2, 3, 4, 5 };
    private int m_randomNo;

    private string[] m_goalTexts = new string[3];
    private bool[] m_goalComplete = new bool[3];

    private int m_goalAmount;
    public Text objectiveText;
    void Start()
    {
        m_goalAmount = m_subGoals.Length;
        for (int i = 0; i < m_subGoals.Length; i++)
        {
            m_randomNo = Random.Range(0, m_goalTypes.Count);
            m_subGoals[i] = m_goalTypes[m_randomNo];
            m_goalTypes.RemoveAt(m_randomNo);
        }
        ChoosingGoals();
    }

    void ChoosingGoals()
    {
        for (int i = 0; i < m_subGoals.Length; i++)
        {
            switch (m_subGoals[i])
            {
                case 0:
                    LightsOnObjective(i, 8, false);
				    break;
                case 1:
                    CamerasOnObjective(i, 8, false);
                    break;
                case 2:
                    SpecificLightsOnObjective(i, null, false);
                    break;
                case 3:
                    WaitInRoomObjective(i, false);
                    break;
                case 4:
                    BlindObjective(i, false);
                    break;
                case 5:
                    PanicButtonObjective(i);
                    break;
                default:
                    print("Error");
                    break;
            }
        }

        objectiveText.text = "Current Objectives:";
        for (int i = 0; i < m_subGoals.Length; i++)
        {
            objectiveText.text += string.Format("\n{0}", m_goalTexts[i]);
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

               CheckObjectives();
            }
        }
    }

    //how many cameras should be on
    int m_cameraAmount;
    int cameraOnGoalNo;
    bool m_isCameraObjective;
    public void CamerasOnObjective(int goalNo, int camerasOn, bool isComplete)
    {
        if (!isComplete)
        {
            cameraOnGoalNo = goalNo;
            m_isCameraObjective = true;
            m_cameraAmount = Random.Range(0, 8);
            m_goalTexts[goalNo] = string.Format("Have {0} cameras enabled at the same time", m_cameraAmount);
        }
        else
        {
            if (m_isCameraObjective)
            {
                if (camerasOn == m_cameraAmount)
                    m_goalComplete[cameraOnGoalNo] = true;
                else
                    m_goalComplete[cameraOnGoalNo] = false;

                CheckObjectives();
            }
        }
    }

    //certain lights on
    bool[] m_lightsOn = new bool[8];
    int specificLightGoalNo;
    public void SpecificLightsOnObjective(int goalNo, bool[] lightsOn, bool isComplete)
    {
        if(!isComplete)
        {
            specificLightGoalNo = goalNo;
            for (int i = 0; i < m_lightsOn.Length; i++)
            {
                int truefalse = Random.Range(0, 2);
                if (truefalse == 0)
                    m_lightsOn[i] = false;
                else
                    m_lightsOn[i] = true;
            }
            m_goalTexts[goalNo] = string.Format("Have lights: 0 {0}, 1 {1}, 2 {2}, 3 {3}, 4 {4}, 5 {5}, 6 {6}, 7 {7}", m_lightsOn[0], m_lightsOn[1], m_lightsOn[2], m_lightsOn[3], m_lightsOn[4], m_lightsOn[5], m_lightsOn[6], m_lightsOn[7]);
        }
        else
        {
            int checkNo = 0;
            for (int i = 0; i < m_lightsOn.Length; i++)
            {
                if (m_lightsOn[i] == lightsOn[i])
                    checkNo += 1;
                else
                    break;
            }

            if(checkNo == m_lightsOn.Length)
            {
                m_goalComplete[specificLightGoalNo] = true;
                CheckObjectives();
            }
        }
    }

    //wait inside a room for a certain amount of time
    int m_roomWaitNo;
    float m_roomWaitSeconds;
    int m_waitRoomGoalNo;
    private string[] m_roomName = new string[9] { "Main Laboratory", "Small Office", "Server Room", "AI HUB", "Archives", "Dr. Kirkoff's Office", "Corridor One", "Corridor Two", "Corridor Three" };
    public CheckRoom checkRoomScript;
    public void WaitInRoomObjective(int goalNo, bool isComplete)
    {
        if (!isComplete)
        {
            m_waitRoomGoalNo = goalNo;
            m_roomWaitNo = Random.Range(0, 6);
            m_roomWaitSeconds = Random.Range(4, 12);
            checkRoomScript.WaitObjectiveAI(m_roomWaitNo, m_roomWaitSeconds);
            m_goalTexts[goalNo] = string.Format("Have the agent wait inside {0} for {1} seconds", m_roomName[m_roomWaitNo], m_roomWaitSeconds);
        }
        else
        {
            m_goalComplete[m_waitRoomGoalNo] = true;
            CheckObjectives();
        }
    }

    //all cameras Off
    float m_blindTime;
    int m_blindGoalNo;
    private CameraController m_cameraController;
    public void BlindObjective(int goalNo, bool isComplete)
    {
        if(!isComplete)
        {
            m_blindGoalNo = goalNo;
            m_blindTime = Random.Range(12, 24);
            GetComponent<CameraController>().BlindObjective(m_blindTime);
            m_goalTexts[goalNo] = string.Format("Have all cameras disabled for {0} seconds", m_blindTime);
        }
        else
        {
            m_goalComplete[m_blindGoalNo] = true;
            CheckObjectives();
        }
    }

    //press panic button
    int m_panicButtonGoalNo;
    bool m_panicObjectiveComplete;
    public void PanicButtonObjective(int goalNo)
    {
        if(!m_panicObjectiveComplete)
        {
            m_panicButtonGoalNo = goalNo;
            m_panicObjectiveComplete = true;
            if(goalNo != 3)
                m_goalTexts[goalNo] = string.Format("Press the panic button");
        }
        else
        {
            m_goalComplete[m_panicButtonGoalNo] = true;
            CheckObjectives();
        }
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
