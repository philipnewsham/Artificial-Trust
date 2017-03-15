using UnityEngine;
using System.Collections;

public class ScientistObjectives : MonoBehaviour
{
    int m_lightAmount;
    int m_cameraAmount;
    bool m_subGoalOne;
    bool m_subGoalTwo;
    bool m_subGoalThree;
	// Use this for initialization
	void Start ()
    {
        SubGoal();
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
    }
}
