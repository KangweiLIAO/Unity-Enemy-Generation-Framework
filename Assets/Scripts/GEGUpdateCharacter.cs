using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace GEGFramework
{
    [CreateAssetMenu(fileName = "GEGUpdateCharacter", menuName = "GEG Framework/GEGUpdateCharacter")]
    public class GEGUpdateCharacter : ScriptableObject
    {
        public int numberNextTurn; // number of instances to spawn in next wave
        public GEGCharacter characters;
        [System.Serializable]
        public class GEGWaveProperties
        {
            public GEGProperty property;
            public GEGProperty getproperty()
            {
                return property;
            }

            public float value;
            public float getvalue()
            {
                return value;
            }
        }
        public List<GEGWaveProperties> properties;

    }
}
