using UnityEngine;

/// <summary>
/// Responsible for computing a new difficulty level (score)
/// </summary>
public class GEGScoreManager
{
    // Q: if one function works, why declaring these class variables?
    // A: ��Ϊ��Щ�����ֻҪʵ�������Ǿ��ܱ���ס����Ϊһ�ε��÷���������һ�ε���Щ���ݣ�
    // ������һ�ε��õ÷�����һ�ε�prevPlayerHealth������͵���������ɡ����о�����maxPlayerHealth�����ģ�
    // ��ʼ��һ�δ�Ϊ������������ˣ�����function���滹���ظ�����ܶ�ΰɡ�

    public int currDiff; // Current difficulty level 0 - 9
    private int prevPlayerHealth; // Player's health in previous score computation; ISSUE: health should be float
    private int maxPlayerHealth; // Maximum player health; ISSUE: health should be float
    private float passedTime; // Q: Passed seconds since last score computation? If not, shouldn't named passedTime
    // A: passedTimeָ���ǽ���ĳ��difficulty��Χ������>=8, ==0, >0 &&<7���Ժ��ʱ�䣬����֮��ı��Ѷ�
    // �����ڼ��Ѷȴ�һ��ʱ�����ߵ����Ѷȡ�

    /// <summary>
    /// GEGScoreManager constructor
    /// </summary>
    /// <param name="initialDiff">Initial difficulty level</param>
    /// <param name="maxPlayerHealth">Maximum player health</param>
    public GEGScoreManager(int initialDiff, int maxPlayerHealth)
    {
        currDiff = initialDiff;
        prevPlayerHealth = maxPlayerHealth;
        this.maxPlayerHealth = maxPlayerHealth;
        passedTime = 0f;
    }

    /// <summary>
    /// Returns a difficulty level (score) based on the inputs
    /// </summary>
    /// <param name="currPlayerHealth">Current player health within range [0, maxPlayerHealth]</param>
    /// <param name="updateDiffInterval">Q: Not sure what is this?</param> 
    /// A: ����Ǹ����Ѷȵļ����
    /// ���������Ϊ�����Ѷȵļ���ǹ̶��ġ����������������passedTime��
    /// ���������ͬһdifficulty��Χ������ֱ���ۼ�ʱ��passedTime += updateDiffInterval
    /// <param name="peakInterval">Desired peak interval</param>
    /// <param name="lowDiffInterval">Desired low difficulty interval</param>
    /// <param name="zeroDiffInterval">Desired zero difficulty interval</param>
    /// <returns>New difficulty Level</returns>
    public int GetDifficulty(int currPlayerHealth, float updateDiffInterval, float peakInterval,
        float lowDiffInterval, float zeroDiffInterval)
    {
        // Cases for difficulty equals to 0
        if (currDiff == 0)
        {
            // Enough time to relax, so we set difficulty to very low level including 1, 2, 3. 
            if (passedTime >= zeroDiffInterval)
            {
                passedTime = 0;
                currDiff = Random.Range(1, 4);
            }

            // Not enough time to relax, so we continue 0 difficulty
            else
            {
                passedTime += updateDiffInterval;
            }
        }



        // Cases for low difficulty from 1 to 6
        else if (currDiff > 0 && currDiff < 7)
        {
            // Enough time for low level difficulty, so we switch to high level difficulty.
            if (passedTime > lowDiffInterval)
            {
                passedTime = 0;
                currDiff = 7;
            }
            // Not enough time in low level
            else
            {
                //Not reached the highest level for low difficulty yet, so we make the difficulty a little bit higher
                if (currDiff < 6)
                {
                    
                    currDiff++;
                }
                passedTime += updateDiffInterval;
            }

        }



        // Cases for high difficulty 7,8,9
        else if (currDiff >= 7)
        {
            if(passedTime > peakInterval || (currPlayerHealth / maxPlayerHealth) < 0.1 || (currPlayerHealth - prevPlayerHealth) / maxPlayerHealth > 0.5)
            {
                currDiff = 0;
                passedTime = 0;
            }
            else
            {
                // 1. The time is not long enough
                int continuePeakOrNot = Random.Range(0, 10);

                // 30% new difficulty = 0
                if (continuePeakOrNot >= 7)
                { 
                    currDiff = 0;
                    passedTime = 0;
                }

                // 70% increase difficulty
                else
                { 
                    if(currDiff < 9)
                    {
                        currDiff += 1;
                    }
                    passedTime += updateDiffInterval;

                }
            }
        }




        return currDiff;
    }
}
