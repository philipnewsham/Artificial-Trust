using UnityEngine;
using System.Collections;

public class StopInteractions : MonoBehaviour {
    public GameObject[] interactableObjects;
    private Material[] specificMaterials;
    public Material blankMaterial;
	// Use this for initialization
	void Start () {
        interactableObjects = GameObject.FindGameObjectsWithTag("Interactable");
        specificMaterials = new Material[interactableObjects.Length];
        for (int i = 0; i < interactableObjects.Length; i++)
        {
            interactableObjects[i].tag = "Untagged";
            specificMaterials[i] = interactableObjects[i].GetComponentInChildren<Renderer>().material;
            interactableObjects[i].GetComponent<Renderer>().material = blankMaterial;
        }
	}

    public void AllowInteractions()
    {
        for (int i = 0; i < interactableObjects.Length; i++)
        {
            interactableObjects[i].tag = "Interactable";
            interactableObjects[i].GetComponentInChildren<Renderer>().material = specificMaterials[i];
        }
    }
}
