using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GEGFramework
{
    /// <summary>
    /// Update enemy numbers, attributes and locations based on difficulty level
    /// </summary>
    public class GEGLevelController
    {
        List<Transform> enemySpawnPoints;
        bool randomSpawn = true;

        public GEGLevelController()
        {
        }

        //List<GEGTypeContainer> enemyTypeData;// = packedData.enemyTypeData;
        /// <summary>
        /// an example to run.
        /// </summary>
        /// <param name="difflevel">Difficulty level (from 0 to 10)</param>
        /// <returns></returns>
        public List<int> RunExample(GEGPackedData data, int diffLevel)
        {
            List<int> enemys = EnemyNumberGenerator(GEGPackedData.enemyTypeData, diffLevel);
            EnemyPropertyGenerator(diffLevel, GEGPackedData.enemyTypeData);
            return enemys;
        }

        /// <summary>
        /// Generate enemy properties.
        /// </summary>
        /// <param name="difflevel">Difficulty level (from 0 to 10)</param>
        /// <param name="PropertyList">Dictionary contains all attributes with enable or not</param>
        /// <returns></returns>
        void EnemyPropertyGenerator(int difflevel, List<GEGTypeContainer> enemyTypeData)
        {
            for (int i = 0; i < enemyTypeData.Count; i++)
            {
                foreach (GEGProperty<double> kvp in enemyTypeData[i].defaultProperty)
                {
                    if (kvp.enabled)
                    {
                        kvp.Update(difflevel);
                    }
                }
            }
        }

        /// <summary>
        /// Helper functions, computational purposes
        /// </summary>
        /// <returns></returns>
        private double EnemyNumberCal(float difficultyEnem, int difficulty, float baseValue, bool v)
        {
            if (v)
            {
                return difficultyEnem * difficulty / baseValue;
            }
            else
            {
                return difficultyEnem * baseValue * difficulty;
            }
        }

        /// <summary>
        /// Generate enemy number.
        /// </summary>
        /// <param name="difflevel">Difficulty level (from 0 to 10)</param>
        /// <returns></returns>
        List<int> EnemyNumberGenerator(List<GEGTypeContainer> enemies, int difflevel)
        {
            enemies.Sort((a, b) => a.diffFactor.CompareTo(b.diffFactor));//sort enemy by diffFactor
            List<int> re = new List<int>();
            int t = Mathf.RoundToInt(enemies.Count * difflevel / 10);
            for (int i = 0; i < t; i++)
            {
                int ts = (int)EnemyNumberCal(enemies[i].diffFactor, difflevel, enemies[i].diffFactor, true);
                re.Add(ts);
            }
            return re;
        }


    }

}