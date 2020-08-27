using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonControllerPauseGame : MonoBehaviour
{
    [SerializeField]
    private Button m_MainSceneButton, m_ResumeGameButton, m_RestartGameButton;
    [SerializeField]
    private int m_MainScene = 2, m_SameScene;
    [SerializeField]
    private GameController m_GameController;


    private void Start()
    {
        //Calls the StartGame/LearnGame methods when the buttons are clicked
        m_MainSceneButton.onClick.AddListener(ReturnToMain);
        m_RestartGameButton.onClick.AddListener(ReturnToMenu);
        m_ResumeGameButton.onClick.AddListener(ResumeGame);

    }


    private void ReturnToMain()
    {
        //Loads the new scene
        SceneManager.LoadScene(m_MainScene);
    }


    private void ReturnToMenu()
    {
        //Reloads the menu for the mini game the player is playing
        SceneManager.LoadScene(m_SameScene);
    }

    private void ResumeGame()
    {
        //Unpauses the game
        m_GameController.Unpause();
    }

}
