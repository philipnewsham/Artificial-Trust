using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CameraToggleInstantiate : MonoBehaviour
{
    public GameObject cameraToggles;
    private GameObject m_ai;
    private CameraController m_cameraControllerScript;
    private Toggle[] m_cameraToggleArray;
    private int m_camerasLength;
    private float[] m_cameraTimes;
    private bool[] m_countingDown;
    private float m_shutOutTime = 10f;
    public Font textFont;
    private bool m_allCamerasOn;
    // Use this for initialization
    void Start()
    {
        m_ai = GameObject.FindGameObjectWithTag("AI");
        m_cameraControllerScript = m_ai.GetComponent<CameraController>();
        m_camerasLength = m_cameraControllerScript.cameras.Length;
        m_cameraToggleArray = new Toggle[m_camerasLength];
        m_cameraTimes = new float[m_camerasLength];
        m_countingDown = new bool[m_camerasLength];
        for (int i = 0; i < m_camerasLength; i++)
        {
            GameObject cameraToggleClone = Instantiate(cameraToggles, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            cameraToggleClone.transform.SetParent(gameObject.transform);
            cameraToggleClone.GetComponentInChildren<Text>().font = textFont;
            CameraToggle cameraToggleScript = cameraToggleClone.GetComponent<CameraToggle>();
            m_cameraToggleArray[i] = cameraToggleClone.GetComponentInChildren<Toggle>();
            cameraToggleScript.cameraID = i;
            cameraToggleScript.ChangeName();
            m_cameraTimes[i] = m_shutOutTime;
        }
    }

    void Update()
    {
        if (m_countingDown[0])
        {
            m_cameraTimes[0] -= 1 * Time.deltaTime;
            if(m_cameraTimes[0] <= 0f)
            {
                m_countingDown[0] = false;
                m_cameraTimes[0] = m_shutOutTime;
                ReEnableCamera(0);
            }
        }
        if (m_countingDown[1])
        {
            m_cameraTimes[1] -= 1 * Time.deltaTime;
            if (m_cameraTimes[1] <= 0f)
            {
                m_countingDown[1] = false;
                m_cameraTimes[1] = m_shutOutTime;
                ReEnableCamera(1);
            }
        }
        if (m_countingDown[2])
        {
            m_cameraTimes[2] -= 1 * Time.deltaTime;
            if (m_cameraTimes[2] <= 0f)
            {
                m_countingDown[2] = false;
                m_cameraTimes[2] = m_shutOutTime;
                ReEnableCamera(2);
            }
        }
        if (m_countingDown[3])
        {
            m_cameraTimes[3] -= 1 * Time.deltaTime;
            if (m_cameraTimes[3] <= 0f)
            {
                m_countingDown[3] = false;
                m_cameraTimes[3] = m_shutOutTime;
                ReEnableCamera(3);
            }
        }
    }

    public void NotEnoughPower()
    {
        for (int i = 0; i < m_camerasLength; i++)
        {
            if (m_cameraToggleArray[i].isOn == false)
            {
                m_cameraToggleArray[i].interactable = false;
            }
        }
    }

    public void EnoughPower()
    {
        for (int i = 0; i < m_camerasLength; i++)
        {
            if (m_cameraToggleArray[i].isOn == false)
            {
                m_cameraToggleArray[i].interactable = true;
            }
        }
    }

    public void DisabledCamera(int cameraID)
    {
        m_cameraToggleArray[cameraID].isOn = false;
        m_cameraToggleArray[cameraID].interactable = false;
        //m_cameraControllerScript.CameraSwitch(cameraID);
        m_countingDown[cameraID] = true;
    }

    void ReEnableCamera(int cameraID)
    {
        //m_cameraToggleArray[cameraID].isOn = true;
        m_cameraToggleArray[cameraID].interactable = true;
    }

    public void AllCameras()
    {
        m_allCamerasOn = !m_allCamerasOn;
        if (m_allCamerasOn)
        {
            for (int i = 0; i < m_camerasLength; i++)
            {
                if (m_cameraToggleArray[i].isOn)
                {
                    m_cameraToggleArray[i].isOn = false;
                }
            }
        }
        else
        {
            for (int i = 0; i < m_camerasLength; i++)
            {
                if (!m_cameraToggleArray[i].isOn && m_cameraToggleArray[i].interactable)
                {
                    m_cameraToggleArray[i].isOn = true;
                }
            }
        }
    }
}
