using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CharacterInteract : GameController
{
    [SerializeField]
    protected CharTalk m_CharTalk;
    [SerializeField]
    private Collider m_SelfCollider;

    [SerializeField]
    protected CanvasGroup m_CharacterCanvasGroup;
    [SerializeField]
    private GameObject m_Instructions;
    [SerializeField]
    private GameObject m_PlayGameButton;
    [SerializeField]
    private GameObject m_ReturnToHallwayButton;

    protected bool m_IsPlayerInRange;
    [SerializeField]
    private const float m_MaximumRange = 6f;
    [SerializeField]
    private const float m_MinimumRange = 2f;

    private bool m_Talking = false;
    private bool m_DoneTalking = false;
    private float m_TimeToTalk;

    private Animator m_Animator;
    [SerializeField]
    private Animator m_SecondAnimator = null;
    private AudioSource m_Audio;

    private float m_DistanceFromCharacters = 4f;



    private void Start()
    {
        //Set up animator
        m_Animator = transform.GetChild(0).GetComponent<Animator>();

        //Set up the audio
        m_Audio = GetComponent<AudioSource>();
        if (m_Audio != null)
            m_TimeToTalk = m_Audio.clip.length;
        else
            m_TimeToTalk = 10;

        base.SetUpCameraRig();
    }



    protected override void Update()
    {
        //Send out a raycast from the m_Player
        if (!m_Talking)
            CheckRay();


        //If m_Player is facing the character, show the canvases for that character and allow for user input
        if (m_IsPlayerInRange && !m_Talking)
        {            
            //Player not m_Talking so original m_Instructions
            m_CharacterCanvasGroup.alpha = 1;
            m_Instructions.SetActive(true);

            if (base.GetUserInput())
            {
                //Slight delay in m_Talking so that the game doesn't automatically start
                StartCoroutine(StartTalking());

            } 
        }
        else if (!m_DoneTalking)
        {
            //Player not facing character so no m_Talking or canvas
            m_CharacterCanvasGroup.alpha = 0;
        }


        if (base.CheckMenuPressed())
            SceneManager.LoadScene(0);
    }



    protected void CheckRay()
    {
        //Check to see whether player is in the zone of the character
        RaycastHit hit;
        Vector3 endPos = base.GetHeadForward();
        endPos.y = 0.5f;

        //If it's a hit, with the character talking resumes
        if (Physics.Raycast(base.GetCameraRigPos(), endPos, out hit, m_MaximumRange) && hit.collider == m_SelfCollider)
            m_IsPlayerInRange = true;

        //Talking does not resume, not in the hit zone
        else
            m_IsPlayerInRange = false;

    }



    private IEnumerator StartTalking()
    {

        float fadeTime = 0.3f;
        base.Fade(Color.black, fadeTime);

        
        float waitTime = 3f;
        yield return new WaitForSeconds(waitTime);
        MovePlayerInFront();

        //Start the character talking
        m_CharTalk.StartTalk(gameObject);
        //Turn on the glowing floor
        transform.GetChild(2).gameObject.SetActive(true);
        m_Talking = true;


        base.Fade(Color.clear, fadeTime);

        //Begin animation

        if (m_Animator != null)
            m_Animator.SetBool("IsTalking", true);

        if (m_SecondAnimator != null)
        {
            m_SecondAnimator.SetBool("IsTalking", true);
        }
            
        
        yield return new WaitForSeconds(fadeTime);


        //Start sound
        if (m_Audio != null)
            m_Audio.Play();

        yield return new WaitForSeconds(m_TimeToTalk);

        //Display the screen to continue with the game
        DisplayOptions();

        //Change animations
        if (m_Animator != null)
            m_Animator.SetBool("IsTalking", false);

        if (m_SecondAnimator != null)
        {
            m_SecondAnimator.SetBool("IsTalking", false);
        }

    }



    private void MovePlayerInFront()
    {
        //Figure out translation
        Vector3 translateVector;

        Transform transformSelf = GetComponent<Transform>();

        translateVector = (transformSelf.position + transformSelf.forward * m_DistanceFromCharacters);
        translateVector.y = 0;

        //Move to new position
        base.SetCameraRigPos(translateVector);
    }



    private void DisplayOptions()
    {
        //The character is now done talking
        m_DoneTalking = true;

        //Show the buttons to either play the game or return to the main hallway
        m_CharacterCanvasGroup.alpha = 1;
        m_Instructions.SetActive(false);
        if(m_PlayGameButton != null)
            m_PlayGameButton.SetActive(true);
        m_ReturnToHallwayButton.SetActive(true);

        //Turn on the pointer on the main controller
        m_CharTalk.EnablePointer(true);
    }


    protected override void PauseGame()
    {
        //Turn on the pointer on the main controller
        m_CharTalk.EnablePointer(true);
        base.PauseGame();
    }
}
