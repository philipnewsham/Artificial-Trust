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
        m_newLine += string.Format("\n{0} {1} {2}",objectType,objectNo.ToString(),m_stateString[objectState]);
        m_taskLogText.text = m_newLine; 
    }
}
