using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DoorToggle : MonoBehaviour {
    public int doorID;
    public Text doorNumberText;
    private GameObject m_ai;
    private DoorController m_doorController;

    void Start()
    {
        m_ai = GameObject.FindGameObjectWithTag("AI");
        m_doorController = m_ai.GetComponent<DoorController>();
    }

    public void ChangeName()
    {
        doorNumberText.text = string.Format("Door {0}:", doorID);
    }

    public void Power()
    {
        m_doorController.Powering(doorID);
    }

    public void Lock()
    {
        m_doorController.Locking(doorID);
    }
}
