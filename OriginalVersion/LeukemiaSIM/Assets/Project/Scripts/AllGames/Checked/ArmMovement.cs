using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ArmMovement : MonoBehaviour
{
    [SerializeField]
    private Transform m_RightController = null;
    [SerializeField]
    private Transform m_LeftController = null;
    [SerializeField]
    private float m_Speed;
    [SerializeField]
    private float m_WalkDistance = 1f;
    [SerializeField]
    private float m_DontWalkDistance = 4f;
    [SerializeField]
    private float m_DistanceBetweenRemotesForWalk = 0.05f;

    private Vector3 m_RightControllerPos;
    private Vector3 m_LeftControllerPos;

    private string m_RemoteAbove = "Right";
       
    private Transform m_CameraRig;
    private Transform m_Head;

    private Vector3 m_MoveTowards;

    private bool m_FirstMovement;


    //Set up required variables
    private void Start()
    {
        m_CameraRig = SteamVR_Render.Top().origin;
        m_Head = SteamVR_Render.Top().head;

        m_Speed = 2 * m_WalkDistance;
    }


    //Walk if the player performs the Walk motion
    private void Update()
    {
        if (CheckWalk())
            Walk();


        //Move
        float step = m_Speed * Time.deltaTime;
        m_CameraRig.position = Vector3.MoveTowards(m_CameraRig.position, m_MoveTowards, step);

    }

    
    private bool CheckWalk()
    {
        //Verify whether the Walk action is happening
        m_RightControllerPos = m_RightController.position;
        m_LeftControllerPos = m_LeftController.position;

        if ((m_LeftControllerPos.y - m_RightControllerPos.y) > m_DistanceBetweenRemotesForWalk && (m_RemoteAbove == "Right") && m_FirstMovement)
        {
            m_FirstMovement = false;

        }
        else if ((m_RightControllerPos.y - m_LeftControllerPos.y) > m_DistanceBetweenRemotesForWalk && (m_RemoteAbove == "Right"))
        {
            m_RemoteAbove = "Left";
            return true;
            
        }
        else if ((m_LeftControllerPos.y - m_RightControllerPos.y )> m_DistanceBetweenRemotesForWalk && (m_RemoteAbove == "Left"))
        {
            m_RemoteAbove = "Right";
            return true;
        }

        //No walking - one remote is staying over the other one
        return false;
    }



    private void Walk()
    {
        //Figure out translation
        Vector3 groundPosition = m_Head.forward * m_WalkDistance;
        groundPosition.y = 0;

        //Update move position
        if (CheckNoObstacle(m_CameraRig.position, groundPosition))
            m_MoveTowards = m_CameraRig.position + groundPosition;

    }




    private bool CheckNoObstacle(Vector3 rayStart, Vector3 rayEnd)
    {

        RaycastHit hit;

        rayEnd.y = 0.5f;

        //If it's a hit, don't want to Walk
        if (Physics.Raycast(rayStart, rayEnd, out hit, m_DontWalkDistance))
            return false;

        //Nothing in front, can Walk
        return true;
    }
}
