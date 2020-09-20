using System.Collections;
using UnityEngine;

public class VRPlayerMovement : MonoBehaviour
{
    [SerializeField]
    private MainErythrocyteGameController m_Controller;
    [SerializeField]
    private Transform m_Child;

    private float m_ForwardSpeed = 0f;


    private float m_SpeedEasy = 10f;
    private float m_SpeedHard = 15f;



    private void Update()
    {
        //Add velocity based on where the players head is in space
        AddPlayerVelocity();

    }


    public void StartGame(float speed)
    {
        //Set up the speed of travel based on the players choice

        switch (speed)
        {
            case 0:
                m_ForwardSpeed = m_SpeedEasy;
                break;
            case 1:
                m_ForwardSpeed = m_SpeedHard;
                break;
        }

        //Add the VR Player as a child of the player movement after the camera has been rotated to account for 
        //the player position
        m_Child.SetParent(transform);
        
    }



    private void AddPlayerVelocity()
    {
        //Do not add velocity if game paused
        if (Time.timeScale == 1)
        {
            //Move the player character to the position where the player is looking
            Vector3 newVesselPos = m_Controller.GetHeadForward();
            newVesselPos = new Vector3(newVesselPos.x, 0, newVesselPos.z);

            //Move the rigidbody of the player to the position where they are looking

            transform.GetComponent<Rigidbody>().AddForce(newVesselPos * m_ForwardSpeed, ForceMode.Force);
        }


    }


    public IEnumerator ReachEnd()
    {
        yield return new WaitForSeconds(1f);

        //Turn off movement
        m_ForwardSpeed = 0f;

        //Win the game
        m_Controller.WinGame();
    }

}
