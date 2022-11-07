using System.Collections.Generic;

public class GEGEnemyTypeData {
    static int totalInstThisWave { get; set; }
    static float baseFactor { get; set; }
    static float difficultyFactor { get; set; }
    static float speedPercent { get; set; }
    static float healthPercent { get; set; }
}


public class GEGPackedData {
    public static float currentPHealthPercent; // Current player's health percentage
    public static float scoreEvalInterval; // Time interval (in seconds) between each difficulty score evaluation
    public static float waveInterval; // Time interval (in seconds) between each wave
    public static List<GEGEnemyTypeData> enemyTypeData; // A list packed data for each type of enemy

    // Stores key-values <propertyName : (enabled?, proportion?, weight)>
    public static Dictionary<string, (bool, bool, float)> dictProperty;

    /// <summary>
    /// Default GEGPackedData constructor
    /// </summary>
    public GEGPackedData() {
        currentPHealthPercent = 1f;
        scoreEvalInterval = 10f;
        waveInterval = 100f;
        enemyTypeData = new List<GEGEnemyTypeData>();
        dictProperty = new Dictionary<string, (bool, bool, float)>() {
            {"PlayerHealth", (true, false, 1f)}, {"PlayerAPM", (false, false, 0f)},{"PlayerAccuracy",(false, false, 0f)},
            {"PlayerScore", (true, false, 1f)}, {"EnemyHealth", (false, false, 1f)}
        };
    }
}