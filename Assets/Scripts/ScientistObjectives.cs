using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ScientistObjectives : MonoBehaviour
{
    int m_lightAmount;
    int m_cameraAmount;
    bool m_subGoalOne;
    bool m_subGoalTwo;
    bool m_subGoalThree;
    string m_subGoalOneText;

    string[] m_lightLocations = new string[8] {"Archive Room","Dr. Kirkoff's Office","Small Office","Server Room","Main Laboratory","Corridor One","Corridor Two","Corridor Three"};

    int m_firstLight;
    int m_secondLight;
    float m_seconds;

    public LightController lightController;
    void Start ()
    {
        SubGoal();
        ChooseLightSequence();
        ObjectiveText();
	}	
	
	void SubGoal ()
    {
        m_lightAmount = Random.Range(0, 8);
        m_cameraAmount = Random.Range(0, 8);
	}

    public void CheckLights(int lightsOn)
    {
        if(lightsOn == m_lightAmount)
            m_subGoalOne = true;
        else
            m_subGoalOne = false;

        CheckObjectives();
    }

    public void CheckCameras(int camerasOn)
    {
        if (camerasOn == m_cameraAmount)
            m_subGoalTwo = true;
        else
            m_subGoalTwo = false;

        CheckObjectives();
    }


    void ChooseLightSequence()
    {
        //switch
        m_firstLight = Random.Range(0, 8);
        //then
        m_secondLight = Random.Range(0, 8);
        //within
        m_seconds = Random.Range(30, 60);
        lightController.LightSwitchObjectiveOrder(m_firstLight, m_secondLight, m_seconds);
    }


    public void CheckLightSequence()
    {
        m_subGoalThree = true;
        CheckObjectives();
    }

    public Text objectiveText;

    void ObjectiveText()
    {
        string objectiveOne = string.Format("Have {0} lights on at the same time", m_lightAmount);
        string objectiveTwo = string.Format("Have {0} cameras enabled at the same time", m_cameraAmount);
        string objectiveThree = string.Format("Switch the light in {0}, then switch the light in {1} within {2} seconds", m_lightLocations[m_firstLight], m_lightLocations[m_secondLight], m_seconds);

        objectiveText.text = "Current Objectives:";
        objectiveText.text += string.Format("\n{0}", objectiveOne);
        objectiveText.text += string.Format("\n{0}", objectiveTwo);
        objectiveText.text += string.Format("\n{0}", objectiveThree);
    }

    void CheckObjectives()
    {
        if (m_subGoalOne && m_subGoalTwo && m_subGoalThree)
            print("all goals completed");
    }
}
