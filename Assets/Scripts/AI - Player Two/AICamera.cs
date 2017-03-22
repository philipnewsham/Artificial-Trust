using UnityEngine;
using System.Collections;

public class AICamera : MonoBehaviour {
    private bool m_cameraDrag;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            m_cameraDrag = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            m_cameraDrag = false;
        }

        if (m_cameraDrag)
        {

        }
	}
}
