using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ButtonControllerMenu : MonoBehaviour
{
    [SerializeField]
    private Button m_MainSceneButton, m_SameSceneButton;
    [SerializeField]
    private int m_MainScene = 2, m_SameScene = 15;


    private void Start()
    {
        //Calls the StartGame/LearnGame methods when the buttons are clicked
        m_MainSceneButton.onClick.AddListener(ReturnToMain);
        m_SameSceneButton.onClick.AddListener(ReturnToGame);

    }


    private void ReturnToMain()
    {
        //Loads the new scene
        SceneManager.LoadScene(m_MainScene);
    }
   

    private void ReturnToGame()
    {
        //Reloads the scene where the player just came from
        SceneManager.LoadScene(m_SameScene);
    }
}
