using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ZoomButton : MonoBehaviour
{
    [SerializeField]
    private Button m_ZoomInButton, m_ZoomOutButton;

    [SerializeField]
    private Texture[] m_ZoomTextures;

    [SerializeField]
    private Material m_MicroscopeView;

    [SerializeField]
    private GameObject m_ZoomOutObject;

    [SerializeField]
    private TextMeshProUGUI m_ZoomInText;

    [SerializeField]
    private int m_EnterSceneNum;

    private int m_ZoomNum=0;
    private bool m_CanMicroMe = false;

    private void Start()
    {
        //Calls the StartGame/LearnGame methods when the buttons are clicked
        m_ZoomInButton.onClick.AddListener(ZoomIn);
        m_ZoomOutButton.onClick.AddListener(ZoomOut);

        //Turn off the zoom out button
        m_ZoomOutObject.SetActive(false);

        SetMicroscopeView();
    }

    private void ZoomIn()
    {
        //Turn on the zoom out button
        m_ZoomOutObject.SetActive(true);

        //Increase the zoom num and change veiw
        m_ZoomNum++;
        SetMicroscopeView();

        //If the furthest zoom in, allow enter
        if (m_ZoomNum == m_ZoomTextures.Length - 1) AllowMicro();
        
    }

    private void ZoomOut()
    {
        //Zoomed out so allow enter no longer
        StopEnter();

        //Decrease the zoom num and change veiw
        m_ZoomNum--;
        SetMicroscopeView();

        //If the furthest zoom out, turn off zoom out button
        if (m_ZoomNum == 0) m_ZoomOutObject.SetActive(false);
    }


    private void SetMicroscopeView()
    {
        //Change the main veiw
        if (m_CanMicroMe) SceneManager.LoadScene(m_EnterSceneNum);
        else
        {
            m_MicroscopeView.mainTexture = m_ZoomTextures[m_ZoomNum];

            //Change the lighting veiw
            m_MicroscopeView.SetTexture("_EmissionMap", m_ZoomTextures[m_ZoomNum]);
        }
    }

    private void AllowMicro()
    {
        //Change the text to the appropriate text
        m_ZoomInText.SetText("Micro Me!");

        m_CanMicroMe = true;
    }

    private void StopEnter()
    {
        //Change the text to the appropriate text
        m_ZoomInText.SetText("Zoom In");

        m_CanMicroMe = false;
    }

    public void ResetZoom(Texture [] newZoomTextures, int newNum)
    {
        //Change the scene number when the player microme's
        m_EnterSceneNum = newNum;

        //Change the zoom textures to the textures being reset with
        m_ZoomTextures = newZoomTextures;

        //Zoom out to the furthest position
        m_ZoomNum = 0;
        m_ZoomOutObject.SetActive(false);
        SetMicroscopeView();
    }
}
