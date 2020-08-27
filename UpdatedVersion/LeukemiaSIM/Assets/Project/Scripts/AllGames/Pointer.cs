using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Pointer : MonoBehaviour
{
    [SerializeField]
    private float m_DefaultLength = 5.0f;
    [SerializeField]
    private GameObject m_Dot;
    [SerializeField]
    private VRInputModule m_InputModule;
   

    private bool m_CanPoint = true;
    protected LineRenderer m_LineRenderer = null;


    protected void Awake()
    {
        //Set up the line renderer
        m_LineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        //Check to see whether the ray from the end of the remote is colliding with anything
        //If a collision occurs render the line and dot at that point
        if (m_CanPoint)
        {
            UpdateLine();
            m_Dot.SetActive(true);
            m_LineRenderer.enabled = true;
        }
        else
        {
            m_Dot.SetActive(false);
            m_LineRenderer.enabled = false;
        }


    }

    public void SetCanPoint(bool point)
    {   //Change the point variable     
        m_CanPoint = point;
    }


    private void UpdateLine()
    {
        //Use default or distance
        PointerEventData data = m_InputModule.GetData();
        float targetLength = data.pointerCurrentRaycast.distance == 0 ? m_DefaultLength : data.pointerCurrentRaycast.distance;

        //Raycast
        RaycastHit hit = CreateRaycast(targetLength);


        //Default
        Vector3 endPosition = transform.position + (transform.forward * targetLength);

        //Or based on hit
        if (hit.collider != null)
        {
            endPosition = hit.point;
        }


        //Set position of the m_Dot
        m_Dot.transform.position = endPosition;

        //Set linerenderer
        m_LineRenderer.SetPosition(0, transform.position);
        m_LineRenderer.SetPosition(1, endPosition);
    }


    private RaycastHit CreateRaycast(float length)
    {
        //Send out a raycast from the end of the remote
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, m_DefaultLength);

        RespondToHit(hit);

        return hit;
    }


    //Inherited to method to respond to a hit by the remote based on the level
    public abstract void RespondToHit(RaycastHit hit);



    public Vector3 GetDotPosition()
    {
        //Returns the end position of the pointer
        return m_Dot.transform.position;
    }


}
