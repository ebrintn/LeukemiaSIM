using System.Collections;
using UnityEngine;



public class OxygenController : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] m_Sounds;
    [SerializeField]
    private GetRandom m_Randomizer;
    [SerializeField]
    private Collider m_Player;
    [SerializeField]
    private MainErythrocyteGameController m_MainController;
    private AudioSource m_AudioSource;


    private void Awake()
    {
        //Set up the audio source to play the sounds
        m_AudioSource = GetComponent<AudioSource>();
    }



    private void OnTriggerEnter(Collider other)
    {
        //When an oxygen character is hit, increase the amount of oxygen collected and then destroy the oxygen
        if (other == m_Player)
        {
            m_MainController.IncreaseOxygen(transform);
            StartCoroutine(PlayAudioClip());
        }
    }



    private IEnumerator PlayAudioClip()
    {
        //Play a random congratulatory sound
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = m_Sounds[m_Randomizer.GetRandomNum(0, m_Sounds.Length)];
        audio.Play();

        //Wait until the audio has finished playing
        yield return new WaitForSeconds(audio.clip.length);

        //Stop the audio
        audio.Stop();

    }

}
