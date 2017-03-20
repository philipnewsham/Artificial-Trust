using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CheckRoom : MonoBehaviour
{
    private string[] m_roomName = new string[9] { "Main Laboratory", "Small Office", "Server Room", "AI HUB", "Archives", "Dr. Kirkoff's Office","Corridor One", "Corridor Two", "Corridor Three" };
    private float[] m_roomTime = new float[9];
    public Text roomNameText;
    public Text objectiveText;
    private int m_currentObjectiveInt;
    private string[] m_objectives = new string[6]
    {
        "Enter Main Laboratory",
        "Solve Binary Puzzle",
        "Enter Small Office",
        "Solve Pattern Puzzle",
        "Enter Server Room",
        "Solve Geometry Puzzle"
    };
	// Use this for initialization
	void Start ()
    {
        roomNameText.text = m_roomName[6];
        objectiveText.text = string.Format("Current Objective: {0}", m_objectives[m_currentObjectiveInt]);
    }
    /*
    intro:
    mission one: go to main lab - ontriggerenter
    mission two: help AI - binarypuzzlecomplete
    mission three: go to office - ontriggerenter
    mission four: help AI - patternpuzzlecomplete
    mission five: go to server - ontriggerenter
    mission six: help AI - geometrypuzzlecomplete

    main:

    */
    bool m_introObjectives = true;
    int m_roomNo = 1;
    bool m_checkingWait;
	// Update is called once per frame
	void OnTriggerEnter (Collider other)    
    {
	    if(other.gameObject.tag == "Room")
        {
            m_roomNo = other.gameObject.GetComponent<CurrentRoom>().currentRoom;
            roomNameText.text = m_roomName[m_roomNo];

            if (m_roomNo == m_waitRoom)
                m_checkingWait = true;
            else
                m_checkingWait = false;

            /*
            if(m_currentObjectiveInt == 0 && roomNo == 0)
            {
                UpdateObjectiveText();
            }

            if(m_currentObjectiveInt == 2 && roomNo == 1)
            {
                UpdateObjectiveText();
            }

            if(m_currentObjectiveInt == 4 && roomNo == 2)
            {
                UpdateObjectiveText();
            }
            */

            if(m_currentObjectiveInt == (m_roomNo/2) && m_introObjectives)
            {
                UpdateObjectiveText();
            }
        }
	}
    private int m_waitRoom;
    private float m_waitTime;
    public void WaitObjective(int roomNo, float time)
    {
        m_waitRoom = roomNo;
        m_waitTime = time;
    }
    bool m_objectiveComplete;
    void Update()
    {
        m_roomTime[m_roomNo] += Time.deltaTime;
        if(m_checkingWait && m_roomTime[m_roomNo] >= m_waitTime && !m_objectiveComplete)
        {
            m_objectiveComplete = true;
            GetComponent<ScientistObjectives>().CompletedWaitObjective();
        }
    }

    public void UpdateObjectiveText()
    {
        m_currentObjectiveInt += 1;
        objectiveText.text = string.Format("Current Objective: {0}", m_objectives[m_currentObjectiveInt]);
        if(m_currentObjectiveInt >= 6)
        {
            m_introObjectives = false;
        }
    }
}
