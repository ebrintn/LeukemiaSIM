using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameButtonController : MonoBehaviour
{
    [SerializeField]
    private Button m_PlayButton, m_ReturnButton;


    [SerializeField]
    private int m_MainSceneNum = 2;

    [SerializeField]
    protected MiniGameController m_MainGameController;

    protected virtual void Start()
    {
        //Adds event listiners that call respective methods when the buttons are pushed
        m_PlayButton.onClick.AddListener(StartGame);
        m_ReturnButton.onClick.AddListener(ReturnToMain);
    }


    protected virtual void StartGame()
    {
        //Starts the game using the main controller
        m_MainGameController.StartGame();
    }


    private void ReturnToMain()
    {
        //Return to the main scene if the player presses the back button
        SceneManager.LoadScene(m_MainSceneNum);
    }
}



