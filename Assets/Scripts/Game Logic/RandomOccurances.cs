using UnityEngine;
using System.Collections;

public class RandomOccurances : MonoBehaviour {
    private int m_randomTime;
    private int m_randomTimeTwo;
    private float m_countingUp;

    private bool m_thingOne;
    private bool m_thingTwo;
	// Use this for initialization
	void Start ()
    {
        m_randomTime = Random.Range(60, 300);
        m_randomTimeTwo = Random.Range(300, 600);
	}
	
	// Update is called once per frame
	void Update () {
        m_countingUp += Time.deltaTime;
        if(m_countingUp >= m_randomTime && !m_thingOne)
        {
            m_thingOne = true;
            ThingTwo();
        }
        if(m_countingUp >= m_randomTimeTwo && !m_thingTwo)
        {
            m_thingTwo = true;
            ThingTwo();
        }
	}

    void ThingOne()
    {

    }

    void ThingTwo()
    {

    }
}
