using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CameraButton : MonoBehaviour 
{
	public int cameraID;
	private GameObject m_ai;
	private CameraController m_cameraController;
	private bool m_isOn = true;
	public Sprite[] sprites;
	private Image m_image;

	void Start()
	{
		m_ai = GameObject.FindGameObjectWithTag("AI");
		m_cameraController = m_ai.GetComponent<CameraController>();
		m_image = gameObject.GetComponent<Image>();
	}

	public void Power()
	{
		m_isOn = !m_isOn;
		m_cameraController.CameraSwitch(cameraID);

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
