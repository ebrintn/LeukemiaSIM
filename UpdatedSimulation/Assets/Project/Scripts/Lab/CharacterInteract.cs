using System.Collections;
using UnityEngine;
using UnityEngine.Video;


public class CharacterInteract : MonoBehaviour
{

    [SerializeField]
    private LabController m_Controller;

    [SerializeField]
    private GameObject m_CharacterUI, m_CountUI, m_PlayGameButton;

    [SerializeField]
    private bool m_HasMiniGame = false;

    [SerializeField]
    private int m_MiniGameSceneNum;

    [SerializeField]
    private VideoClip m_Clip;

    [SerializeField]
    private VideoPlayer m_VideoPlayer;

    private Material[] m_Materials;
    



    private void Start()
    {
        //Set up the materials variable with the materials from each character (the children of the transform)
        m_Materials = transform.GetChild(0).GetChild(1).GetComponent<Renderer>().materials;

        //Turn off character UI if it is on
        if (m_CharacterUI.activeSelf) m_CharacterUI.SetActive(false);
    }


    public void Highlight()
    {
        //Make each material green
        foreach (Material mat in m_Materials)
        {
            mat.EnableKeyword("_EMISSION");
        }

        //Check to see if the button for interaction has been pressed
        CheckForInput();
    }

    public void StopHighlight()
    {
        //Turn off green highlight
        foreach (Material mat in m_Materials)
        {
            mat.DisableKeyword("_EMISSION");
        }
    }


    private void CheckForInput()
    {
        //Check to see if input is given - if it is display the character information UI
        if (m_Controller.GetUserInput())
        {
            StartCoroutine(ChangeUI());
            

        }
    }

    private IEnumerator ChangeUI()
    {
        //Set the appropriate clip
        m_VideoPlayer.clip = m_Clip;

        m_Controller.ReattachUI();

        //Wait until video clip is changed
        yield return new WaitForSeconds(0.5f);
        
        //Make the UI active after clip is playing
        m_CharacterUI.SetActive(true);
        m_CountUI.SetActive(true);

        if (m_HasMiniGame) m_PlayGameButton.SetActive(true);


        //Get controller to update UI
        m_Controller.UpdateUI(gameObject.name, m_MiniGameSceneNum);
    }


}
