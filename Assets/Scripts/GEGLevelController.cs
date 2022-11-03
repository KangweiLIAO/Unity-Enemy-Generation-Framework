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

    /*
     * PropertyList specifies which property the developer wants to modify.
     * gameDif is the desired game difficulty for now.
     * float[] typeDiff is the difficulty for each enemy type which should be ordered!!!!!!!
     * baseDict is a dictionary storing the base value for each property
     * propotionDict is also a dictionary storing if the property value should be propotional to the typeDiff
     * 
     */
    public Dictionary<string, float[]> updateProperties (Dictionary<string, bool> PropertyList, 
                                                        int gameDif, 
                                                        float[] typeDiff, 
                                                        Dictionary<string, int> baseDict, 
                                                        Dictionary<string, bool> propotionDict)
    {
        // Dictionary to be returned
        Dictionary<string, float[]> propertyValues = new Dictionary<string, float[]>();

        // Store the number of all enemy types
        int length = typeDiff.Length;

        // From endIndex to the end of array, the difficulty of those types should be 0 which means ignoring them
        // Should use Mathf.Ceil to round up
        int endIndex = (int) Mathf.Ceil(length * gameDif / 10);
        for (int i = endIndex; i < length; i++)
        {
            typeDiff[i] = 0;
        }


        
        // Loop properties that need to be modified
        foreach (var item in PropertyList)
        {
            if(item.Value == true)
            {
                // The array containing values of the property for different types of enemies
                float[] values = new float[length];

                // if proportional [1,2,3,4,5] -> [1,2,3,4,5]
                if (propotionDict[item.Key] == true)
                {
                    for (int i = 0; i < length; i++)
                    {
                        values[i] = typeDiff[i] * gameDif * baseDict[item.Key];
                    }
                }

                // if inversely proportional [1,2,3,4,5] -> [5,4,3,2,1]
                else
                {
                    for (int i = endIndex-1, j = 0;  i >= 0;  i--,j++)
                    {
                        values[i] = typeDiff[j] * gameDif * baseDict[item.Key];
                    }
                }
                
                propertyValues[item.Key] = values;
            }  

        }


        return propertyValues;
    }

}
