using UnityEngine;

public class RotateVRPlayer : MonoBehaviour
{
    [SerializeField]
    private RotateVRPlayer m_PreviousRotation;
    [SerializeField]
    private Transform m_Player;
    [SerializeField]
    private bool m_DisableGravity = false;

    private bool m_CanRotate = false;
    private Transform m_ToRotate;
    private Quaternion m_TargetRotation;

    private void Update()
    {
        //If m_ToRotate can been rotated, rotate m_ToRotate at half the speed of time
        if (m_CanRotate)
        {
            m_ToRotate.rotation = Quaternion.Lerp(m_ToRotate.rotation, m_TargetRotation, Time.deltaTime * 0.25f );
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        //Set up what is to be rotating
        m_ToRotate = other.transform;


        if(m_ToRotate = m_Player)
        {
            //Find out where the player must rotate next
            Vector3 relativePos = m_ToRotate.forward - transform.forward;
            float rotation = Quaternion.LookRotation(relativePos, Vector3.up).eulerAngles.y;
            m_TargetRotation = Quaternion.Euler(m_ToRotate.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 180,
                m_ToRotate.rotation.eulerAngles.z);


            //Turn off the rotation caused by the previous collider
            if (m_PreviousRotation != null)
                m_PreviousRotation.TurnOffRotation();

            //Start rotating
            m_CanRotate = true;

            //Turn off gravity if needed
            if(m_DisableGravity)
                Physics.gravity = Vector3.zero;
        }



    }


    public void TurnOffRotation()
    {
        //Obstacle no longer rotates the player
        m_CanRotate = false;
    }

}
