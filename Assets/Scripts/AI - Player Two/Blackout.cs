using UnityEngine;
using System.Collections;

public class Blackout : MonoBehaviour {
    private Animator m_blackoutAnim;

    void Start()
    {
        m_blackoutAnim = gameObject.GetComponent<Animator>();
    }
    public void EnterBlackout(int time)
    {
        m_blackoutAnim.SetTrigger("Enter");
        Invoke("ExitBlackout", time);
    }

    void ExitBlackout()
    {
        m_blackoutAnim.SetTrigger("Exit");
    }
}
