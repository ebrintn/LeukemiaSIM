using System.Collections;
using UnityEngine;



public class OxygenController : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] m_Sounds;

    private Collider m_Player;
    private MainErythrocyteGameController m_MainController;
    private AudioSource m_AudioSource;


    private void Awake()
    {
        //Set up the audio source to play the sounds
        m_AudioSource = GetComponent<AudioSource>();
    }



    public void SetPlayer(Collider gamer)
    {
        //Setter for the player collider which is required when the oxygen object is instantiated
        m_Player = gamer;
    }



    public void SetMainController(MainErythrocyteGameController controller)
    {
        //Setter for the game controller which is required when the oxygen object is instantiated
        m_MainController = controller;
    }


    private void OnTriggerEnter(Collider other)
    {
        //When an oxygen character is hit, increase the amount of oxygen collected and then destroy the oxygen
        if (other == m_Player)
        {
            m_MainController.IncreaseOxygen();
            StartCoroutine(DestroyObject());
        }
    }



    private IEnumerator DestroyObject()
    {
        //Play a random congratulatory sound
        AudioSource audio = GetComponent<AudioSource>();
        GetComponent<Renderer>().enabled = false;
        audio.clip = m_Sounds[m_MainController.GetRandomNum(0, m_Sounds.Length)];
        audio.Play();

        //Wait until the audio has finished playing
        yield return new WaitForSeconds(audio.clip.length);

        //Destroy the oxygen character so the player can keep on moving
        audio.Stop();
        Destroy(gameObject);

    }

}
