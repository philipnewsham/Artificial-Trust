using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayfindingPath : MonoBehaviour
{
    private Transform m_startingPoint;
    public Transform[] wayfindingPath;
    private int m_currentPosition = -1;
    
    void Start()
    {
        m_startingPoint = GetComponent<Transform>();
    }
    void OnTriggerEnter(Collider other)
    {
        m_currentPosition += 1;
        if(m_currentPosition != wayfindingPath.Length)
        {
            transform.position = wayfindingPath[m_currentPosition].transform.position;
        }
        else
        {
            gameObject.SetActive(false);
            transform.position = m_startingPoint.position;
            m_currentPosition = -1;
        }
    }

    public void EnableWayfinding()
    {
        gameObject.SetActive(true);
    }
}
