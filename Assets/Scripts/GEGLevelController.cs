using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GEGLevelController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="propertyList">Specifies which property the developer wants to modify</param>
    /// <param name="gameDiff">The desired game difficulty for now</param>
    /// <param name="typeDiff">A ranking of enemy types according to their difficulty</param>
    /// <param name="dictBaseVals">Storing the base value for each property</param>
    /// <param name="dictPropotion">Stores a bool indicating whether the property value should 
    ///                             be proportional to the typeDiff</param>
    /// <returns>Property values</returns>
    /// 
    /// Q: Why is the dictBaseVals for each property not for each enemy type?
    /// A: 这个dictBaseVals是表明每个属性是多少的，
    ///    比如速度可能只有1-10，单血量有1-100，所以speed这个属性的base是*10，
    ///    health这个属性是*100.所以不是对每一个enemy的
    /// 
    /// 
    /// Q: 这个new constructor没懂，就比如dictBaseVals怎么传?
    ///public Dictionary<string, float[]> UpdateProperties(int gameDiff, GEGPackedData packedData) { // new constructor
    public Dictionary<string, float[]> UpdateProperties(int gameDiff, float[] typeDiff,
        Dictionary<string, bool> dictProperty, Dictionary<string, int> dictBaseVals, Dictionary<string, bool> dictPropotion)
    {

        //float[] typeDiff = new float[packedData.enemyTypeData.Length];
        //for (int i = 0; i < packedData.enemyTypeData.Length; ++i) {
        //    // Construct what you need below
        //}

        Dictionary<string, float[]> propertyValues = new Dictionary<string, float[]>();

        int numEnemyType = typeDiff.Length; // number of all enemy types
        gameDiff /= 10; // convert difficulty level to percentage

        // From endIndex to the end of array, the difficulty of those types should be 0 which means ignoring them
        // Should use Mathf.Ceil to round up
        int endIndex = (int)Mathf.Ceil(numEnemyType * gameDiff / 10); // Q: why not use gameDiffPercent? A: they are the same. both OK
        for (int i = endIndex; i < numEnemyType; i++)
        {
            typeDiff[i] = 0;
        }

        // Loop properties that need to be modified
        foreach (var item in dictProperty)
        {
            if (item.Value == true)
            {
                // The array containing values of the property for different types of enemies
                float[] values = new float[numEnemyType];

                if (dictPropotion[item.Key] == true)
                { // proportional [1,2,3,4,5] -> [1,2,3,4,5]
                    for (int i = 0; i < numEnemyType; i++)
                    {
                        values[i] = gameDiff * typeDiff[i] * dictBaseVals[item.Key];
                    }
                }
                else
                { // inversely proportional [1,2,3,4,5] -> [5,4,3,2,1]
                    for (int i = endIndex - 1, j = 0; i >= 0; i--, j++)
                    {
                        values[i] = gameDiff * typeDiff[j] * dictBaseVals[item.Key];
                    }
                }
                propertyValues[item.Key] = values;
            }
          
        }
        return propertyValues;
    }
}
