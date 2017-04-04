using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatedElevator : MonoBehaviour
{
    public AgentWin agentWinScript;
    public AIWin aiWinScript;

    public bool agentEscaped;
    public bool aiEscaped;
	
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Agent")
        {
            if (agentWinScript.completedMission == true)
            {
                if (agentWinScript.mainObjective == 0)
                {
                    if (aiWinScript.mainObjective == 0)
                    {
                        if (aiEscaped)
                        {
                            //end
                        }
                        else
                        {
                            //increase timer Speed
                        }
                    }
                    else 
                    {
                        //AI shuts down
                    }
                }
                else
                {
                    //win
                }
            }
        }

        if(other.gameObject.tag == "AI")
        {
            if (aiWinScript.completedMission == true)
            {
                if(agentWinScript.mainObjective == 0 && !agentEscaped)
                {
                    //increase timer speed
                }
                else if(agentWinScript.mainObjective == 1)
                {
                    //agent failed
                }
            }
        }
    }
}
