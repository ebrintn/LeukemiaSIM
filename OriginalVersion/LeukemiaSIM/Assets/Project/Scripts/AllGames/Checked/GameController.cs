using UnityEngine;
using Valve.VR;
using Random = System.Random;

public abstract class GameController : Timer
{
    [SerializeField]
    private GameObject m_Player;
    [SerializeField]
    private int m_GameSceneNum;
    [SerializeField]
    private int m_MainGameSceneNum = 2;

    [SerializeField]
    private SteamVR_Action_Boolean m_GrabPinchAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");
    [SerializeField]
    private SteamVR_Action_Boolean m_MenuAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Menu");
    [SerializeField]
    private SteamVR_Action_Single m_SqueezeAction = SteamVR_Input.GetAction<SteamVR_Action_Single>("Squeeze");
    [SerializeField]
    private SteamVR_Input_Sources m_HandType = SteamVR_Input_Sources.Any;


    private Random m_Random = new Random();
    private int m_PreviousRandomNum = 0;

    private Transform m_CameraRig;
    private Transform m_Head;

    private bool m_GamePaused = false;


    protected virtual void Update()
    {
        CheckForGamePause();

        //Pauses all based time actions when the game is paused
        if (m_GamePaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

    }


    protected void CheckForGamePause()
    {
        //Checks to see if the player has pressed the menu pause button
        bool pauseGame = CheckMenuPressed();

        //If the menu button has been pressed displays the pause game screen, and pauses the timer
        if (pauseGame)  
            PauseGame();
    }


    public void SetUpCameraRig()
    {
        //Sets up the camera rig if needed for the scene
        m_CameraRig = SteamVR_Render.Top().origin;
        m_Head = SteamVR_Render.Top().head;
    }


    public GameObject GetPlayer()
    {
        //Safely returns the player object to the other script
        return m_Player;
    }



    public bool GetUserInput()
    {
        //Lets other objects get information on the state of the controller
        return m_GrabPinchAction.GetStateDown(m_HandType);
    }



    public float GetTriggerSqueeze()
    {
        //Returns the pressure of the trigger squeezing action - for partial trigger press
        return m_SqueezeAction.GetAxis(m_HandType);
    }


    public int GetRandomNum(int lowestNum, int highestNum)
    {
        //Returns a random number between the lowest and highest number which is different from the last num
        int randNum = m_PreviousRandomNum;
        while (randNum == m_PreviousRandomNum)
        {
            randNum = m_Random.Next(lowestNum, highestNum);
        }
        m_PreviousRandomNum = randNum;
        return randNum;

    }

    public Vector3 GetRandomPosition(float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
    {
        //Returns a random position in space
        float posX = UnityEngine.Random.Range(minX, maxX);
        float posY = UnityEngine.Random.Range(minY, maxY);
        float posZ = UnityEngine.Random.Range(minZ, maxZ);

        return new Vector3(posX, posY, posZ);
    }


    protected void SetCameraRigPos(Vector3 newPos)
    {
        //Changes the position of the camera rig to the new position
        m_CameraRig.position = newPos;
    }



    protected void SetCameraRigLocalPos(Vector3 newPos)
    {
        //Changes the position of the camera rig to the new local position
        m_CameraRig.localPosition = newPos;
    }



    public Vector3 GetCameraRigPos()
    {
        //Returns the position of the camera rig
        return m_CameraRig.position;
    }


    protected Quaternion GetCameraRigRotation()
    {
        //Returns the rotation of the camera rig
        return m_Head.rotation;
    }

    public Vector3 GetHeadForward()
    {
        //Returns the direction that the head is facing
        return m_Head.forward;
    }


    protected Vector3 GetHead()
    {
        //Returns the relative position of the head
        return m_Head.localPosition;
    }



    public void Fade(Color colour, float fadeTime)
    {
        //Performs a steamVR fade action - in controller to ensure that all VR actions are together
        SteamVR_Fade.Start(colour, fadeTime, true);
    }


    protected bool CheckMenuPressed()
    {
        //Checks to see whether the menu button on the remote has been pressed
        return m_MenuAction.GetStateDown(m_HandType);
    }


    public virtual void Unpause()
    {
        //Unpause the game
        m_GamePaused = false;
    }


    protected virtual void PauseGame()
    {
        //Used by children to pause game at beginning of mini games
        m_GamePaused = true;
    }


    protected bool GetGamePaused()
    {
        //Getter for the paused game
        return m_GamePaused;
    }


}





