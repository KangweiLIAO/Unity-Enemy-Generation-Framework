using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour {
    [SerializeField] float spawnInterval = 3f;
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] List<Transform> enemySpawnPoints;
    [SerializeField] int minHealth = 10, maxHealth = 100;
    [SerializeField] int minSpeed = 1, maxSpeed = 10;
    [SerializeField] float attackSpeed = 1f;
    [SerializeField] bool randomSpawn = false;

    float spawnTimer; // timer for spawn interval

    // Start is called before the first frame update
    void Start() {
        spawnTimer = spawnInterval; // init timer
    }

    // Update is called once per frame
    void Update() {
        spawnTimer -= Time.deltaTime; // timer countdown
        if (spawnTimer <= 0f) {
			/*
            if (randomSpawn) {
                int randEnemy = Random.Range(0, enemyPrefabs.Count);
                int randSpawnPoint = Random.Range(0, enemySpawnPoints.Count);
                Instantiate(enemyPrefabs[randEnemy], enemySpawnPoints[randSpawnPoint].position, 
                    transform.rotation); // instantiate a random enemy at random position
            } else {
                Instantiate(enemyPrefabs[0], enemySpawnPoints[0].position, transform.rotation);
            }*/
			int difflevel = 10;
			List<int> ls = enemyNumberGenerator(difflevel);
			enemypropertyGenerator(difflevel, ls);
            spawnTimer = spawnInterval; // reset spawn timer
        }
		
		
    }
	
	

    List<int> enemyNumberGenerator(int difflevel)
    {
        List<int> re = new List<int>();
        int t = difflevel;
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            int y = Random.Range(0, t);
            re.Add(y);
            t = t - y;
        }

        return re;
    }

    void enemypropertyGenerator(int difflevel, List<int> enemys)
    {
        for (int i = 0; i < enemys.Count; i++)
        {

            int t = difflevel;
            int y = Random.Range(0, t);
            t = t - y;
            int health = PropertyGenStrategy(minHealth, maxHealth, y, 1);
            y = Random.Range(0, t);
            t = t - y;
            int Speed = PropertyGenStrategy(minSpeed, maxSpeed, y, 1);

            for (int j = 0; j < enemys[i]; j++)
            {
                int di = enemypositionGenerator();
                GameObject enemy = Instantiate(enemyPrefabs[i], enemySpawnPoints[di].position, transform.rotation);
                Debug.Log("enemy type "+ i + ", health: " + health +", speed: " + Speed + ". location SpawnPoints" + di);
            }

        }
    }

    int enemyNumberStrategy(int difflevel)
    {
        return difflevel;
    }


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
