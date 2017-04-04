using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgentObjectives : MonoBehaviour
{
    private int[] m_subGoals = new int[3];
    private bool[] m_goalComplete = new bool[3];
	private string[] m_goalTexts = new string[3];
    private List<int> m_goalTypes = new List<int>() { 0, 1, 2, 3, 4, 5};
    private int m_randomNo;
    private int m_goalAmount;

    void Start()
    {
        m_goalAmount = m_subGoals.Length;
        for (int i = 0; i < m_goalAmount; i++)
        {
            m_randomNo = Random.Range(0, m_goalTypes.Count);
            m_subGoals[i] = m_goalTypes[m_randomNo];
            m_goalTypes.RemoveAt(m_randomNo);
        }
		ChoosingGoals ();
    }

	public Text objectiveText;
    public Text[] objectiveTexts;
    private string[] objectiveStrings = new string[3];
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
				LightSwitchSequenceObjective(i, false);
                    break;
                case 3:
				WaitInRoomObjective(i, false);
                    break;
                case 4:
                    SwitchPositions(i, false);
                    break;
                case 5:
                    UnlockComputer(i, false);
                    break;
                default:
                    print("Error");
                    break;
            }
        }
		objectiveText.text = "Current Objectives:";
		for (int i = 0; i < m_subGoals.Length; i++) 
		{
            objectiveStrings[i] = string.Format("{0}", m_goalTexts[i]);
            objectiveTexts[i].text = objectiveStrings[i];
        }
    }

    //how many lights should be on
    int m_lightAmount;
    int m_lightOnGoalNo;
	bool m_isLightObjectective;
    public void LightsOnObjective(int goalNo, int lightsOn, bool isComplete)
    {
        if(!isComplete)
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
					m_goalComplete [m_lightOnGoalNo] = true;
				else
					m_goalComplete [m_lightOnGoalNo] = false;

				CheckObjectives ();
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
					m_goalComplete [cameraOnGoalNo] = true;
				else
					m_goalComplete [cameraOnGoalNo] = false;

				CheckObjectives ();
			}
        }
    }

    //switch two lights in order within the time
    int m_firstLight;
    int m_secondLight;
    int lightSequenceGoalNo;
    float m_seconds;
    public LightController lightController;
	string[] m_lightLocations = new string[8] {"Archive Room","Dr. Kirkoff's Office","Small Office","Server Room","Main Laboratory","Corridor One","Corridor Two","Corridor Three"};
    public void LightSwitchSequenceObjective(int goalNo, bool isComplete)
    {
        if(!isComplete)
        {
            lightSequenceGoalNo = goalNo;
            //switch
            m_firstLight = Random.Range(0, 8);
            //then
            m_secondLight = Random.Range(0, 8);
            //within
            m_seconds = Random.Range(30, 60);
            lightController.LightSwitchObjectiveOrder(m_firstLight, m_secondLight, m_seconds);
			m_goalTexts[goalNo] = string.Format("Switch the light in {0}, then switch the light in {1} within {2} seconds", m_lightLocations[m_firstLight], m_lightLocations[m_secondLight], m_seconds);
        }
        else
        {
            m_goalComplete[lightSequenceGoalNo] = true;
            CheckObjectives();
        }

    }

    //wait inside a room for a certain amount of time
	int m_roomWaitNo;
	float m_roomWaitSeconds;
	int m_waitRoomGoalNo;
	private string[] m_roomName = new string[9] { "Main Laboratory", "Small Office", "Server Room", "AI HUB", "Archives", "Dr. Kirkoff's Office", "Corridor One", "Corridor Two", "Corridor Three" };
	public void WaitInRoomObjective(int goalNo, bool isComplete)
    {
		if (!isComplete) {
			m_waitRoomGoalNo = goalNo;
			m_roomWaitNo = Random.Range (0, 6);
			m_roomWaitSeconds = Random.Range (4, 12);
			GetComponent<CheckRoom> ().WaitObjective (m_roomWaitNo, m_roomWaitSeconds);
			m_goalTexts[goalNo] = string.Format("Wait inside {0} for {1} seconds", m_roomName[m_roomWaitNo], m_roomWaitSeconds);
		}
		else 
		{
			m_goalComplete[m_waitRoomGoalNo] = true;
			CheckObjectives();
		}
    }

    //set switch positions
    int[] m_switchPositions = new int[3];
    public ThreeSwitches switchesScript;
    private int m_switchGoalNo;
    public void SwitchPositions(int goalNo, bool isComplete)
    {
        if(!isComplete)
        {
            m_switchGoalNo = goalNo;
            for (int i = 0; i < 3; i++)
            {
                m_switchPositions[i] = Random.Range(0, 2);
            }
            m_goalTexts[goalNo] = string.Format("Put the switches in the server room to {0}, {1}, {2}", m_switchPositions[0], m_switchPositions[1], m_switchPositions[2]);
            switchesScript.AgentSwitchPositions(m_switchPositions[0], m_switchPositions[1], m_switchPositions[2]);
        }
        else
        {
            m_goalComplete[m_switchGoalNo] = true;
            CheckObjectives();
        }
    }

    //unlock computer in small office
    private int m_computerGoalNo;
    bool m_isComputerGoal;
    public void UnlockComputer(int goalNo, bool isComplete)
    {
        if(!isComplete)
        {
            m_computerGoalNo = goalNo;
            m_isComputerGoal = true;
            m_goalTexts[goalNo] = string.Format("Unlock the computer in the small office");
        }
        else
        {
            if(m_isComputerGoal)
            {
                m_goalComplete[m_computerGoalNo] = true;
                CheckObjectives();
            }
        }
    }

    /*
	void ObjectiveText()
	{
		string objectiveOne = string.Format("Have {0} lights on at the same time", m_lightAmount);
		string objectiveTwo = string.Format("Have {0} cameras enabled at the same time", m_cameraAmount);
		string objectiveThree = string.Format("Switch the light in {0}, then switch the light in {1} within {2} seconds", m_lightLocations[m_firstLight], m_lightLocations[m_secondLight], m_seconds);
		string objectiveFour = string.Format("Wait inside {0} for {1} seconds", m_roomName[m_roomWaitNo], m_roomWaitSeconds);

		objectiveText.text = "Current Objectives:";
		objectiveText.text += string.Format("\n{0}", objectiveOne);
		objectiveText.text += string.Format("\n{0}", objectiveTwo);
		objectiveText.text += string.Format("\n{0}", objectiveThree);
		objectiveText.text += string.Format("\n{0}", objectiveFour);
	}
	*/

    public SwitchOffAI switchOffAIScript;

    void CheckObjectives()
    {
        int goalsDone = 0;
        switchOffAIScript.UpdateSubObjectives(m_goalComplete);
        for (int i = 0; i < m_goalAmount; i++)
        {
            if (m_goalComplete[i])
            {
                goalsDone += 1;
                objectiveTexts[i].text = string.Format("{0} Done!", objectiveStrings[i]);
            }
            else
            {
                objectiveTexts[i].text = string.Format("{0}", objectiveStrings[i]);
            }
        }

        if (goalsDone == m_goalAmount)
            print("Sub Objectives Completed!");
    }
}
