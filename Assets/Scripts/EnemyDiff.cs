using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDiff : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] List<Transform> enemySpawnPoints;
    [SerializeField] int minHealth = 10, maxHealth = 100;
    [SerializeField] int minSpeed = 1, maxSpeed = 10;
    [SerializeField] bool randomSpawn = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int difflevel = 10;
        List<int> ls = enemyNumberGenerator(difflevel);
        enemypropertyGenerator(difflevel, ls);
    }

   //Adjusts spawned enemies based on difficulty, now one enemy per difficulty factor
    List<int> enemyNumberGenerator(int difflevel)
    {
        List<int> re = new List<int>();
        int t = difflevel;
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            int y = Random.Range(0, t);
            re.Add(enemyNumberStrategy(y));
            t = t - y;
        }

        return re;
    }


    void enemypropertyGenerator(int difflevel, List<int> enemys)
    {
        for (int i = 0; i < enemys.Count; i++)
        {

            int t = difflevel;//Difficulty factor
            int y = Random.Range(0, t);
            t = t - y;//Difficulty factor remaining
            int health = PropertyGenStrategy(minHealth, maxHealth, y, 1);//using strategy adjustnment health
            y = Random.Range(0, t);
            t = t - y;//Difficulty factor remaining
            int Speed = PropertyGenStrategy(minSpeed, maxSpeed, y, 1);//using strategy adjustnment speed

            for (int j = 0; j < enemys[i]; j++)
            {
                int di = enemypositionGenerator();
                //GameObject enemy = Instantiate(enemyPrefabs[i], enemySpawnPoints[di].position, transform.rotation);//return enemy object
                Debug.Log("enemy type "+ i + ", health: " + health +", speed: " + Speed + ". location SpawnPoints" + di);
            }

        }
    }
    
    int enemyNumberStrategy(int difflevel)
    {
        return difflevel;
    }

    // return Enemy Spawn Point from list randomly, or 0 point.could extend more strategies.
    int enemypositionGenerator()
    {

        if (randomSpawn)
        {
            return Random.Range(0, enemySpawnPoints.Count);
        }
        else
        {
            return 0;
        }
    }
    //Strategy of Enemy Property adjustment, now only increase the value proportionally according to the difficulty
    int PropertyGenStrategy(int min, int max, int ranseed, int stepin)
    {
        if (min + stepin * ranseed < max)
        {
            return min + stepin * ranseed;
        }
        else
        {
            return max;
        }

    }
}
