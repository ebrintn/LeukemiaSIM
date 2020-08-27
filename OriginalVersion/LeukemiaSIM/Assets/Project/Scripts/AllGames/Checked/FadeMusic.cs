using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeMusic : MonoBehaviour
{
    private AudioSource m_AudioSource;
    private bool m_Fade = false;
    private bool m_Increase = false;

    private AudioClip m_NewAudioClip;
    private bool m_AudioClipChanged = true;


    void Start()
    {
        //Set up the audio source for the music that is being faded
        m_AudioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        //Change the volume according to instructions and the current volume
        if (m_AudioSource.volume <= 0)
        {
            if (m_NewAudioClip != null)
            {
                m_AudioSource.clip = m_NewAudioClip;
                m_AudioClipChanged = true;
            }
            m_Fade = false;
        }
        else if (m_AudioSource.volume > 1)
            m_Increase = false;
        else if (m_Fade)
            m_AudioSource.volume -= 0.008f;
        else if (m_Increase)
            m_AudioSource.volume += 0.008f;


        if (m_AudioClipChanged)
            m_NewAudioClip = null;

    }


    public void Fade(AudioClip newAudioClip = null)
    {
        //Allow the audio source to fade the sound
        m_Fade = true;
        m_Increase = false;

        //Change the audio clip
        m_NewAudioClip = newAudioClip;
        m_AudioClipChanged = false;
    }


    public void Increase()
    {
        //Allow the audio source to increase the sound
        m_Increase = true;
        m_Fade = false;
    }
}
