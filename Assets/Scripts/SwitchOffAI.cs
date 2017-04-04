using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SwitchOffAI : MonoBehaviour
{
    private float[] m_leverTimers = new float[3];
    private float[] m_multipliers = new float[3];
    public bool[] subObjectives = new bool[3];
    bool[] m_countingDown = new bool[3];
    public float maxTime;
    private int m_currentLever;
    private bool m_leverOn;
    public Image[] percentImages;

    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            m_multipliers[i] = 1f;
        }
    }
    public void UpdateSubObjectives(bool[] objectivesComplete)
    {
        for (int i = 0; i < 3; i++)
        {
            subObjectives[i] = objectivesComplete[i];
            if (subObjectives[i])
                m_multipliers[i] = 2.5f;
            else
                m_multipliers[i] = 1f;
        }
    }
	
    public void Interacted(int lever)
    {
        m_countingDown[lever] = !m_countingDown[lever];
        if(m_countingDown[lever])
        {
            m_leverOn = true;
            for (int i = 0; i < 3; i++)
            {
                if(i != lever)
                {
                    m_countingDown[i] = false;
                }
                else
                {
                    m_currentLever = i;
                }
            }
        }
        else
        {
            m_leverOn = false;
        }
    }
    // Update is called once per frame
    bool[] m_leverComplete = new bool[3];
	void Update ()
    {
        if (m_leverOn)
        {
            if (m_countingDown[m_currentLever] && !m_leverComplete[m_currentLever])
            {
                m_leverTimers[m_currentLever] += Time.deltaTime * m_multipliers[m_currentLever];
                percentImages[m_currentLever].fillAmount = m_leverTimers[m_currentLever] / maxTime;
                if (m_leverTimers[m_currentLever] >= maxTime)
                {
                    m_leverComplete[m_currentLever] = true;
                }
            }
        }
	}
    public GameObject aiLoseScreen;
    void CheckLevers()
    {
        int goalsDone = 0;
        for (int i = 0; i < 3; i++)
        {
            if (m_leverComplete[i])
                goalsDone += 1;
            else
                break;
        }

        if (goalsDone == 3)
        {
            //AI is switched off.
        }
    }
}
