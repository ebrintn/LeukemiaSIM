using UnityEngine;
using Valve.VR;

public class VRPlayer : MonoBehaviour
{

    float m_Rotation;

    //Called when level starts, starts the VR game: can be modified when making iOS or PC versions
    private void Start()
    {
        PlayVRGame();
    }


    private void PlayVRGame()
    {

        Transform cameraRig = SteamVR_Render.Top().origin;
        Transform head = SteamVR_Render.Top().head;


        //Move the rotation of the rig to the relative position in space
        Vector3 headRotation = head.rotation.eulerAngles;


        float rotationForY = 0f;

        //Adjust for the position in space based on location 
        switch (headRotation.y)
        {
            case float angle when (angle < 45f):
                rotationForY = headRotation.y - ((headRotation.y + 45) * 2) + 180;
                break;
            case float angle when (angle < 135f):
                rotationForY = headRotation.y - ((headRotation.y - 45) * 2);
                break;
            case float angle when (angle < 225f):
                rotationForY = headRotation.y - ((headRotation.y - 135) * 2) + 180;
                break;
            case float angle when (angle < 315f):
                rotationForY = headRotation.y - ((headRotation.y - 225) * 2);
                break;
            case float angle when (angle < 360f):
                rotationForY = headRotation.y - ((headRotation.y - 315) * 2) + 180;
                break;
            default:
                print("Reached default: should not happen");
                break;
        }



        //Set the new rotation to the area
        cameraRig.transform.rotation = Quaternion.Euler(cameraRig.transform.rotation.eulerAngles.x,
            rotationForY, cameraRig.transform.rotation.eulerAngles.z);


        //Move the position of the character to the relative position in space
        Vector3 headPosition = cameraRig.position + (cameraRig.position - head.position);
        headPosition.y = cameraRig.position.y;
        cameraRig.position = headPosition;

               
    }


    public float GetRotation()
    {
        //Getter for the y rotation
        return m_Rotation;
    }


}
