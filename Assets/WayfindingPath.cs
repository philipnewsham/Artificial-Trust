using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayfindingPath : MonoBehaviour
{
    public Transform[] wayfindingPath;
    private int m_currentPosition = -1;
    
    void OnTriggerEnter(Collider other)
    {
        m_currentPosition += 1;
        if(m_currentPosition != wayfindingPath.Length)
        {
            transform.position = wayfindingPath[m_currentPosition].transform.position;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
