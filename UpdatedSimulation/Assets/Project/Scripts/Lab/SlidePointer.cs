using UnityEngine;

public class SlidePointer : Pointer
{
    private CharacterInteract m_PreviousHit = null;

    public override void RespondToHit(RaycastHit hit)
    {
        //Stop the last highlight
        if (m_PreviousHit != null) m_PreviousHit.StopHighlight();


        //Check if the hit is in the slides, if it is highlight and update previous hit
        if (hit.transform != null && hit.transform.GetComponent<CharacterInteract>() != null)
        {
            CharacterInteract currentHit = hit.transform.GetComponent<CharacterInteract>();
            currentHit.Highlight();
            m_PreviousHit = currentHit;

        }

    }

}
