using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CameraToggle : MonoBehaviour {
    public int cameraID;
    public Text cameraNumberText;
    private GameObject m_ai;
    private CameraController m_cameraController;

    void Start()
    {
        m_ai = GameObject.FindGameObjectWithTag("AI");
        m_cameraController = m_ai.GetComponent<CameraController>();
    }

    public void ChangeName()
    {
        cameraNumberText.text = string.Format("Camera {0}:", cameraID);
    }

    public void Power()
    {
        m_cameraController.CameraSwitch(cameraID);
    }
}
