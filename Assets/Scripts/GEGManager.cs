using UnityEngine;
using GEGFramework;
using UnityEngine.Events;

public class GEGManager : MonoBehaviour {
    // public UnityEvent onPlayerKilled;
    // public UnityEvent onEnemyKilled;
    public UnityEvent onDifficultyChanged; // triggered when new difficulty level is computed

    static GEGPackedData packedData;
    static GEGScoreManager scoreManager;

    [SerializeField] GEGLevelController controller; // Not good...
    [SerializeField, Range(0, 10)] int defaultDifficulty; // prompt for default difficulty level for this scene

    float diffEvalTimer; // countdown timer for difficulty evaluation
    int lastDiffLevel, newDiffLevel;

    void Start() {
        packedData = new GEGPackedData();
        diffEvalTimer = GEGPackedData.diffEvalInterval;

        lastDiffLevel = defaultDifficulty; // test value
        newDiffLevel = lastDiffLevel;
        scoreManager = new GEGScoreManager(5); // test values
    }

    void Update() {
        diffEvalTimer -= Time.deltaTime;
        if (diffEvalTimer <= 0) {
            //newDiffLevel = scoreManager.GetDifficulty(50, 3f, 3f, 3f, 3f); // test values
            //controller.UpdateProperties(newDiffLevel, packedData);
            onDifficultyChanged.Invoke();
        }
    }
}
