using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SlideDisplayController : MonoBehaviour
{
    [SerializeField]
    private Button m_ExitButton;
    [SerializeField]
    private Transform m_Camera;


    private void Start()
    {
        //Set up event listners
        m_ExitButton.onClick.AddListener(ExitScene);

    }


    private void ExitScene()
    {
        //Return to the main scene after zoom out
        SceneManager.LoadScene(0);
    }
}
