﻿using UnityEngine;
using System.Collections;

public class AIWin : MonoBehaviour
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
