using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraController : MonoBehaviour 
{
    public GameObject[] cameraGameObjects;
    public Camera[] cameras;
    private bool[] m_camerasOn;
    private AIPower m_aiPowerScript;
    private int m_cameraPower;
    private bool m_showingCamerasA = false;
    private bool m_showingCamerasB = false;
    public Material[] materials;
    public GameObject[] cameraLights;
    private Renderer[] m_cameraLightRenderers;
    private int m_cameraLightLength;

    public Button[] cameraButtons;
    public TaskLog taskLogScript;
	// Use this for initialization
	void Start () 
	{
        m_aiPowerScript = gameObject.GetComponent<AIPower>();
        m_cameraPower = m_aiPowerScript.cameraPower;
        m_cameraLightLength = cameras.Length;
        m_camerasOn = new bool[m_cameraLightLength];
        
        m_cameraLightRenderers = new Renderer[m_cameraLightLength];
        for (int i = 0; i < m_cameraLightLength; i++)
        {
            m_cameraLightRenderers[i]  = cameraLights[i].GetComponent<Renderer>();
        }
        for (int i = 0; i < m_cameraLightLength; i++)
        {
            m_camerasOn[i] = true;
        }
	}
    public void CurrentCameraPower(int newPower)
    {
        m_cameraPower += newPower;
    }

    public Text[] cameraText;
    public GameObject[] cameraRecImage;

    public void CameraSwitch(int camNo)
    {
        //print("Message Recieved " + camNo);
        if (m_camerasOn[camNo] == true)
        {
            cameras[camNo].enabled = false;
            m_cameraLightRenderers[camNo].material = materials[0];
            m_camerasOn[camNo] = !m_camerasOn[camNo];
            m_aiPowerScript.PowerExchange(m_cameraPower);
            taskLogScript.UpdateText("Camera", camNo, 1);
            cameraText[camNo].text = "Offline";
            cameraRecImage[camNo].SetActive(false);
        }
        else
        {
            if (m_aiPowerScript.CheckPower(m_cameraPower) == true)
            {
                m_cameraLightRenderers[camNo].material = materials[1];
                cameras[camNo].enabled = true;
                m_camerasOn[camNo] = !m_camerasOn[camNo];
                m_aiPowerScript.PowerExchange(-m_cameraPower);
                taskLogScript.UpdateText("Camera", camNo, 0);
                cameraText[camNo].text = "Recording";
                cameraRecImage[camNo].SetActive(true);
            }
            else
            {
                //no power
            }
        }
    }

    public void WaitAndShowCamerasA()
    {
            ShowCamerasA();
            HideCamerasB();
    }

    public void WaitAndShowCamerasB()
    {
            ShowCamerasB();
            HideCamerasA();
    }
    public void LoadCameras()
    {
        Invoke("ShowCamerasA", 1f);
    }
    public void WaitAndHideCameras()
    {
        Invoke("HideAllCameras", 1f);
    }

    void ShowCamerasA()
    {
        for (int i = 0; i < m_cameraLightLength - 4; i++)
        {
            cameraGameObjects[i].SetActive(true);
        }
    }

    void HideCamerasB()
    {
        for (int i = m_cameraLightLength - 4; i < m_cameraLightLength; i++)
        {
            cameraGameObjects[i].SetActive(false);
        }
    }



    void ShowCamerasB()
    {
        print("yeah");
        for (int i = m_cameraLightLength - 4; i < m_cameraLightLength; i++)
        {
            cameraGameObjects[i].SetActive(true);
        }
    }

    void HideCamerasA()
    {
        print("Boy");
        for (int i = 0; i < m_cameraLightLength - 4; i++)
        {
            cameraGameObjects[i].SetActive(false);
        }
    }

    void HideAllCameras()
    {
        for (int i = 0; i < m_cameraLightLength; i++)
        {
            cameraGameObjects[i].SetActive(false);
        }
    }

    public void NoPower()
    {
        for (int i = 0; i < cameraButtons.Length; i++)
        {
            if (!m_camerasOn[i])
            {
               cameraButtons[i].interactable = false;
            }
        }
    }

    public void EnoughPower()
    {
        for (int i = 0; i < cameraButtons.Length; i++)
        {
            if (!cameraButtons[i].interactable)
            {
                cameraButtons[i].interactable = true;
            }
        }
    }
}
