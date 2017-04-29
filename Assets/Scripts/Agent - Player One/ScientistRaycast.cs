using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ScientistRaycast : MonoBehaviour
{
    public float distance;
    private float m_distance
    {
        get { return distance; }
    }

    private Ray m_ray;
    private RaycastHit m_hit;

    public GameController gameController;
    public Text interactText;
    public GameObject aButton;

    private bool m_showingRules;
    public GameObject scientistRules;//and don't you forget it!
    private bool m_hoverLook;

    public Image waitImage;
    public float maxHold;
    private float m_count;

    private string[] m_interactionInfo = new string[12]
        {
        "Unlock the safe to access secret documents!",
        "These filing cabinets have passwords hidden in them, search through to find out",
        "Flip these switches to make different effects happen, press the button on the right to activate", 
        "Hit this button to perform the switches action",
        "Read this to find out what switch positions (in the server room) do what",
        "Warning: Do not press this button unless you are confident in what it does",
        "Unlocks doors. Green: Unlocked, Red: Locked (Takes longer to open), Black: Open",
        "Switches a light on or off",
        "Disables the camera, stopping the AI from seeing from it",
        "Unlock the computer to access information that the AI isn't giving you",
        "Check map to see current location and objectives, alternatively press the start button",
        "Have all of these switches on to open the AI core, allowing you to destroy it if necessary"
        };
    private int[] m_intToInfo = new int[48]
    {
        0,
        1,1,1,
        2,2,2,
        3,
        4,
        5,
        6,6,6,6,6,6,6,6,6,6,
        7,7,7,7,7,7,7,7,7,7,
        8,8,8,8,8,8,8,8,8,8,
        9,
        10,
        -1,-1,-1,
        11,11,11,
    };
    public Text interactionInfoText;
    public GameObject interactionInfoPanel;
    // Use this for initialization
    void Start () {
        interactText.text = "";
        //float distance = 10f;
	}

	public void SelectHoverLook()
	{
		m_hoverLook = !m_hoverLook;
	}

	void StopLooking()
	{
		m_countingDown = false;
		m_count = maxHold;
		m_firstCheck = true;
		m_isLooking = false;
	}

	bool m_firstCheck = true;
	bool m_isLooking;
	bool m_countingDown;
	bool m_doneLooking;

	public GameObject currentObjectiveUI;
    int interactableID;
    bool checkedOnce;
	void Update ()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.TransformDirection(Vector3.forward);
        m_ray = new Ray(origin, direction);
		waitImage.fillAmount = 1 - (m_count / maxHold);

		if (Physics.Raycast (m_ray, out m_hit, distance)) 
		{
			if (m_hit.collider.tag == "Interactable" /*&& m_hit.distance <= distance*/) {
				if (m_hoverLook) 
				{
					if (m_firstCheck) 
					{
						m_firstCheck = false;
						m_countingDown = true;
					}
				}
				else 
				{
                    if (!checkedOnce)
                    {
                        interactableID = m_hit.collider.gameObject.GetComponent<InteractableObject>().interactableID;
                        interactionInfoText.text = m_interactionInfo[m_intToInfo[interactableID]];
                        interactionInfoPanel.SetActive(true);
                        checkedOnce = true;
                    }
					m_isLooking = true;
					m_count = 0;
					interactText.text = "Interact!";
					aButton.SetActive (true);
					if (Input.GetButtonDown ("ControllerA") || Input.GetKeyDown (KeyCode.E))
                    {
						gameController.InteractedWith (/*m_hit.collider.gameObject.GetComponent<InteractableObject> ().*/interactableID);
					}
				}
			} 
			else 
			{
				StopLooking ();
                checkedOnce = false;
                interactionInfoPanel.SetActive(false);
				interactText.text = "";
				aButton.SetActive (false);
			}
		} else {
			StopLooking ();
		}
        if (Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            CheckMap();
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectHoverLook();
        }

		if (m_countingDown) 
		{
			m_count -= Time.deltaTime;
			if (m_count <= 0) 
			{
				m_doneLooking = true;
				m_countingDown = false;
				m_count = maxHold;
				gameController.InteractedWith (m_hit.collider.gameObject.GetComponent<InteractableObject> ().interactableID);
			}
		}
	}
	public FreezeControls freezeControlScript;
    public void CheckMap()
    {
        m_showingRules = !m_showingRules;
        scientistRules.SetActive(m_showingRules);
		currentObjectiveUI.SetActive (!m_showingRules);
		freezeControlScript.FirstPersonControllerEnabled(!m_showingRules);
    }
	public void Interacted()
	{
		if (m_isLooking) 
		{
			gameController.InteractedWith (m_hit.collider.gameObject.GetComponent<InteractableObject> ().interactableID);
		}
	}
}
