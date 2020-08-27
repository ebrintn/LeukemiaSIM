using UnityEngine;


public class MainErythrocyteGameController : LivesGameController
{
    [SerializeField]
    private Transform m_Bus;
    [SerializeField]
    private Rigidbody m_PlayerRB;
    [SerializeField]
    private BoxCollider m_Collider;

    private float m_ForwardSpeed = 2f;
    private static int m_OxygenCollected = 0;



    protected override void Start()
    {
        //Camera rig needed to control player movement
        SetUpCameraRig();

        //Puts the player in the drivers seat of the bus
        m_Bus.position = new Vector3(m_Bus.position.x ,base.GetHead().y - 2f, m_Bus.position.z);


        base.Start();
    }


    protected override void Update()
    {
        //Update base first because images are different
        base.Update();

        //Shows the needed images for each of the lives and displays the correct game text
        base.SetGameText(string.Format("Students collected: {0}", m_OxygenCollected));


        if (base.GetGameWon())
        {
            //Sets the score of the game to the oxygen collected
            base.SetWinningText(string.Format("You collected {0} students", m_OxygenCollected));
        }

        //Recenter the player then the collider for the player
        MoveForward();
        m_Collider.center = new Vector3(m_Collider.center.x, base.GetHead().y, m_Collider.center.z);



    }
        
   

    public void IncreaseOxygen()
    {
        //Called when an oxygen is destroyed. Increase the oxygen variable.
        m_OxygenCollected++;
    }



    private void MoveForward()
    {
        //Move the player character to the position where the player is looking
        Vector3 newVesselPos = base.GetHeadForward() * m_ForwardSpeed;

        //Move the rigidbody of the player to the position where they are looking
        m_PlayerRB.velocity = newVesselPos;

    }


    public override void ChangePlayArea(bool paused)
    {
        //Turn the bus on and off when the play area is changed
        m_Bus.gameObject.SetActive(!paused);

        base.ChangePlayArea(paused);
    }


}
