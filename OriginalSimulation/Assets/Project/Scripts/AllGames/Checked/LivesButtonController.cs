using UnityEngine;
using UnityEngine.UI;

public class LivesButtonController : MonoBehaviour
{
    [SerializeField]
    private Button m_PlayButton;
    [SerializeField]
    private LivesGameController m_MainGameController;

    void Start()
    {
        //Adds event listiners that call respective methods when the buttons are pushed
        m_PlayButton.onClick.AddListener(StartGame);
    }


    private void StartGame()
    {
        //Starts the game with the number of objects for easy
        m_MainGameController.StartGame();
    }
}



