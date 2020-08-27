using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LabController : GameController
{
    [SerializeField]
    private Transform m_CurrentSample;

    [SerializeField]
    private Transform m_UIBase, m_Camera, m_VRPlayer, m_PauseUI;

    [SerializeField]
    private TextMeshProUGUI m_Collected;

    [SerializeField]
    private Button m_Back, m_PlayGame, m_Return, m_Quit;


    [SerializeField]
    private GetRandom m_Random;

    [SerializeField]
    private Vector3 m_MinPos, m_MaxPos;

    private float m_Speed = 0.05f;

    private Vector3 m_MoveTowards;


    private bool m_FindPose = true;

    private bool m_CanMove;


    [SerializeField]
    private int m_NumCharToFind;
    private int m_NumCharFound = 0;
    private string[] m_FoundCharacters;


    private Vector3 m_ResetPosition;
    private Quaternion m_ResetRotation;

    private int m_LoadSceneNum = 0;



    private void Start()
    {
        //Set up a string to fill as characters are found
        m_FoundCharacters = new string[m_NumCharToFind];

        //Set up the positions to reset the UI to
        if(m_UIBase != null)
        {
            m_ResetPosition = m_UIBase.localPosition;
            m_ResetRotation = m_UIBase.localRotation;
        }

        //Set up the button listners
        if(m_Back != null)
        {
            m_Back.onClick.AddListener(ReattachUI);
            m_PlayGame.onClick.AddListener(LoadPlayGame);
            m_Quit.onClick.AddListener(ReturnMain);
            m_Return.onClick.AddListener(Unpause);
            m_CanMove = true;
        }

        //Game not paused at the start
        Time.timeScale = 1;


    }

    protected override void Update()
    {

        if (m_CanMove)
        {
            //If the player is able to move move that player to the respective position to move to
            if (m_FindPose) StartCoroutine(FindNewPos());

            float step = m_Speed * Time.deltaTime;

            Transform player = base.GetPlayer().transform;

            player.position = Vector3.MoveTowards(player.position, m_MoveTowards, step);


            base.Update();
        }

    }



    private IEnumerator FindNewPos()
    {
        //Move the player towards a random position
        m_FindPose = false;


        m_MoveTowards = m_Random.GetRandomPosition(m_MinPos.x, m_MaxPos.x,
            m_MinPos.y, m_MaxPos.y, m_MinPos.z, m_MaxPos.z);
        m_MoveTowards.y = m_MinPos.y;

        //Wait for a random amount of time
        yield return new WaitForSeconds(m_Random.GetRandomNum(3, 10));

        //Find another new pose because a new pose can be found
        m_FindPose = true;
    }

    public Transform GetSample()
    {
        //Return the current sample
        return m_CurrentSample;
    }


    public void SetSample(Transform newSample)
    {
        //Set the current sample
        m_CurrentSample = newSample;
    }


    public void UpdateUI(string newSelection, int newSceneNum)
    {
        //Change the load scene num to the appropriate new scene num
        m_LoadSceneNum = newSceneNum;

        //Place the UI in the appropriate position
        m_UIBase.SetParent(m_VRPlayer);
        m_UIBase.rotation = Quaternion.Euler(0, m_UIBase.eulerAngles.y, 0);
        m_UIBase.localPosition = new Vector3(m_UIBase.localPosition.x, 
            m_ResetPosition.y, m_UIBase.localPosition.z);

        //Update the number of characters found if a character is found
        if (!CheckIfSelectionFound(newSelection))
        {
            m_FoundCharacters[m_NumCharFound] = newSelection.Substring(0, 2);
            m_NumCharFound++;
            m_Collected.SetText( "Blood units found: " + m_NumCharFound + "/" + m_NumCharToFind);
        }

        //Stop movement
        m_CanMove = false;

    }

    public void ReattachUI()
    {

        //Put the UI on the remote again
        m_UIBase.SetParent(m_Camera);
        m_UIBase.localPosition = m_ResetPosition;
        m_UIBase.localRotation = m_ResetRotation;


        //Turn off the UI
        foreach (Transform UI in m_UIBase.GetComponentInChildren<Transform>())
        {
            UI.gameObject.SetActive(false);
        }

        //Turn on movement 
        m_CanMove = true;
    }


    private void LoadPlayGame()
    {
        //Load the game that is currently attached to the UI
        SceneManager.LoadScene(m_LoadSceneNum);
    }


    private void ReturnMain()
    {
        //Reload the main game
        SceneManager.LoadScene(0);
    }


    private bool CheckIfSelectionFound(string newSelection)
    {
        //Substring the first 3 characters of the new selection, as these are the only characters needed
        // for the comparison
        newSelection = newSelection.Substring(0, 2);


        //Check to see if the new selection has already been found
        foreach (string character in m_FoundCharacters)
        {
            if (character == newSelection) return true;
        }

        return false;
    }


    public override void Unpause()
    {
        //Turn off the pause UI
        m_PauseUI.gameObject.SetActive(false);


        //Reset the UI in the appropriate place
        ReattachUI();
    }

    protected override void PauseGame()
    {
        //Turn on the pause UI
        m_PauseUI.gameObject.SetActive(true);

        //Place the UI in the appropriate position
        m_UIBase.SetParent(m_VRPlayer);
        m_UIBase.rotation = Quaternion.Euler(0, m_UIBase.eulerAngles.y, 0);
        m_UIBase.localPosition = new Vector3(m_UIBase.localPosition.x,
            m_ResetPosition.y, m_UIBase.localPosition.z);

        //Player cannot move around the scene
        m_CanMove = false;
    }
}
