using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;


public class MainErythrocyteGameController : MiniGameController
{
    [SerializeField]
    private Rigidbody m_PlayerRB;
    [SerializeField]
    private Transform m_BloodCell;
    [SerializeField]
    private VRPlayerMovement m_Movement;
    [SerializeField]
    private TextMeshProUGUI m_Timer;

    [SerializeField]
    private Transform[] m_OxygenPositions;
    private Transform[] m_OxygenOnCell = new Transform[4];

    [SerializeField]
    private Image[] m_FullOxygenImages;

    [SerializeField]
    private GameObject[] m_OxygensEasyHard;

    [SerializeField]
    private Transform m_WinningPosition;

    [SerializeField]
    private GameObject m_Pointer;

    private int m_OxygenCollected = 0;
    private int m_OxygenAttachedToHemoglobin = 4;


    private float m_BaseScore = 50f;
    private float m_BaseSpeed = 0f;



   

    protected override void Start()
    {

        base.Start();

        Physics.gravity = new Vector3(0, -5f, 0);



        m_BloodCell.transform.SetParent(base.GetPlayer().transform);
        //Set the blood cell right below the player camera
        m_BloodCell.localPosition= new Vector3 (base.GetHead().x, m_BloodCell.localPosition.y, base.GetHead().z );

    }


    public override void StartGame(int level = 0)
    {

        //Turn on the right oxygens for the level, turn off the other one
        foreach(GameObject oxygen in m_OxygensEasyHard)
        {
            oxygen.SetActive(false);
        }
        m_OxygensEasyHard[level].SetActive(true);


        //Set the base speed according to the level
        switch (level)
        {
            case 0:
                m_BaseSpeed = 4f;
                break;
            case 1:
                m_BaseSpeed = 2.5f;
                break;
        }


        //Ensure that the player is travelling at the right speed according to their level choice
        m_Movement.StartGame((float)level);

        base.StartGame(level);
    }



    protected override void Update()
    {
        //If the game is being played make the blood cell visible and the game foggy
        if (base.IsPlaying())
            m_BloodCell.gameObject.SetActive(true);
        else
            m_BloodCell.gameObject.SetActive(false);


        //Set up timer for the UI
        m_Timer.SetText("Time: " + GetComponent<Timer>().GetTime());


        //Check to see if the blood cell has flipped over
        if (m_PlayerRB.transform.rotation.eulerAngles.z < 180 && 
            m_PlayerRB.transform.rotation.eulerAngles.z >30)
            StartCoroutine(CheckFlip());

        //Update base first because images are different
        base.Update();


    }
        
   

    public void IncreaseOxygen(Transform oxygen)
    {
        //Unfade the oxygen image for the correct oxygen
        m_FullOxygenImages[m_OxygenCollected / 4].fillAmount += 0.25f;

        //Called when an oxygen is destroyed. Increase the oxygen variable.
        m_OxygenCollected++;

        //Position the oxtygen on the hemoglobin surf board
        PositionOxygen(oxygen);

        //If the oxygen fills a hemoglobin reset which oxygen are there
        if (m_OxygenCollected % 4 == 0)
            StartCoroutine(ResetOxygen());


    }


    private void PositionOxygen(Transform oxygen)
    {

        //Assign the oxygen to the correct location in the array
        m_OxygenOnCell[m_OxygenCollected % 4] = oxygen;

        //Find which position on the transform that the oxygen must be moved to
        int positionInArray = m_OxygenCollected % 4;

        //Move the oxygen to the appropriate position on the surf board
        oxygen.position = m_OxygenPositions[positionInArray].position;

        //Resize the oxygen
        oxygen.localScale = new Vector3(0.5f, 0.5f, 0.5f);


        //Assign the oxygen's parent so that it moves with the surf board
        oxygen.parent = m_BloodCell;
    }


    private IEnumerator ResetOxygen()
    {



        yield return new WaitForSeconds(2f);

        //Delete the oxygen that is currently in the oxygen on cell array
        for (int pos = 0; pos < m_OxygenOnCell.Length; pos++)
        {
            Destroy(m_OxygenOnCell[pos].gameObject);
            m_OxygenOnCell[pos] = null;

        }

    }

    public override void WinGame(string scoreText = "WinningScore")
    {
        //Calculate the score of the player

        //Start with a baseline score
        float score = m_BaseScore;

        //Adjust score according to the player speed
        float speedAdjustment =   m_BaseSpeed - GetComponent<Timer>().GetFloatTime();
        score = score + speedAdjustment;



        //Add points for the oxygen collected
        score = score + (m_OxygenCollected * 10f);


        //Display score and high scores
        scoreText = score.ToString();


        //Move the player to the winning position
        m_PlayerRB.transform.position = m_WinningPosition.position;
        m_PlayerRB.transform.rotation = Quaternion.Euler(0,
            m_PlayerRB.transform.rotation.eulerAngles.y, 0);

        base.WinGame(scoreText);
    }



    private IEnumerator CheckFlip()
    {

        //Delay to make sure that the flip is not temporary
        yield return new WaitForSeconds(2f);

        

        //If the flip is not temporary, give no base score because it is not a win and 
        //then finish the game
        if (m_PlayerRB.transform.rotation.eulerAngles.z < 180 &&
            m_PlayerRB.transform.rotation.eulerAngles.z > 30)
        {
            m_BaseScore = 0;
            m_BaseSpeed = GetComponent<Timer>().GetFloatTime();

            

            WinGame();
        }


        

    }

    protected override void PauseGame()
    {
        //Turn on the pointer when paused
        m_Pointer.SetActive(true);

        base.PauseGame();
    }



    public override void Unpause()
    {
        //Turn on the pointer when paused
        m_Pointer.SetActive(false);

        base.Unpause();
    }

}
