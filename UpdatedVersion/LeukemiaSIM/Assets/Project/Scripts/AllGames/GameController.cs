using UnityEngine;
using Valve.VR;

public abstract class GameController : MonoBehaviour
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

    private Transform m_CameraRig;
    private Transform m_Head;

    private bool m_GamePaused = false;


    protected virtual void Update()
    {
        CheckForGamePause();

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



    public void SetCameraRigPos(Vector3 newPos)
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


    private Quaternion GetCameraRigRotation()
    {
        //Returns the rotation of the camera rig
        return m_Head.rotation;
    }

    public Quaternion GetCameraRigLocalRotation()
    {
        return m_Head.localRotation;
    }

    public Vector3 GetHeadForward()
    {
        //Returns the direction that the head is facing
        return m_Head.forward;
    }


    public Vector3 GetHead()
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


    protected abstract void PauseGame();

    public abstract void Unpause();


    protected bool GetGamePaused()
    {
        //Getter for the paused game
        return m_GamePaused;
    }


    protected void MakeCameraParent(Transform child)
    {
        //Sets the parent in accordance to what the function is called with
        child.SetParent(SteamVR_Render.Top().head.transform);
    }


}





