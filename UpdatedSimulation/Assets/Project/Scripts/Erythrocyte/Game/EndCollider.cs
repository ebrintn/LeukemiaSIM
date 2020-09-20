using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCollider : MonoBehaviour
{
    [SerializeField]
    private Transform m_Player;

    private void OnTriggerEnter(Collider other)
    {
        //End the game once the player reaches the heart
        if(other.transform == m_Player)
            StartCoroutine(other.transform.GetComponent<VRPlayerMovement>().ReachEnd());


    }
}
