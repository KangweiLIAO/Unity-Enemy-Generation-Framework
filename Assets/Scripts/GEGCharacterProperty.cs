using System;
using UnityEngine;

namespace GEGFramework {
    public abstract class GEGCharacterProperty : ScriptableObject {

        public double value; // template value to generate the property value in each instance
        public double defaultValue;
        public string propName;
        public float importance;
        public bool diffEnabled;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="propName"></param>
        /// <param name="value"></param>
        /// <param name="enabled"></param>
        public GEGCharacterProperty(string propName, double value, float importance, bool enabled) {
            this.value = value;
            defaultValue = value;
            this.propName = propName;
            this.importance = importance;
            diffEnabled = enabled;
        }

        public override string ToString() => String.Format("{0}:\n\tcurrent value: {1}\n\tdiffWeight:{2}\n\tenabled:{3}",
            propName, value, importance, diffEnabled);
    }
}