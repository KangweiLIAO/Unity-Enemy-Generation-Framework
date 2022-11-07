using UnityEngine;

/// <summary>
/// Responsible for computing a new difficulty level (score)
/// </summary>
public class GEGScoreManager
{
    // Q: if one function works, why declaring these class variables?
    // A: 因为这些类变量只要实例在他们就能被存住，因为一次调用方法得用上一次的这些数据，
    // 比如这一次调用得访问上一次的prevPlayerHealth，这个就得是类变量吧。还有就是像maxPlayerHealth这样的，
    // 初始化一次存为类变量就完事了，放在function里面还得重复输入很多次吧。

    public int currDiff; // Current difficulty level 0 - 9
    private int prevPlayerHealth; // Player's health in previous score computation; ISSUE: health should be float
    private int maxPlayerHealth; // Maximum player health; ISSUE: health should be float
    private float passedTime; // Q: Passed seconds since last score computation? If not, shouldn't named passedTime
    // A: passedTime指的是进入某个difficulty范围（比如>=8, ==0, >0 &&<7）以后的时间，方便之后改变难度
    // 比如在简单难度呆一定时间就提高到高难度。

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
    /// A: 这个是更新难度的间隔，
    /// 我想的是因为更新难度的间隔是固定的。所以用这个来计算passedTime。
    /// 比如如果在同一difficulty范围，可以直接累加时间passedTime += updateDiffInterval
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
