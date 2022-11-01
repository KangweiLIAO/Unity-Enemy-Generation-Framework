using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;


public class EnemyDiff : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] List<Transform> enemySpawnPoints;
    [SerializeField] int minHealth = 10, maxHealth = 100;
    [SerializeField] int minSpeed = 1, maxSpeed = 10;
    [SerializeField] bool randomSpawn = false;
    [SerializeField] Dictionary<string, bool> PropertyList = new Dictionary<string, bool>();

    // Start is called before the first frame update
    void Start()
    {
        PropertyList.Add("health", true);
        PropertyList.Add("Speed", true);
        //Invoke("health", 2.0f);
        gameObject.SendMessage("health", 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        int difflevel = 10;
        List<List<double>> property = enemypropertyGenerator(difflevel, PropertyList);
        List<int> enemyNumbers = enemyNumberGenerator(difflevel, property);
        List<List<int>> genpositions = enemypositionsGenerator(enemyNumbers);
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            Debug.Log("enemyPrefabs " + i + " Number " + enemyNumbers[i] + ",hp " + property[i][0]* maxHealth + ",Speed " + property[i][1]* maxSpeed);
        }
        //object[] lists = { };
        //MethodInfo method = this.GetType().GetMethod("health");
        //method.Invoke(this, lists);


    }

    //Adjusts spawned enemies based on difficulty, now one enemy per difficulty factor
    List<int> enemyNumberGenerator(int difflevel, List<List<double>> property)
    {
        
        List<int> re = new List<int>();
        int t = difflevel;
        //Debug.Log("enemyPrefabs size " + enemyPrefabs.Count);
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            //Debug.Log("enemyPrefabs "+i);
            List<double> pro = property[i];
            double diff = calculatediff(pro);
            int y = Random.Range(0, t);
            int ts = Mathf.RoundToInt(enemyNumberStrategy(y) / (float)diff);
            //Debug.Log("enemy number " + ts);
            re.Add(ts);
            t = t - y;
        }
        //float sds = (float)calculatediff(property[(enemyPrefabs.Count - 1)]);
        //re.Add(Mathf.RoundToInt(enemyNumberStrategy(t) / sds));
        return re;
    }

    double calculatediff(List<double> property)
    {
        double re = 0;
        //Debug.Log(property.Count);
        //Debug.Log(property.Count);
        foreach (double ppe in property)
        {
            //Debug.Log(ppe);
            re = re + ppe;
        }
        return re;
    }


    //public List<double> enemysPropertyPrefab
    List<List<double>> enemypropertyGenerator(int difflevel, Dictionary<string, bool> dictionary)
    {
        List<List<double>> re = new List<List<double>>();
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            List<double> res = new List<double>();
            foreach (KeyValuePair<string, bool> kvp in dictionary)
            {
                if (kvp.Value)
                {
                    //exclude behaviour
                    //kvp.Key.Method();
                    Debug.Log(kvp.Key);
                    //double ra = Invoke(kvp.Key, 2.0f);
                    //object[] lists = { };
                    //MethodInfo addMethod = EnemyDiff.GetMethod("health");
                    //object addValue = addMethod.Invoke(null, new object[] { });
                    //typeof(EnemyDiff).GetMethod(kvp.Key).Invoke(null, lists);
                    double r = 0;
                    //double ras = (double)Invoke(kvp.Key, difflevel);
                    if (kvp.Key=="health")
                    {
                        r = 0.5;
                    } else if (kvp.Key == "Speed")
                    {
                        r = 0.8;
                    }
                    res.Add(r);
                }
            }
            re.Add(res);
        }
            
        //List<List<double>> property = enemypropertyGenerator(difflevel, dictionary);
        return re;
    }

    List<double> propertyAssign(int difficulty)
    {
        List<double> re = new List<double>();
        List<string> test = new List<string>(PropertyList.Keys);
        for (int i = 0; i < test.Count; i++)
        {
            double ds;
            switch (test[i])
            {
                case "health":
                    ds = difficulty * 20 + 10;
                    re.Add(ds);
                    break;
                case "Speed":
                    ds = difficulty * 0.1 + 1;
                    re.Add(ds);
                    break;
        
            }
        }
        return re;
    }

    double health(int s)
    {

        Debug.Log("health doing");
        double re = 0.5;
        return re;
    }

    double Speed(int s)
    {

        Debug.Log("Speed doing");
        double re = 0.8;
        return re;
    }



    int enemyNumberStrategy(int difflevel)
    {
        return difflevel;
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

    //todo list: span signal, model adapter property adjustment.

    //Adjusts spawned enemies based on difficulty, now one enemy per difficulty factor
    List<int> oldenemyNumberGenerator(int difflevel)
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
    void oldenemypropertyGenerator(int difflevel, List<int> enemys)
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
                Debug.Log("enemy type " + i + ", health: " + health + ", speed: " + Speed + ". location SpawnPoints" + di);
            }

        }
    }

}
