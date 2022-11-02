using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;


public class EnemyDiff : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] List<Transform> enemySpawnPoints;
    [SerializeField] Dictionary<string, bool> PropertyList = new Dictionary<string, bool>();
    [SerializeField] bool randomSpawn = false;
    [SerializeField] List<double> difficultyHP;
    [SerializeField] List<double> Enemydifficulty;
    [SerializeField] List<double> difficultySpeed;
    [SerializeField] List<double> difficultyAttackRate;
    [SerializeField] List<double> baseHP;
    [SerializeField] List<double> baseSpeed;
    [SerializeField] List<double> baseEnemyNumber;
    [SerializeField] List<double> baseAttackRate;

    // Start is called before the first frame update
    void Start()
    {
        PropertyList.Add("health", true);
        PropertyList.Add("Speed", true);
        //Invoke("health", 2.0f);
        //gameObject.SendMessage("health", 2.0f);
    }

    // Update is called once per frame
    void Update()
    {

        int difflevel = 6;
        List<int> enemys = enemyNumberGenerator(difflevel);
        List<int> enemyproperty = enemypropertyGenerator(difflevel, PropertyList);
        List < List<int> > position  = enemypositionsGenerator(enemys);

    }

    void todoproperty(int difflevel)
    {
        List<double> hp = todoHP(difflevel);
        List<double> Speed = todoSpeed(difflevel);
        List<double> AttackRate = todoAttackRate(difflevel);
    }


    List<double> todoHP(int difflevel)
    {
        List<double> re = new List<double>();
        int enemyNumber = enemyPrefabs.Count;
        for (int i = 0; i < enemyNumber; i++)
        {
            re.Add(PropertyCal(difficultyHP[i], difflevel, baseHP[i],false));
        }
        return re;
    }


    List<double> todoSpeed(int difflevel)
    {
        List<double> re = new List<double>();
        int enemyNumber = enemyPrefabs.Count;
        for (int i = 0; i < enemyNumber; i++)
        {
            re.Add(PropertyCal(difficultySpeed[i], difflevel, baseAttackRate[i], false));
        }
        return re;

    }


    List<double> todoAttackRate(int difflevel)
    {
        List<double> re = new List<double>();
        int enemyNumber = enemyPrefabs.Count;
        for (int i = 0; i < enemyNumber; i++)
        {
            re.Add(PropertyCal(difficultyAttackRate[i], difflevel, baseHP[i], false));
        }
        return re;

    }


    double PropertyCal(double difficultyEnem, int difficulty, double baseValue, bool v)
    {
        double re;
        if (v)
        {
            re = difficultyEnem * difficulty / baseValue;
        }
        else
        {
            re = difficultyEnem * baseValue * difficulty;
        }
        return re;
    }

    double enemyNumberCal(double difficultyEnem, int difficulty, double baseValue, bool v)
    {
        double re;
        if (v)
        {
            re = difficultyEnem * difficulty / baseValue;
        }
        else
        {
            re = difficultyEnem * baseValue * difficulty;
        }
        return re;
    }

    int enemyPercentage(int difflevel)
    {
        return Mathf.RoundToInt(enemyPrefabs.Count * difflevel / 10);
    }

    List<int> enemyNumberGenerator(int difflevel)
    {
        List<int> re = new List<int>();
        int t = enemyPercentage(difflevel);
        for (int i = 0; i < t; i++)
        {
            int ts = (int)enemyNumberCal(baseEnemyNumber[i], difflevel, Enemydifficulty[i], true);
            re.Add(ts);
        }
        return re;
    }

    List<int> enemypropertyGenerator(int difflevel, Dictionary<string, bool> PropertyList)
    {
        List<int> re = new List<int>();
        int enemyNumber = enemyPrefabs.Count;
        foreach (KeyValuePair<string, bool> kvp in PropertyList)
        {
            if (kvp.Value)
            {
                if (kvp.Key == "health")
                {
                    todoHP(difflevel);
                } 
                else if (kvp.Key == "health")
                {
                    todoSpeed(difflevel);
                }
                else if (kvp.Key == "health")
                {
                    todoAttackRate(difflevel);
                }
            }
        }
        return re;
    }
    List<List<int>> enemypositionsGenerator(List<int> enemys)
    {
        List<List<int>> res = new List<List<int>>();
        for (int i = 0; i < enemys.Count; i++)
        {
            List<int> re = new List<int>();
            for (int j = 0; j < enemys[i]; j++)
            {
                re.Add(enemypositionGenerator());
            }
            res.Add(re);

        }
        return res;
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

}
