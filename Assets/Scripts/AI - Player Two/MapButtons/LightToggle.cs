using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LightToggle : MonoBehaviour {
    public int lightID;
    public Text lightNumberText;
    private GameObject m_ai;
    private LightController m_lightController;

    void Start()
    {
        m_ai = GameObject.FindGameObjectWithTag("AI");
        m_lightController = m_ai.GetComponent<LightController>();
    }

    public void ChangeName()
    {
        lightNumberText.text = string.Format("Light {0}:", lightID);
    }

    public void Power()
    {
        //print("clockwerk");
        m_lightController.LightSwitch(lightID);

    }
}
