using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_CountDown;
    [SerializeField]
    private Image m_BackgroundCounter;
    [SerializeField]
    private GameObject m_GamePlay;
    [SerializeField]
    private GameObject m_Guidance;

    private float m_WaitTime = 1f;
    private float m_FadeTime = 0.1f;


    private void Start()
    {
        m_GamePlay.SetActive(false);
    }


    public IEnumerator CountDown()
    {
        //Turn on the guidance instructions
        m_Guidance.SetActive(true);

        //Count down from 3, using a for loop

        for (int num = 3; num > 0; num--)
        {
            //Display the current number on the screen
            DisplayCountdownNumber(num);

            //Wait the wait time
            yield return new WaitForSecondsRealtime(m_WaitTime);

            //Hide the number and the counter
            m_CountDown.gameObject.SetActive(false);
            m_BackgroundCounter.enabled = false;

            //Give appearance of fading
            yield return new WaitForSecondsRealtime(m_FadeTime);
        }

        //Set the text to start to begin the game
        m_CountDown.SetText("Start");
        m_CountDown.gameObject.SetActive(true);


        //Wait the wait time
        yield return new WaitForSecondsRealtime(m_WaitTime);

        //Hide the number and the counter
        m_CountDown.gameObject.SetActive(false);
        m_BackgroundCounter.enabled = false;

        //Give appearance of fading
        yield return new WaitForSecondsRealtime(m_FadeTime);

        StartCoroutine(DisplayGameArea());

    }



    private void DisplayCountdownNumber(int num)
    {
        //Change the text in the middle of the countdown timer to the num to set to
        m_CountDown.SetText(num.ToString());

        //Change the colour of the circle
        Color colour;

        switch (num)
        {
            case 3:
                colour = Color.red;
                break;
            case 2:
                colour = Color.yellow;
                break;
            case 1:
                colour = Color.green;
                break;
            default:
                colour = Color.white;
                break;
        }

        //Make semi-transparent
        colour.a = 0.5f;

        m_BackgroundCounter.color = colour;


        //Enable the text and background
        m_CountDown.gameObject.SetActive(true);
        m_BackgroundCounter.enabled = true;
    }


    private IEnumerator DisplayGameArea()
    {
        //Start movement
        Time.timeScale = 1;

        //Display the game area
        m_GamePlay.SetActive(true);
        

        //Turn off the guidance after a few seconds
        yield return new WaitForSecondsRealtime(3*m_WaitTime);

        m_Guidance.SetActive(false);
    }
}
