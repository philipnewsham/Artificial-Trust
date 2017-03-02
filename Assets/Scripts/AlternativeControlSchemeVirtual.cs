using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
public class AlternativeControlSchemeVirtual : MonoBehaviour
{
    private FirstPersonController m_firstPersonControllerScript;
    public int mouseSpeed;
    private bool m_isSelected;
    private MouseLook m_mouseLook;

    void Start()
    {
        m_firstPersonControllerScript = GetComponent<FirstPersonController>();
        m_mouseLook = m_firstPersonControllerScript.m_MouseLook;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Selected();
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_isSelected = true;
            Selected();
        }
    }

    public void Looking(float xSpeed)
    {
        m_mouseLook.ScreenButtonRotation(xSpeed);
    }
    public GameObject virtualJoystick;
    public void Selected()
    {
        m_isSelected = !m_isSelected;
        if (m_isSelected)
        {
            virtualJoystick.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            //changing options to move with on screen buttons
            m_mouseLook.XSensitivity = mouseSpeed;
            m_mouseLook.MinimumX = 0f;
            m_mouseLook.MaximumX = 0f;
            m_mouseLook.lockCursor = false;
            m_firstPersonControllerScript.alternativeControlScreen = true;
            m_mouseLook.alternateControlSchemeScreen = true;
        }
        else
        {
            virtualJoystick.SetActive(false);
            m_mouseLook.XSensitivity = 2;
            m_mouseLook.MinimumX = -90f;
            m_mouseLook.MaximumX = 90f;
            m_mouseLook.lockCursor = true;
            m_firstPersonControllerScript.alternativeControlScreen = false;
            m_mouseLook.alternateControlSchemeScreen = false;
            /*things to change:
            mouseSensitivity
            
            lock looking updown
            mouseclick to move
            */

        }
    }
}
