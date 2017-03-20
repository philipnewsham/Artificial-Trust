using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LightController : MonoBehaviour
{
    public Light[] lights;
    private bool[] m_lightOn;
    private AIPower m_aiPowerScript;
    private int m_lightPower;
    public TaskLog taskLogScript;
	void Start ()
    {
        m_aiPowerScript = gameObject.GetComponent<AIPower>();
        m_lightPower = m_aiPowerScript.lightPower;
        m_lightOn = new bool[lights.Length];
        for (int i = 0; i < lights.Length; i++)
        {
            m_lightOn[i] = true;
        }
        CheckLights();
	}

    private int m_lightOne;
    private int m_lightTwo;
    private float m_timeNeeded;
    private float m_countingDown;
    private bool m_isCountingDown;
    private bool m_lightOneOn;
    private bool m_lightTwoOn;

    public void LightSwitchObjectiveOrder(int lightOne, int lightTwo, float timeNeeded)
    {
        m_lightOne = lightOne;
        m_lightTwo = lightTwo;
        m_timeNeeded = timeNeeded;
        m_countingDown = m_timeNeeded;
    }

    public void CurrentLightPower(int newPower)
    {
        m_lightPower += newPower;
    }
    public ScientistObjectives scientistObjectiveScript;

    public void LightSwitch(int lightNo)
    {
        if(m_lightOn[lightNo] == true)
        {
            lights[lightNo].enabled = false;
            m_lightOn[lightNo] = !m_lightOn[lightNo];
            m_aiPowerScript.PowerExchange(m_lightPower);
            taskLogScript.UpdateText("Light", lightNo, 1);
        }
        else
        {
            if(m_aiPowerScript.CheckPower(m_lightPower) == true)
            {
                lights[lightNo].enabled = true;
                m_lightOn[lightNo] = !m_lightOn[lightNo];
                m_aiPowerScript.PowerExchange(-m_lightPower);
                taskLogScript.UpdateText("Light", lightNo, 0);
            }
        }
        CheckLights();
        
        if(lightNo == m_lightOne && !m_lightOneOn)
        {
            m_isCountingDown = true;
            m_lightOneOn = true;
        }

        if (lightNo == m_lightTwo && m_lightOneOn && m_isCountingDown)
        {
            scientistObjectiveScript.CheckLightSequence();
        }
    }

    void Update()
    {
        if(m_isCountingDown)
        {
            m_countingDown -= Time.deltaTime;
            if(m_countingDown <= 0)
            {
                m_lightOneOn = false;
                m_isCountingDown = false;
            }
        }
    }

    void CheckLights()
    {
        int lightsOnInt = 0;

        for (int i = 0; i < m_lightOn.Length; i++)
        {
            if (m_lightOn[i])
            {
                lightsOnInt += 1;
            }
        }
        scientistObjectiveScript.CheckLights(lightsOnInt);
    }

	public Button[] lightButtons;

	public void NoPower()
	{
		for (int i = 0; i < lightButtons.Length; i++) 
		{
			if (!m_lightOn [i]) 
			{
				lightButtons [i].interactable = false;
			}
		}
	}

    public void EnoughPower()
    {
        for (int i = 0; i < lightButtons.Length; i++)
        {
            if (!lightButtons[i].interactable)
            {
                lightButtons[i].interactable = true;
            }
        }
    }
}