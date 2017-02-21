using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour {
    private bool m_scientistReady = false;
    private bool m_aiReady = false;

    public Text scientistReadyText;
    public Text aiReadyText;
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("ControllerA"))
        {
            ScientistReady();
        }
	}

    void ScientistReady()
    {
        m_scientistReady = !m_scientistReady;
        if (m_scientistReady)
        {
            scientistReadyText.text = "Ready!";
            CheckReadyUp();
        }
        else
        {
            scientistReadyText.text = "Not Ready!";
        }
    }

    public void AIReady()
    {
        m_aiReady = !m_aiReady;
        if (m_aiReady)
        {
            aiReadyText.text = "Ready!";
            CheckReadyUp();
        }
        else
        {
            aiReadyText.text = "Not Ready!";
        }
    }

    void CheckReadyUp()
    {
        if(m_aiReady && m_scientistReady)
        {
            SceneManager.LoadScene(1);
        }
    }
}
