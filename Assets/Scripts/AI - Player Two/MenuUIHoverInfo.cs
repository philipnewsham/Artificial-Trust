using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MenuUIHoverInfo : MonoBehaviour/*, IPointerEnterHandler, IPointerExitHandler*/ {
    public Text infoBox;
    public string info;
	// Use this for initialization
    public void OnMouseOver()
    {
        infoBox.text = info;
    }

    void OnMouseEnter()
    {
        infoBox.text = info;
    }
}
