using UnityEngine;
using System.Collections;

public class DisableSavedCanvas : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("ControllerA"))
        {
            gameObject.SetActive(false);
        }
	}
}
