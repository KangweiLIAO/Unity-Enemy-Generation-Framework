using UnityEngine;
using GEGFramework;
using System.Collections.Generic;

namespace GEGFramework {
    public class GEGManager : MonoBehaviour {
        [SerializeField, Range(0, 10)] int defaultDiff; // prompt for default difficulty level for this scene
        [SerializeField] float spawnInterval;
        [SerializeField] bool randomSpawn;
        [SerializeField] List<Transform> enemySpawnPoints;
        [SerializeField] List<GEGCharacter> characters;

        static GEGPackedData packedData;
        static GEGDifficultyManager diffManager;
        static GEGUpdater updater;

        float spawnTimer;   // countdown timer for spawner
        float diffEvalTimer; // countdown timer for difficulty evaluation

        void Start() {
            updater = new GEGUpdater();
            packedData = new GEGPackedData(100f, 10f);
            spawnInterval = 3f;
            randomSpawn = true;
            diffEvalTimer = GEGPackedData.diffEvalInterval;
            diffManager = new GEGDifficultyManager(defaultDiff); // test values
            spawnTimer = spawnInterval; // init timer
        }

        void Update() {
            diffEvalTimer -= Time.deltaTime;
            spawnTimer -= Time.deltaTime; // timer countdown

            if (spawnTimer <= 0f) { // time to spawn a enemy
                // ...
                spawnTimer = spawnInterval; // reset spawn timer
            }

            if (diffEvalTimer <= 0) { // time to change difficulty
                // ...
            }
        }
    }
}
