using System.Collections.Generic;
using UnityEngine;

namespace GEGFramework {
    /// <summary>
    /// Packed data for GEG Framework.
    /// </summary>
    [System.Serializable]
    public class PackedData {
        static PackedData _instance = null;
        public static PackedData Instance {
            get {
                if (_instance == null) {
                    _instance = new PackedData();
                }
                return _instance;
            }
        }

        [SerializeField] public List<string> enemySpawnPoints;
        [SerializeField] public List<GEGCharacter> characters;

        public PackedData() {
            enemySpawnPoints = new List<string>();
            characters = new List<GEGCharacter>();
        }
    }
}