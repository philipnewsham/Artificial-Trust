using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseObjectives : MonoBehaviour
{
    private int[] m_subGoals = new int[3];
    private List<int> m_goalTypes = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    private int m_randomNo;

	void Start ()
    {
        for (int i = 0; i < m_subGoals.Length; i++)
        {
            m_randomNo = Random.Range(0, 10 - i);
            m_subGoals[i] = m_goalTypes[m_randomNo];
            m_goalTypes.RemoveAt(m_randomNo);
        }
    }
}
