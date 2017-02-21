using UnityEngine;
using System.Collections;

public class SingleSwitch : MonoBehaviour {
    public int switchID;
    private int m_switchID { get { return switchID; } }
    public ThreeSwitches threeSwitches;
    private bool m_switchedOn = false;
    public GameObject switchPivot;
    private Animator m_anim;
    private AudioSource m_audioSource;
    void Start()
    {
        m_anim = switchPivot.GetComponent<Animator>();
        m_audioSource = gameObject.GetComponent<AudioSource>();
    }
	void Update()
    {
        if (m_switchID == 1)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Interact();
            }
        }
        if (m_switchID == 2)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Interact();
            }
        }
        if (m_switchID == 3)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Interact();
            }
        }

    }
    public void Interact()
    {
        m_switchedOn = !m_switchedOn;
        if(m_switchID == 1)
        {
            threeSwitches.SwitchOne();
        }
        else if(m_switchID == 2)
        {
            threeSwitches.SwitchTwo();
        }
        else if(m_switchID == 3)
        {
            threeSwitches.SwitchThree();
        }
        if (m_switchedOn)
        {
            //transform.eulerAngles = new Vector3(0, 0, 90f);
            m_anim.SetTrigger("On");
        }
        else
        {
            m_anim.SetTrigger("Off");
        }
        m_audioSource.Play();
    }
}
