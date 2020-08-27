using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Highlighter;
    [SerializeField]
    private LabController m_GameController;

    [SerializeField]
    private ZoomButton m_Zoom;
    [SerializeField]
    private Texture[] m_ZoomTextures;

    [SerializeField]
    private int m_MicroMeNum;

 


    private void FixedUpdate()
    {
        //Turn off highlighting if not set
        StopHighlight();
    }


    public void Highlight()
    {
        //Turn on the highlighter to make the object green and switch plates if should

        m_Highlighter.SetActive(true);
        CheckIfPlateSwitch();
    }

    public void StopHighlight()
    {
        //Turn off the highlighter to make the object normal

        m_Highlighter.SetActive(false);
    }



    private void CheckIfPlateSwitch()
    {
        //Check if a button pressed - if pressed change
        if(m_GameController.GetUserInput()) SwitchPlate(m_GameController.GetSample());
    }


    public void SwitchPlate(Transform currentPlateVeiw)
    {
        //Swap the position and rotation of the plate under the microscope and the plate to switch with
        Vector3 currentPosition = transform.position;
        Quaternion currentRotation = transform.rotation;

        transform.position = currentPlateVeiw.position;
        transform.rotation = currentPlateVeiw.rotation;

        currentPlateVeiw.position = currentPosition;
        currentPlateVeiw.rotation = currentRotation;


        //Reset the screen to the furthest view
        m_Zoom.ResetZoom(m_ZoomTextures, m_MicroMeNum);

        //Change the sample in the game controller
        m_GameController.SetSample(transform);
    }


}
