using UnityEngine;
using Random = System.Random;

public class GetRandom : MonoBehaviour
{
    private Random m_Random = new Random();
    private int m_PreviousRandomNum = 0;


    public int GetRandomNum(int lowestNum, int highestNum)
    {
        //Returns a random number between the lowest and highest number which is different from the last num
        int randNum = m_PreviousRandomNum;
        while (randNum == m_PreviousRandomNum)
        {
            randNum = m_Random.Next(lowestNum, highestNum);
        }
        m_PreviousRandomNum = randNum;
        return randNum;

    }

    public Vector3 GetRandomPosition(float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
    {
        //Returns a random position in space
        float posX = UnityEngine.Random.Range(minX, maxX);
        float posY = UnityEngine.Random.Range(minY, maxY);
        float posZ = UnityEngine.Random.Range(minZ, maxZ);

        return new Vector3(posX, posY, posZ);
    }
}
