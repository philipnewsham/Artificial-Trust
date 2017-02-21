using UnityEngine;
using System.Collections.Generic;

public class Passwords : MonoBehaviour {
    private Dictionary<int,string> m_passwords =  new Dictionary<int,string>();
	// Use this for initialization
	void Start () {
	
	}
	
    public void AddPassword(int passwordID, string password)
    {
        m_passwords.Add(passwordID, password);
    }
}
