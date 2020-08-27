using System.Collections;
using UnityEngine;


public class ObstacleController : MonoBehaviour
{
    [SerializeField]
    private AudioClip [] m_Sounds;

    private Collider m_Player;
    private MainErythrocyteGameController m_MainController;


    public void SetPlayer(Collider gamer)
    {
        //Setter that sets the main player to collider of the player (rigid body which encases the player). Used when obstacle instantiated
        m_Player = gamer;
    }



    public void SetMainController(MainErythrocyteGameController controller)
    {
        //Setter that sets the main erythrocyte game controller for the obstacle when it is instantiated into the game
        m_MainController = controller;
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        //If the player collides with the obstacle - make the player lose a life then destroy the obstacle so the player can keep moving

        if (collision.collider == m_Player)
        {
            m_MainController.LoseLife();
            StartCoroutine(DestroyObject());
        }
    }



    private IEnumerator DestroyObject()
    {
        //Play the lose a life sound
        AudioSource audio = GetComponent<AudioSource>();
        GetComponent<Renderer>().enabled = false;
        audio.clip = m_Sounds[m_MainController.GetRandomNum(0, m_Sounds.Length)];
        audio.Play();

        //Wait until the audio has played
        yield return new WaitForSeconds(audio.clip.length);


        //Destroy the game object so the player can keep playing
        audio.Stop();
        Destroy(gameObject);

    }
}
