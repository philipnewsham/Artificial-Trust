using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class AIMenuNavigation : MonoBehaviour
{
    public GameObject[] panels;
    public Button[] panelButtons;
    
    void Start()
    {
        ChangePanel(0);
        HoverText(6);
    }

    public void ChangePanel(int panelNo)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (i == panelNo)
            {
                panels[i].SetActive(true);
                panelButtons[i].interactable = false;
            }
            else
            {
                panels[i].SetActive(false);
                panelButtons[i].interactable = true;
            }
        }
    }

    public Text hoverText;
    private string[] m_hoverTextString = new string[7]
    {
        "Open map to control the power",
        "Open document page to gather information",
        "Open robotic locks to gain access to robot body",
        "Open to find out your current objective",
        "Open cameras to find out where the agent is",
        "Error. Unknown what will happen when button is pressed",
        ""
    };

    public void HoverText(int panelNo)
    {
        hoverText.text = m_hoverTextString[panelNo];
    }
}
