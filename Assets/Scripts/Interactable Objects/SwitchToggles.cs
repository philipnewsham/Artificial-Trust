using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SwitchToggles : MonoBehaviour
{
    private GameObject m_gameController;
    private GameController m_gameControllerScript;
    private SingleSwitch[] m_singleSwitchScripts = new SingleSwitch[3];
    private bool[] m_switchOn = new bool[3] { false, false, false };
    private bool[] m_toggledSwitch = new bool[3] { false, false, false };
    public Toggle[] switchToggles;

    void Start()
    {
        m_gameController = GameObject.FindGameObjectWithTag("GameController");
        m_gameControllerScript = m_gameController.GetComponent<GameController>();
        m_singleSwitchScripts = m_gameControllerScript.switchScripts;
    }

    public void SwitchFlipped(int switchNo, bool isNowOn)
    {
        switchToggles[switchNo].isOn = isNowOn;
        m_switchOn[switchNo] = isNowOn;
       // print("Done");
    }
}
