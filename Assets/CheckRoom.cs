using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CheckRoom : MonoBehaviour
{
    private string[] m_roomName = new string[7] { "Main Laboratory", "Small Office", "Server Room", "AI HUB", "Archives", "Dr. Kirkoff's Office","Corridor" };
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
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other)
    {
	    if(other.gameObject.tag == "Room")
        {
            int roomNo = other.gameObject.GetComponent<CurrentRoom>().currentRoom;
            roomNameText.text = m_roomName[roomNo];

            if(m_currentObjectiveInt == 0 && roomNo == 0)
            {
                UpdateObjectiveText();
            }

            if(m_currentObjectiveInt == 2 && roomNo == 1)
            {
                UpdateObjectiveText();
            }
        }
	}

    public void UpdateObjectiveText()
    {
        m_currentObjectiveInt += 1;
        objectiveText.text = string.Format("Current Objective: {0}", m_objectives[m_currentObjectiveInt]);
    }
}
