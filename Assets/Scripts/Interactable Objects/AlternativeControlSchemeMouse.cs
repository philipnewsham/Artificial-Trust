using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
public class AlternativeControlSchemeMouse : MonoBehaviour
{
    private FirstPersonController m_firstPersonControllerScript;
    public int mouseSpeed;
    private bool m_isSelected;
    private MouseLook m_mouseLook;

    void Start ()
    {
        m_firstPersonControllerScript = GetComponent<FirstPersonController>();
        m_mouseLook = m_firstPersonControllerScript.m_MouseLook;
	}

    public Slider mouseSensitivitySlider;
    public void ChangeMouseSensitivity()
    {
        mouseSpeed = Mathf.FloorToInt(mouseSensitivitySlider.value);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Selected();
        }
    }

    public void Selected()
    {
        m_isSelected = !m_isSelected;
        if (m_isSelected)
        {
            //changing options to move with only mouse
            m_mouseLook.XSensitivity = mouseSpeed;
            m_mouseLook.MinimumX = 0f;
            m_mouseLook.MaximumX = 0f;
            m_firstPersonControllerScript.alternativeControlMouse = true;
            m_mouseLook.alternateControlSchemeMouse = true;
        }
        else
        {
            m_mouseLook.XSensitivity = 2;
            m_mouseLook.MinimumX = -90f;
			m_mouseLook.alternateControlSchemeMouse = false;
            m_mouseLook.MaximumX = 90f;
            m_firstPersonControllerScript.alternativeControlMouse = false;
            /*things to change:
            mouseSensitivity
            
            lock looking updown
            mouseclick to move
            */

        }
    }

}
