using System.Collections;
using UnityEngine;

public class CharTalk : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Hallway;
    [SerializeField]
    private GameObject m_Room;
    [SerializeField]
    private GameObject[] m_InteractableCharacters;
    [SerializeField]
    private Material[] m_Skyboxes;
    [SerializeField]
    private ArmMovement m_ArmMovement;
    [SerializeField]
    private GameObject m_Pointer;
    [SerializeField]
    private GameController m_Controller;

    private GameObject m_TalkingChar;
    private FadeMusic m_SoundFader;




    void Start()
    {
        m_SoundFader = GetComponent<FadeMusic>();

        //Make sure the pointer is disabled
        EnablePointer(false);

        //Make sure player can move
        Time.timeScale = 1;
    }


    public void StartTalk(GameObject talkingChar)
    {
        //Set up the talking char variable
        m_TalkingChar = talkingChar;

        //Changes the skybox to the spacelike skybox
        RenderSettings.skybox = m_Skyboxes[1];

        //Makes all characters that are not talking invisible
        foreach (GameObject character in m_InteractableCharacters)
        {
            if (character != m_TalkingChar)
                character.SetActive (false);
        }

        //Makes the main hallway disappear
        m_Hallway.SetActive(false);

        //Turn off arm movement
        ChangeArmMovement(false);

        //Fades out the music
        m_SoundFader.Fade();

    }

    public IEnumerator EndTalk()
    {
        //Fades in the music
        m_SoundFader.Increase();

        //Fade to black
        float fadeTime = 0.3f;
        m_Controller.Fade(Color.black, fadeTime);
        yield return new WaitForSeconds(1);

        //Turn on the hallway/change skybox/turn on characters
        m_Hallway.SetActive(true);
        RenderSettings.skybox = m_Skyboxes[0];
        foreach (GameObject character in m_InteractableCharacters)
        {
            character.SetActive(true);
            //Reset the floor
            character.transform.GetChild(2).gameObject.SetActive(false);
        }

        //Turn on arm movement
        ChangeArmMovement(true);

        //Fade to normal colour
        m_Controller.Fade(Color.clear, fadeTime);

    }



    private void ChangeArmMovement(bool on)
    {
        //Turns on or off the arm movement action to make sure the player watches the scene
        m_ArmMovement.enabled = on;
    }


    public void EnablePointer(bool isActive)
    {
        //Set the pointer activity to the state needed by the game
        m_Pointer.SetActive(isActive);
    }

}
