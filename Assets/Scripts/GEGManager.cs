using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GEGManager : MonoBehaviour {
    // public UnityEvent onPlayerKilled;
    // public UnityEvent onEnemyKilled;
    public UnityEvent onDifficultyChanged; // triggered when new difficulty level is computed

    GEGPackedData packedData;
    GEGScoreManager scoreManager;
    [SerializeField] GEGLevelController controller; // Not good...

    [SerializeField, Range(0, 10)] int defaultDifficulty; // prompt for default difficulty level for this scene

    float diffEvalTimer; // countdown timer for difficulty evaluation
    int lastDiffLevel, newDiffLevel;

    void Start() {
        packedData = new GEGPackedData();
        diffEvalTimer = GEGPackedData.scoreEvalInterval;

        lastDiffLevel = defaultDifficulty; // test value
        newDiffLevel = lastDiffLevel;
        scoreManager = new GEGScoreManager(lastDiffLevel, 100); // test values
    }

    void Update() {
        diffEvalTimer -= Time.deltaTime;
        if (diffEvalTimer <= 0) {
            newDiffLevel = scoreManager.GetDifficulty(50, 3f, 3f, 3f, 3f); // test values
            //controller.UpdateProperties(newDiffLevel, packedData);
            onDifficultyChanged.Invoke();
        }
    }
}
