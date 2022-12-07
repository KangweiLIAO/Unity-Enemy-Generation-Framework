using System;
using UnityEngine;

namespace GEGFramework {
    [CreateAssetMenu(fileName = "Property", menuName = "GEG Framework/Character's Property")]
    public class GEGProperty : ScriptableObject {

        public string propertyName;
        [Tooltip("Whether this property should be enabled for difficulty adjustment")]
        public bool enabled;
        public float value;
        public float defaultValue;
        [Range(0, 100), Tooltip("The importance of this property in difficulty adjustment")]
        public float difficultyImportance;
        [Tooltip("Intensity adjustment scalar for this property adjustment (e.g., property " +
            "increase 1%, scalar = 5; intensity adjust 5%)")]
        public float intensityScalar;
        [Tooltip("Increasing this value will increase intensity or not")]
        public bool increaseIntensity;
    }
}