using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LightToggleInstantiate : MonoBehaviour {
    public GameObject lightToggles;
    private GameObject m_ai;
    private LightController m_lightControllerScript;
    private Toggle[] m_lightToggleArray;
    private LightToggle[] m_lightToggleScripts;
    private int m_lightToggleLength;
    private bool m_allLightsOn;
    public Font textFont;
    // Use this for initialization
    void Start()
    {
        m_ai = GameObject.FindGameObjectWithTag("AI");
        m_lightControllerScript = m_ai.GetComponent<LightController>();
        
        m_lightToggleLength = m_lightControllerScript.lights.Length;
        m_lightToggleScripts = new LightToggle[m_lightToggleLength];
        m_lightToggleArray = new Toggle[m_lightToggleLength];
        for (int i = 0; i < m_lightToggleLength; i++)
        {
            GameObject lightToggleClone = Instantiate(lightToggles, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            lightToggleClone.transform.SetParent(gameObject.transform);
            lightToggleClone.GetComponentInChildren<Text>().font = textFont;
            m_lightToggleScripts[i] = lightToggleClone.GetComponent<LightToggle>();
            m_lightToggleArray[i] = lightToggleClone.GetComponentInChildren<Toggle>();
            m_lightToggleScripts[i].lightID = i;
            m_lightToggleScripts[i].ChangeName();
        }
      
    }

    public void NotEnoughPower()
    {
        for (int i = 0; i < m_lightToggleLength; i++)
        {
            if(m_lightToggleArray[i].isOn == false)
            {
                m_lightToggleArray[i].interactable = false;
            }
        }
    }

    public void EnoughPower()
    {
        for (int i = 0; i < m_lightToggleLength; i++)
        {
            if (m_lightToggleArray[i].isOn == false)
            {
                m_lightToggleArray[i].interactable = true;
            }
        }
    }

    public void LightToggled(int lightNo)
    {
        if (m_lightToggleArray[lightNo].interactable == true)
        {
            m_lightToggleArray[lightNo].isOn = !m_lightToggleArray[lightNo].isOn;
        }
    }

    public void AllLights()
    {
        m_allLightsOn = !m_allLightsOn;
        if (m_allLightsOn)
        {
            for (int i = 0; i < m_lightToggleLength; i++)
            {
                if (m_lightToggleArray[i].isOn)
                {
                    m_lightToggleArray[i].isOn = false;
                }
            }
        }
        else
        {
            for (int i = 0; i < m_lightToggleLength; i++)
            {
                if (!m_lightToggleArray[i].isOn && m_lightToggleArray[i].interactable)
                {
                    m_lightToggleArray[i].isOn = true;
                }
            }
        }
    }
}
