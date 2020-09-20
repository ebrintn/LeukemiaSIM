using UnityEngine;
using TMPro;

public abstract class MiniGameController : GameController
{
    [SerializeField]
    private Transform m_UI;
    [SerializeField]
    private GameObject m_StartUI;
    [SerializeField]
    private GameObject m_GameUI;
    [SerializeField]
    private GameObject m_PauseUI;
    [SerializeField]
    private GameObject m_WinGameUI;
    [SerializeField]
    private TextMeshProUGUI m_WinningScore;


    [SerializeField]
    private GameObject m_Camera;

    private bool m_GameWon;
    private bool m_Playing;

    private Vector3 m_UIStartPos;
    private Quaternion m_UIStartRot;



    protected virtual void Start()
    {
        //No movement at the beginning
        Time.timeScale = 0;

        //Sets up the UI game
        SetUpUI();

        //Set up the camera rig
        base.SetUpCameraRig();

    }



    protected virtual void SetUpUI()
    {
        //Sets the starting UI states
        m_StartUI.SetActive(true);
        m_GameUI.SetActive(false);
        m_PauseUI.SetActive(false);
        m_WinGameUI.SetActive(false);

        //If the UI is moved resets to the correct location
        m_UIStartPos = m_UI.localPosition;
        m_UIStartRot = m_UI.localRotation;
     }



    public virtual void StartGame(int level = 0)
    {
        //Shows the correct displays for gameplay
        m_StartUI.SetActive(false);
        m_GameUI.SetActive(true);
        StartCoroutine(m_GameUI.gameObject.GetComponent<GamePlayUI>().CountDown());

        //Player is now playing 
        m_Playing = true;
        Unpause();




        //Make the canvas the child of the camera
        base.MakeCameraParent(m_UI);

    }


    public virtual void WinGame(string score = "WinningScore")
    {
        //Variable used in child classes to update UI
        m_GameWon = true;
        m_Playing = false;
        PauseGame();

        //Display the correct winning score text
        m_WinningScore.SetText(score);

        //Shows the correct UI display
        m_WinGameUI.SetActive(true);
        m_PauseUI.SetActive(false);
        m_GameUI.SetActive(false);
    }



    protected bool GetGameWon()
    {
        //Return win state
        return m_GameWon;
    }


    protected bool IsPlaying()
    {
        //Tells children classes if the game is being played
        return m_Playing;
    }


    protected override void PauseGame()
    {
        //Changes the UI as needed for the mini game is paused
        m_GameUI.SetActive(false);
        m_StartUI.SetActive(false);
        m_WinGameUI.SetActive(false);
        m_PauseUI.SetActive(true);


        //Change the timescale appropriately for pause
        Time.timeScale = 0;

    }


    public override void Unpause()
    {
;        //Changes the UI as needed for the mini game when the game is unpaused
        if (m_Playing)
            m_GameUI.SetActive(true);
        else if (m_GameWon)
            m_WinGameUI.SetActive(true);
        else
            m_StartUI.SetActive(true);
        m_PauseUI.SetActive(false);
    }



    protected void MoveUIUp(float amount)
    {
        //Change the position of the UI to the new position by the amount set by the other script
        m_UI.position = new Vector3 (m_UI.position.x, m_UI.position.y + amount, m_UI.position.z);
    }


}
