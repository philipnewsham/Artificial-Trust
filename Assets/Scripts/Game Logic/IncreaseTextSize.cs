using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class IncreaseTextSize : MonoBehaviour {
    private Text m_thisText;
	// Use this for initialization
	void Start () {
        m_thisText = gameObject.GetComponent<Text>();
        m_thisText.fontSize += 10;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
