using UnityEngine;


public class BloodVesselController : MonoBehaviour
{
    [SerializeField]
    private Collider m_PlayerCollider;
    [SerializeField]
    private MainErythrocyteGameController m_GameController;
    [SerializeField]
    private GameObject [] m_Obstacles;
    [SerializeField]
    private GameObject m_Oxygen;
    [SerializeField]
    private GameObject m_NextVessel;

    private Vector3 m_NextVesselPos;
    private int m_DistanceBetweenVessels = -120;
    private int m_NumOxygenToMake = 5;
    private int m_DistanceFromTransformX = 60;
    private int m_DistanceFromTransformY = 4;
    private int m_DistanceFromTransformZ = 4;



    private void Start()
    {
        //Make a random obstacle for the current blood vessel
        MakeObstacle();


        //Make randomly placed oxygen for the blood vessel
        for (int i = 0; i < m_NumOxygenToMake; i++)
        {
            MakeOxygen();
        }

        //Set up the next blood vessel position
        m_NextVesselPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + m_DistanceBetweenVessels);
        

    }




    private void MakeOxygen()
    {
        //Find a random position to place the oxygen, choose a rotation toward the direction where the player is going
        int posX = m_GameController.GetComponent<MainErythrocyteGameController>().GetRandomNum(-m_DistanceFromTransformX, m_DistanceFromTransformX);
        int posY = m_GameController.GetComponent<MainErythrocyteGameController>().GetRandomNum(-m_DistanceFromTransformY, m_DistanceFromTransformY);
        int posZ = m_GameController.GetComponent<MainErythrocyteGameController>().GetRandomNum(-m_DistanceFromTransformZ, m_DistanceFromTransformZ);

        Vector3 oxygenPosition = new Vector3(transform.position.x + posX, transform.position.y + posY, transform.position.z + posZ);
        Quaternion oxygenRotation = Quaternion.Euler(0, 90, 0);


        //Instantiate the oxygen and set up the player, controller and parent for the oxygen
        GameObject instantiatedOxygen = Instantiate(m_Oxygen, oxygenPosition, oxygenRotation);

        instantiatedOxygen.GetComponent<OxygenController>().SetPlayer(m_PlayerCollider);
        instantiatedOxygen.GetComponent<OxygenController>().SetMainController(m_GameController);

        instantiatedOxygen.transform.SetParent(transform);
    }


    private void MakeObstacle()
    {
        //Choose a random obstacle from the list of obstacles to place
        int obstacleNum = m_GameController.GetRandomNum(0, m_Obstacles.Length);
        GameObject obstacle = m_Obstacles[obstacleNum];

        //Instantiate the obstacle and rotate the obstacle to a random rotation
        GameObject instantiatedObstacle = Instantiate(obstacle, transform.position, transform.rotation);
        instantiatedObstacle.transform.Rotate(0f, m_GameController.GetComponent<MainErythrocyteGameController>().GetRandomNum(0, 360), 0f, Space.Self);

        //Set the player, controller and parent of the obstacle

        instantiatedObstacle.GetComponent<ObstacleController>().SetPlayer(m_PlayerCollider);
        instantiatedObstacle.GetComponent<ObstacleController>().SetMainController(m_GameController);

        instantiatedObstacle.transform.SetParent(transform);
    }



    private void OnTriggerEnter(Collider other)
    {
        //When the player enters the trigger, which is after the end of the vessel, instantiate a new vessel and destroy this vessel
        if ((other == m_PlayerCollider))
        { 
            Vector3 vesselPosition = new Vector3(transform.position.x + m_DistanceBetweenVessels, transform.position.y, transform.position.z);
            GameObject instantiatedVessel = Instantiate(m_NextVessel, vesselPosition, transform.rotation);
            Destroy(gameObject);
        }
    }


   

}
