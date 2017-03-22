using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class AIMessageScientist : MonoBehaviour
{
    public Text aiMessageText;
    public Text scientistRecieveText;
    private string m_message;

    public void SendMessageToScientist()
    {
        m_message = aiMessageText.text;
        scientistRecieveText.text = m_message;
    }
}
