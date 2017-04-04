using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWin : MonoBehaviour
{
    public int mainObjective;
    public bool completedMission;

    // Update is called once per frame
    public void UpdateWin(int objective)
    {
        if (objective == mainObjective)
        {
            completedMission = true;
        }
    }
}
