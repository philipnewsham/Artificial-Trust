using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class SwitchActions : MonoBehaviour {
    public string[] switchesActions;
    private List<string> m_switchActions = new List<string>();
	// Use this for initialization
	void Start () {
        for (int i = 0; i < switchesActions.Length; i++)
        {
            m_switchActions.Add(switchesActions[i]);
        }
        //m_switchActions.
	}
	

}
