using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LightButton : MonoBehaviour
{
    public int lightID;
    private GameObject m_ai;
    private LightController m_lightController;
    private bool m_isOn = true;
    public Sprite[] sprites;
    private Image m_image;
    void Start()
    {
        m_ai = GameObject.FindGameObjectWithTag("AI");
        m_lightController = m_ai.GetComponent<LightController>();
        m_image = gameObject.GetComponent<Image>();
    }

    public void Power()
    {
        m_isOn = !m_isOn;
        m_lightController.LightSwitch(lightID);
        if(m_isOn)
        {
            m_image.sprite = sprites[0];
        }
        else
        {
            m_image.sprite = sprites[1];
        }
    }
}
