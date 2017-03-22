using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DoorLockButton : MonoBehaviour 
{
	public int doorID;
	private GameObject m_ai;
	private DoorController m_doorController;
	private bool m_isOn = true;
	public Sprite[] sprites;
	private Image m_image;
	void Start()
	{
		m_ai = GameObject.FindGameObjectWithTag("AI");
		m_doorController = m_ai.GetComponent<DoorController>();
		m_image = gameObject.GetComponent<Image>();
	}

	public void Locking()
	{
		m_isOn = !m_isOn;
		m_doorController.Locking(doorID);
		if(m_isOn)
		{
			m_image.sprite = sprites[1];
		}
		else
		{
			m_image.sprite = sprites[2];
		}
	}

	private int m_currentStatus = 1; //0 - Open, 1 - On, 2 - Locked

	public void Powered()
	{
		m_currentStatus = ((m_currentStatus + 1) % 3);
        m_image.sprite = sprites[m_currentStatus];
        m_doorController.ChangeDoorState(doorID);
        /*
		if (m_currentStatus == 0) 
		{
			m_image.sprite = sprites[0];
			m_doorController.Powering (doorID);
						//No power
		} 
		else if (m_currentStatus == 1) 
		{
			m_image.sprite = sprites[1];
			m_doorController.Powering (doorID);
			m_doorController.Locking(doorID);
			//power, not locked
		}
		else if (m_currentStatus == 2) 
		{
			m_image.sprite = sprites[2];
			m_doorController.Locking(doorID);
			//power, locked
		}
        */
	}
}
