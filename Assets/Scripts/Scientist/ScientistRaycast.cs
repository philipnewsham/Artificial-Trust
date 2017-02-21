using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ScientistRaycast : MonoBehaviour {
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
    // Use this for initialization
    void Start () {
        interactText.text = "";
        //float distance = 10f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.TransformDirection(Vector3.forward);
        m_ray = new Ray(origin, direction);
        if (Physics.Raycast(m_ray, out m_hit))
        {
            if(m_hit.collider.tag == "Interactable" && m_hit.distance <= distance)
            {
                //print("hit");
                interactText.text = "Interact!";
                aButton.SetActive(true);
                if (Input.GetButtonDown("ControllerA")/*||Input.GetKeyDown(KeyCode.E)*/)
                {
                    print("clicked on interactable object");
                    gameController.InteractedWith(m_hit.collider.gameObject.GetComponent<InteractableObject>().interactableID);
                }
            }
            else
            {
                interactText.text = "";
                aButton.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            m_showingRules = !m_showingRules;
            scientistRules.SetActive(m_showingRules);
        }
	}
}
