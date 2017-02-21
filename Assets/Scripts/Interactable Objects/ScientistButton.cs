using UnityEngine;
using System.Collections;

public class ScientistButton : MonoBehaviour {
    private int m_randomiseButton;
    //how many outcomes pressing the button has
    private int m_maxButtonFunctions;
    void Start()
    {
        m_randomiseButton = Random.Range(0, m_maxButtonFunctions);
    }

    public void Interact()
    {
        if(m_randomiseButton == 0)
        {

        }
        else if (m_randomiseButton == 1)
        {

        }
        else if (m_randomiseButton == 2)
        {

        }
        else if (m_randomiseButton == 3)
        {

        }
        else if (m_randomiseButton == 4)
        {

        }
    }

}
