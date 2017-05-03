using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScape : MonoBehaviour
{
    public AudioClip[] sfx;
    private AudioSource[] m_audioSource;
    private Animator m_animator;
    public float offset;
    void Start()
    {
        m_audioSource = GetComponents<AudioSource>();
        m_animator = GetComponent<Animator>();
        InvokeRepeating("SoundOne", offset, 6);
        //InvokeRepeating("SoundTwo", 3, 6);
    }

    void SoundOne()
    {
        Invoke("ChangeClipOne", 1);
        m_animator.SetTrigger("Change");
    }

    void ChangeClipOne()
    {
        m_audioSource[0].clip = sfx[Random.Range(0, sfx.Length)];
        m_audioSource[0].Play();
    }
    /*
    bool m_mute;

    IEnumerator ChangeClipOne()
    {
        while(!m_mute)
        {
            yield return new WaitForFixedUpdate();
        }
        m_mute = true;
    }
    */
    void SoundTwo()
    {
        Invoke("ChangeClipTwo", 1);
        m_animator.SetTrigger("Change2");
    }

    void ChangeClipTwo()
    { 
        m_audioSource[1].clip = sfx[Random.Range(0, sfx.Length)];
        m_audioSource[1].Play();
    }
}
