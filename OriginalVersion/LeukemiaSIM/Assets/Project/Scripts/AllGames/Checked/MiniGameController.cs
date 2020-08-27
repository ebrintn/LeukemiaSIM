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
    private GameObject m_MenuArea;
    [SerializeField]
    private GameObject m_PlayArea;

    [SerializeField]
    private AudioClip m_Music;
    [SerializeField]
    private AudioSource m_AudioSource;

    [SerializeField]
    private GameObject m_Camera;

    private bool m_GameWon;
    private bool m_Playing;

    private Vector3 m_UIStartPos;
    private Quaternion m_UIStartRot;


    protected virtual void Start()
    {
        //Sets up the UI game
        base.PauseGame();
        SetUpUI();

    }


    protected override void Update()
    {
        //Change game text
        SetGameText("Time: " + base.GetTime());

        //Change winning text
        if (GetGameWon())
        {
            SetWinningText(string.Format("Your time was: {0}", base.GetTime()));
        }

        base.Update();


    }


    protected virtual void SetUpUI()
    {
        //Sets the starting UI states
        m_StartUI.SetActive(true);
        m_GameUI.SetActive(false);
        m_PauseUI.SetActive(false);
        m_WinGameUI.SetActive(false);

        //If the UI is moved resets to the correct location
        m_UIStartPos = m_UI.position;
        m_UIStartRot = m_UI.rotation;
     }



    public virtual void StartGame(int NumObjectsToStart = 0)
    {
        //Shows the correct displays for gameplay
        m_StartUI.SetActive(false);
        m_GameUI.SetActive(true);

        //Player is now playing 
        m_Playing = true;
        Unpause();


        ////Changes to the music instead of the instructions
        if(m_AudioSource != null)
        {
            m_AudioSource.clip = m_Music;
            m_AudioSource.volume = 0.1f;
            m_AudioSource.Play();
        }


    }


    public virtual void WinGame()
    {
        //Variable used in child classes to update UI
        m_GameWon = true;
        m_Playing = false;
        PauseGame();

        //Shows the correct UI display
        m_WinGameUI.SetActive(true);
        m_PauseUI.SetActive(false);
        m_GameUI.SetActive(false);
    }



    protected void SetGameText(string textToSet)
    {
        //Sets the text in the corner of the screen to the given text
        m_GameUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(textToSet);
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


    protected void SetWinningText(string textToSet)
    {
        //Change the winning display text
        m_WinGameUI.transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText(textToSet);
    }


    protected override void PauseGame()
    {
        //Changes the UI as needed for the mini game is paused
        m_GameUI.SetActive(false);
        m_StartUI.SetActive(false);
        m_WinGameUI.SetActive(false);
        m_PauseUI.SetActive(true);

        ChangePlayArea(true);
        base.PauseGame();

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

        ChangePlayArea(false);
        base.Unpause();
    }



    public virtual void ChangePlayArea(bool paused)
    {
        //Changes the screen of the play area for game pauses
        m_MenuArea.SetActive(paused);
        m_PlayArea.SetActive(!paused);
    }


    protected void MoveUIUp(float amount)
    {
        //Change the position of the UI to the new position by the amount set by the other script
        m_UI.position = new Vector3 (m_UI.position.x, m_UI.position.y + amount, m_UI.position.z);
    }


    protected void MakeCameraUIParent(bool attach=true)
    {
        //Attach to the camera
        if (attach)
            m_UI.SetParent(m_Camera.transform);
        //Remove from the camera
        else
        {
            m_UI.SetParent(null);

            m_UI.position = m_UIStartPos;
            m_UI.rotation = m_UIStartRot;
        }

    }


    protected void SetPlayingAreaAsParent(Transform childToSet)
    {
        //Sets the child to the play area
        childToSet.SetParent(m_PlayArea.transform);
    }

}
