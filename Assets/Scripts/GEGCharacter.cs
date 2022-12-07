using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace GEGFramework {
    [CreateAssetMenu(fileName = "GEGCharacter", menuName = "GEG Framework/Character")]
    public class GEGCharacter : ScriptableObject {
        public GameObject prefab;
        public CharacterType type;
        public int numNextWave; // number of instances to spawn in next wave
        public List<GEGProperty> properties;

        public GEGProperty this[string key] {
            get => properties.Find(item => item.propertyName == key);
        }
    }
}