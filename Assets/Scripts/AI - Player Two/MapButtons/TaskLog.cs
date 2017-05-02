using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TaskLog : MonoBehaviour
{
    private Text m_taskLogText;
    private string m_newLine;
    private string[] m_stateString = new string[5] { "is switched On", "is switched Off", "is open", "is unlocked", "is locked"};
    void Start()
    {
        m_taskLogText = GetComponent<Text>();
        m_newLine = "...Current Task Log";
    }

    public void UpdateText(string objectType, int objectNo, int objectState)
    {
        if(objectType == "Light")
        {
            m_newLine += string.Format("\n<color=#ffff00ff>{0} {1} {2}</color>", objectType, objectNo.ToString(), m_stateString[objectState]);
        }
        if(objectType == "Camera")
        {
            m_newLine += string.Format("\n<color=blue>{0} {1} {2}</color>", objectType, objectNo.ToString(), m_stateString[objectState]);
        }
        if(objectType == "Door")
        {
            m_newLine += string.Format("\n<color=red>{0} {1} {2}</color>", objectType, objectNo.ToString(), m_stateString[objectState]);
        }
        //m_newLine += string.Format("\n{0} {1} {2}", objectType, objectNo.ToString(), m_stateString[objectState]);
        m_taskLogText.text = m_newLine; 
    }

}
