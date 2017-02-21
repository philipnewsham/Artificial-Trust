using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class AIMenuSystem : MonoBehaviour {
    public GameObject[] canvases;
    private Animator[] m_animators;
    private int m_currentCanvas;

    public Text infoText;
    public string info;
    void Start()
    {
        m_animators = new Animator[canvases.Length];
        for (int i = 0; i < m_animators.Length; i++)
        {
            m_animators[i] = canvases[i].GetComponent<Animator>();
        }
        infoText.text = info;
    }

    public void ChangeMenu(int currentMenu)
    {
        m_animators[m_currentCanvas].SetTrigger("Exit");
        m_animators[currentMenu].SetTrigger("Enter");
        m_currentCanvas = currentMenu;
        if(currentMenu == 0)
        {
            infoText.text = info;
        }
    }
}
