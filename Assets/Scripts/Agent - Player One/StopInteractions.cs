using UnityEngine;
using System.Collections;

public class StopInteractions : MonoBehaviour {
    public GameObject[] interactableObjects;
	// Use this for initialization
	void Start () {
        interactableObjects = GameObject.FindGameObjectsWithTag("Interactable");
        for (int i = 0; i < interactableObjects.Length; i++)
        {
            interactableObjects[i].tag = "Untagged";
        }
	}
}
