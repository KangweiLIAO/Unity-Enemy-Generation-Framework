using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace GEGFramework
{
    [CreateAssetMenu(fileName = "GEGWave", menuName = "GEG Framework/GEGWave")]
    public class GEGWave : ScriptableObject
    {
        public float keepTime;
        public int numKeepTurn; // number of instances to spawn in next wave
        public List<GEGUpdateCharacter> characters;


    }
}