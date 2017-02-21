using UnityEngine;
using System.Collections;

public class DisableCamera : MonoBehaviour {
    public int cameraID;
    private Camera m_camera;
    private GameObject m_gameController;
    private FreezeControls m_freezeControlScript;
    private GameObject m_ai;
    private CameraController m_cameraControllerScript;
    private bool m_switchedOff = false;
    public CameraToggleInstantiate m_cameraToggleInstantiateScript;
    private bool m_countingDown;
    private float m_cameraTime;
    private float m_shutOutTime = 10f;
    public AudioClip[] audioClips;
    private AudioSource m_audioSource;
    void Start()
    {
        m_audioSource = gameObject.GetComponent<AudioSource>();
        m_camera = gameObject.GetComponentInChildren<Camera>();
        m_gameController = GameObject.FindGameObjectWithTag("GameController");
        m_ai = GameObject.FindGameObjectWithTag("AI");
        m_cameraControllerScript = m_ai.GetComponent<CameraController>();
        m_freezeControlScript = m_gameController.GetComponent<FreezeControls>();
        m_cameraTime = m_shutOutTime;

        //gameObject.SetActive(false);
    }

    void Update()
    {
        if (m_countingDown)
        {
            m_cameraTime -= 1 * Time.deltaTime;
            if (m_cameraTime <= 0f)
            {
                m_countingDown = false;
                m_cameraTime = m_shutOutTime;
                m_switchedOff = false;
            }
        }
    }
    public void Interact()
    {
        if (!m_switchedOff)
        {
            print("disable camera interact");
            m_audioSource.clip = audioClips[0];
            m_audioSource.loop = true;
            m_audioSource.Play();
            Invoke("ShortCircuitSound", 5f);
            Invoke("SwitchOff", 5f);
            m_switchedOff = true;
            m_freezeControlScript.FirstPersonControllerEnabled(false);
        }
    }

    void ShortCircuitSound()
    {
        m_audioSource.clip = audioClips[1];
        m_audioSource.loop = false;
        m_audioSource.Play();
    }

    void SwitchOff()
    {
        print("switch off");
        //m_cameraControllerScript.CameraSwitch(cameraID);
        m_countingDown = true;
        m_freezeControlScript.FirstPersonControllerEnabled(true);
        m_cameraToggleInstantiateScript.DisabledCamera(cameraID);
    }
}
