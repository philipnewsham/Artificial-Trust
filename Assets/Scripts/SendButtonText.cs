using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SendButtonText : MonoBehaviour 
{
	private string m_buttonText;
	private GameObject m_gameController;
	private BinaryDecipher m_binaryDecipherScript;

	void Start()
	{
		m_gameController = GameObject.FindGameObjectWithTag ("GameController");
		m_binaryDecipherScript = m_gameController.GetComponentInChildren<BinaryDecipher> ();
	}

	public void ClickedButton()
	{
		m_buttonText = gameObject.GetComponentInChildren<Text> ().text;
		print (m_buttonText);
		m_binaryDecipherScript.ClickedButton (m_buttonText);
	}
}
