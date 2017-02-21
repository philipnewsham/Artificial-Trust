using UnityEngine;
using System.Collections;

public class DoorControls : MonoBehaviour {
    public GameObject door;
    private GameObject m_door { get { return door; } }
    private bool m_powerOn;
    private bool m_locked;

    public int unlockingTime;
    private int m_unlockingTime { get { return unlockingTime; } }

    public int openingTime;
    private int m_openingTime { get { return openingTime; } }

    void Start()
    {
        m_powerOn = true;
        m_locked = false;
        Interactable();
    }

    public void Interactable()
    {
        if (m_powerOn)
        {
            if (m_locked)
            {
                Invoke("OpenDoor", m_unlockingTime);
            }
            else
            {
                Invoke("OpenDoor", m_openingTime);
            }
        }
    }

    public void DoorPower(string toggleDoor)
    {
        if(toggleDoor == "Lock")
        {
            m_locked = true;
        }
        if(toggleDoor == "Unlock")
        {
            m_locked = false;
        }
        if(toggleDoor == "Off")
        {
            m_powerOn = false;
        }
        if(toggleDoor == "On")
        {
            m_powerOn = true;
        }
    }

    void OpenDoor()
    {
        m_door.GetComponent<Transform>().position =new Vector3(m_door.GetComponent<Transform>().position.x, m_door.GetComponent<Transform>().position.y + 5f, m_door.GetComponent<Transform>().position.z);
    }
}
