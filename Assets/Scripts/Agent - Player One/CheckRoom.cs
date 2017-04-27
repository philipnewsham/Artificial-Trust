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

	public Button[] mapLocations;
	private Sprite[] m_locationSprites;
	public Sprite standingMan;
	public Sprite wayPoint;

	int mapButtonLength;

	private AgentObjectiveText m_agentObjectiveTextScript;
    private string[] m_objectives = new string[6]
    {
        "Follow the waypoints to the Main Laboratory",
        "Work with the AI to solve the Binary Puzzle",
        "Follow the waypoints to the Small Office",
        "Work with the AI to solve the Pattern Puzzle",
        "Follow the waypoints to the Server Room",
        "Work with the AI to solve the Geometry Puzzle"
    };
	// Use this for initialization
	void Start ()
    {
		m_roomNo = 6;
		m_agentObjectiveTextScript = GetComponent<AgentObjectiveText> ();
		roomNameText.text = m_roomName[m_roomNo];
        objectiveText.text = string.Format("Current Objective: {0}", m_objectives[m_currentObjectiveInt]);
		mapButtonLength = mapLocations.Length;
		m_locationSprites = new Sprite[mapButtonLength];
		for (int i = 0; i < mapButtonLength; i++) 
		{
			m_locationSprites [i] = mapLocations [i].GetComponent<Image> ().sprite;
		}
		UpdateMapLocation ();
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
    Unlock the safe
        1. Go to dr. Kirkoff's office
        2. Find out the password
            2a. Find out the password by asking the AI
            2b. Find out the password in the small office
        3. Find out the shape sequence
            3a. Find out the sequence by asking the AI
            3b. Find out the sequence in the small office
        4. Find out the starsign
            4a. Find out the starsign by asking the AI
            4b. Find out the starsign in the small office
        5. Unlock the Safe
        6. Go to the elevator
    */

    bool m_introObjectives = true;
    int m_roomNo = 1;
    bool m_checkingWait;
    bool m_checkingWaitAI;
	// Update is called once per frame
	void OnTriggerEnter (Collider other)    
    {
	    if(other.gameObject.tag == "Room")
        {
            m_roomNo = other.gameObject.GetComponent<CurrentRoom>().currentRoom;
            roomNameText.text = m_roomName[m_roomNo];

			UpdateMapLocation ();

            if (m_roomNo == m_waitRoom)
                m_checkingWait = true;
            else
                m_checkingWait = false;

            if (m_roomNo == m_waitRoomAI)
                m_checkingWaitAI = true;
            else
                m_checkingWaitAI = false;

            if(m_currentObjectiveInt == 0 && m_roomNo == 0)
            {
                UpdateObjectiveText();
            }

            if(m_currentObjectiveInt == 2 && m_roomNo == 1)
            {
                UpdateObjectiveText();
            }

            if(m_currentObjectiveInt == 4 && m_roomNo == 2)
            {
                UpdateObjectiveText();
            }
            
            /*
            if (m_currentObjectiveInt == (m_roomNo / 2) && m_introObjectives)
            {
                UpdateObjectiveText();
            }
            */
        }
	}

	void UpdateMapLocation()
	{
		for (int i = 0; i < mapButtonLength; i++) 
		{
			if (i == m_roomNo) 
			{
				mapLocations [i].GetComponent<Image> ().sprite = standingMan;
			} 
			else 
			{
				mapLocations [i].GetComponent<Image> ().sprite = m_locationSprites [i];
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

    private int m_waitRoomAI;
    private float m_waitTimeAI;
    public void WaitObjectiveAI(int roomNo, float time)
    {
        m_waitRoomAI = roomNo;
        m_waitTimeAI = time;
    }

    bool m_objectiveComplete;
    bool m_objectiveCompleteAI;
    public AIObjectives aiObjectiveScript;
    void Update()
    {
        m_roomTime[m_roomNo] += Time.deltaTime;
        if(m_checkingWait && m_roomTime[m_roomNo] >= m_waitTime && !m_objectiveComplete)
        {
            m_objectiveComplete = true;
			GetComponent<AgentObjectives>().WaitInRoomObjective(0,true);
        }

        if(m_checkingWaitAI && m_roomTime[m_roomNo] >= m_waitTimeAI && !m_objectiveCompleteAI)
        {
            m_objectiveCompleteAI = true;
            aiObjectiveScript.WaitInRoomObjective(0, true);
        }
    }

    public void UpdateObjectiveText()
    {
		m_agentObjectiveTextScript.CompletedTask ();
        m_currentObjectiveInt += 1;
        
        if(m_currentObjectiveInt >= m_objectives.Length)
        {
            m_introObjectives = false;
            objectiveText.text = "";
        }
        else
        {
            objectiveText.text = string.Format("Current Objective: {0}", m_objectives[m_currentObjectiveInt]);
        }
    }
}
