using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButtonController : GameButtonController
{
    [SerializeField]
    private Button m_IncreaseDifficultyButton, m_DecreaseDifficultyButton;
    [SerializeField]
    private TextMeshProUGUI m_LevelLabel;


    [SerializeField]
    private int[] m_NumEachLevel;
    [SerializeField]
    private string[] m_LevelNames;

    private int m_CurrentLevel = 0;




    protected override void Start()
    {
        //Start with the correct UI
        UpdateUI();

        //Adds event listiners that call respective methods when the buttons are pushed
        m_IncreaseDifficultyButton.onClick.AddListener(IncreaseDifficulty);
        m_DecreaseDifficultyButton.onClick.AddListener(DecreaseDifficulty);

        //Run base commands
        base.Start();
    }

    private void IncreaseDifficulty()
    {
        //Increase the level
        m_CurrentLevel++;

        //Update UI
        UpdateUI();

    }


    private void DecreaseDifficulty()
    {
        //Decrease the level
        m_CurrentLevel--;

        //Update UI
        UpdateUI();
    }


    private void UpdateUI()
    {
        //Ensure that the level label is set to the current level
        m_LevelLabel.SetText(m_LevelNames[m_CurrentLevel]);

        //Disable the buttons that cannot be pressed because highest or lowest level reached
        if (m_CurrentLevel == 0)
            m_DecreaseDifficultyButton.interactable = false;
        else
            m_DecreaseDifficultyButton.interactable = true;

        if (m_CurrentLevel == m_LevelNames.Length - 1)
            m_IncreaseDifficultyButton.interactable = false;
        else
            m_IncreaseDifficultyButton.interactable = true;
    }


    protected override void StartGame()
    {
        //Starts the game using the main controller
        m_MainGameController.StartGame(m_NumEachLevel[m_CurrentLevel]);
    }


}
