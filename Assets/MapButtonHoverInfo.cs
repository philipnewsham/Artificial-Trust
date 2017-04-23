using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MapButtonHoverInfo : MonoBehaviour
{
    public string buttonName;
    public string roomName;
    public Text hoverText;
    
    public void MouseEnter()
    {
        if(buttonName != "")
            hoverText.text = string.Format("{0}\n{1}",buttonName,roomName);
        else
            hoverText.text = roomName;
    }

    public void MouseExit()
    {
        hoverText.text = "";
    }
}
