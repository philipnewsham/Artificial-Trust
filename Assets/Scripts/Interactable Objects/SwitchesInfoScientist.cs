using UnityEngine;
using System.Collections;

public class SwitchesInfoScientist : MonoBehaviour
{
    public GameObject switchInfoCanvas;
    private bool m_isShown = false;

    private GameObject m_gameController;
    private FreezeControls m_freezeControlScript;

    private AudioSource m_audioSource;
    void Start()
    {
        m_gameController = GameObject.FindGameObjectWithTag("GameController");
        m_freezeControlScript = m_gameController.GetComponent<FreezeControls>();
        m_audioSource = gameObject.GetComponent<AudioSource>();
    }
    public void Interact()
    {
        m_isShown = !m_isShown;
        switchInfoCanvas.SetActive(m_isShown);
        m_freezeControlScript.FirstPersonControllerEnabled(!m_isShown);
        m_audioSource.Play();
    }
}
