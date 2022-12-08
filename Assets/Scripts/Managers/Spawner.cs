using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GEGFramework {
    /// <summary>
    /// Integrated spawner of GEG framework
    /// </summary>
    class Spawner : MonoBehaviour {

        public static event Action<int> OnNewWaveStart;

        public bool Spawning { get; private set; }

        public static Spawner Instance { get; private set; } // singleton instance

        [TagSelector, SerializeField]
        string enemyTag; // tag for all enemies in the scene

        [SerializeField, Tooltip("Time interval between spawning two enemies (in seconds)")]
        float maxSpawnInterval;

        [SerializeField, Tooltip("Default spawning strategy (as random as possible)")]
        bool defaultSpawning;

        int waveCounter;
        int totalSpawn; // total number of enemies to spawn in this wave
        bool startSpawning;

        [SerializeField]
        PackedData spawnData = PackedData.Instance;

        private void Awake() {
            // Initialize singleton
            if (Instance != null && Instance != this) Destroy(this);
            else Instance = this;

            waveCounter = 0;
            totalSpawn = 0;
            Spawning = false;
            startSpawning = false;
        }

        void Update() {
            if (!HaveAliveEnemies() && !Spawning) {
                waveCounter++;
                startSpawning = true;
                OnNewWaveStart?.Invoke(waveCounter);
            }
        }

        void LateUpdate() { // start spawning in next frame after receiving signal
            if (startSpawning && !Spawning) {
                startSpawning = false;
                StartCoroutine(SpawnEnemies());
            }
        }

        /// <summary>
        /// Check if there is more than one enemy still alive in this wave
        /// </summary>
        /// <returns>True if there is more than one enemy alive</returns>
        public bool HaveAliveEnemies() {
            var enemies = GameObject.FindGameObjectsWithTag(enemyTag);
            if (enemies.Length == 0) return false;
            return true;
        }

        /// <summary>
        /// Coroutine for spawning a wave of enemies
        /// </summary>
        /// <returns></returns>
        IEnumerator SpawnEnemies() {
            Spawning = true;
            if (defaultSpawning) {
                List<(GameObject Prefab, int Num)> temp = new List<(GameObject, int)>();

                foreach (GEGCharacter c in PackedData.Instance.characters) {
                    if (c.numNextWave > 0 && c.type != CharacterType.Player) {
                        temp.Add((c.prefab, c.numNextWave)); // populate temp array
                        totalSpawn += c.numNextWave;
                    }
                }

                while (totalSpawn > 0) { // while temp is not empty
                    int randType = Random.Range(0, temp.Count);
                    float randSpawnInterval = Random.Range(0.5f, maxSpawnInterval);
                    int randPoint = Random.Range(0, PackedData.Instance.enemySpawnPoints.Count); // spawn at random spawn point

                    var obj = GameObject.Find(PackedData.Instance.enemySpawnPoints[randPoint]);
                    if (obj != null) {
                        var inst = Instantiate(temp[randType].Prefab, obj.transform.position,
                            temp[randType].Prefab.transform.rotation);
                        inst.tag = enemyTag;
                    } else {
                        Debug.LogError("Spawn point not found");
                    }
                    yield return new WaitForSeconds(randSpawnInterval);

                    if (temp[randType].Num - 1 > 0) // if character[i] has more instances to be spawned
                        temp[randType] = (temp[randType].Prefab, temp[randType].Num - 1);
                    else // else remove this type of character from the list
                        temp.RemoveAt(randType);
                    totalSpawn--;
                }
            }
            Spawning = false;
        }
    }
}