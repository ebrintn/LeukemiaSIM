using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabPointer : Pointer
{
    [SerializeField]
    Transform[] m_Slides;

    public override void RespondToHit(RaycastHit hit)
    {
        //Check if the hit is in the slides, if it is highlight it
        if (hit.transform != null && CheckInSlides(hit.transform))
        {
            hit.transform.GetComponent<PlateController>().Highlight();
        }

    }


    private bool CheckInSlides(Transform toCheck)
    {
        //If the hit is a slide return true, otherwise return false
        foreach (Transform slide in m_Slides)
        {
            if (toCheck == slide) return true;
        }


        return false;
    }


}
