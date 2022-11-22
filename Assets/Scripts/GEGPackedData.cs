using System.Collections.Generic;
using UnityEngine;

namespace GEGFramework {
    /// <summary>
    /// Packed data for GEG Framework.
    /// </summary>
    [System.Serializable]
    class GEGPackedData {
        public static bool randomSpawn;
        public static float waveInterval; // Time interval (in seconds) between each wave
        public static float diffEvalInterval; // Time interval (in seconds) between each difficulty score evaluation
        public static List<Transform> enemySpawnPoints;
        public static List<GEGCharacter> characters; // Data of each type of character (in GEGCharacterInst type)

        /// <summary>
        /// Default GEGPackedData constructor
        /// </summary>
        public GEGPackedData(float waveInterval, float diffEvalInterval) {
            GEGPackedData.waveInterval = waveInterval;
            GEGPackedData.diffEvalInterval = diffEvalInterval;
            characters = new List<GEGCharacter>();
        }

        public void AddTestData() {

        }
    }
}