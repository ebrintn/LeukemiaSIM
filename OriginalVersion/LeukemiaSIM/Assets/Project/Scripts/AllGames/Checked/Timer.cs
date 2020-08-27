using UnityEngine;

public class Timer:MonoBehaviour
{

    private float m_TimeAtStart = 0;
    private float m_CurrentTime;



    private void FindCurrentTime()
    {
        //Get the time subtracted from the current time
        m_CurrentTime = Time.timeSinceLevelLoad;
    }

    private int FindMin()
    {
        //Find the minute time of the current time
        return (int)(m_CurrentTime / 60);
    }


    private int FindSec()
    {
        //Find the second time of the current time
        return (int)(m_CurrentTime % 60);
    }


    public string GetTime()
    {
        //Gets the time since the player started playing in seconds and minutes
        FindCurrentTime();
        int minTime = FindMin();
        int secTime = FindSec();

        //Write out the time in a clocklike format, using minutes and seconds
        string timeString = string.Format("{0}:{1}", minTime.ToString("D2"), secTime.ToString("D2"));
        return timeString;
    }


}
