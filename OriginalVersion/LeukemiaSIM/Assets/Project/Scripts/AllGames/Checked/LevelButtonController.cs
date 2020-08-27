using UnityEngine;
using UnityEngine.UI;

public class LevelButtonController : MonoBehaviour
{
    [SerializeField]
    private Button m_EasyButton, m_MediumButton, m_HardButton;

    [SerializeField]
    private int m_NumberObjectsEasy, m_NumberObjectsMedium, m_NumberObjectsHard;

    [SerializeField]
    private MiniGameController m_MainGameController;



    void Start()
    {
        //Adds event listiners that call respective methods when the buttons are pushed
        m_EasyButton.onClick.AddListener(EasyStart);
        m_MediumButton.onClick.AddListener(MediumStart);
        m_HardButton.onClick.AddListener(HardStart);
    }

   

    private void EasyStart()
    {
        //Starts the game with the number of objects for easy
        m_MainGameController.StartGame(m_NumberObjectsEasy);
    }


    private void MediumStart()
    {
        //Starts the game with the number of objects for medium
        m_MainGameController.StartGame(m_NumberObjectsMedium);
    }


    private void HardStart()
    {
        //Starts the game with number of objects for hard
        m_MainGameController.StartGame(m_NumberObjectsHard);
    }

}
