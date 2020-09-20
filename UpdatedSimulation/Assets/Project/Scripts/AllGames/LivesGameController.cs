using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LivesGameController : MiniGameController
{
    [SerializeField]
    private Image m_LivesFull;
    [SerializeField]
    private Image m_LivesHalf;
    [SerializeField]
    private Image m_LivesAlmostGone;
    [SerializeField]
    private GameObject m_Pointer;

    private static int m_LivesLeft = 3;


    protected override void SetUpUI()
    {
        //Enables the correct images at the start of the game
        ResetLives();

        m_LivesFull.enabled = true;
        m_LivesHalf.enabled = true;
        m_LivesAlmostGone.enabled = true;

        base.SetUpUI();
    }


    protected override void Update()
    {
        //Show the correct images for the number of lives left
        ShowCorrectImages();

        base.Update();
    }



    protected void ResetLives()
    {
        //Ensure that every game starts with 3 lives
        m_LivesLeft = 3;
    }



    protected void ShowCorrectImages()
    {

        ////Sets the correct image for the lives by turning off top images
        if (m_LivesLeft == 2)
            m_LivesFull.enabled = false;
        else if (m_LivesLeft == 1)
            m_LivesHalf.enabled = false;
    }




    public void LoseLife()
    {
        //Decrease the number of lives - if no lives left the game is lost (but there is only a win game option in the parent class which acts the same)
        m_LivesLeft--;

        if (m_LivesLeft == 0)
        {
            base.WinGame();
        }

    }



    public int GetLivesLeft()
    {
        //Lives left getter
        return m_LivesLeft;
    }



    protected override void PauseGame()
    {
        //Makes the pointer appear during the UI elements (game paused)
        m_Pointer.SetActive(true);
        base.PauseGame();
    }


    public override void Unpause()
    {
        //Makes the pointer disappear during game play 
        m_Pointer.SetActive(false);

        base.Unpause();
    }

}
