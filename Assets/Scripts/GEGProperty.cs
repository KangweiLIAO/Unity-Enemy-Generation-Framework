using System;

namespace GEGFramework {

    [System.Serializable]
    public abstract class GEGProperty<T> {

        public T value;
        public T baseValue;

        public string name { get; set; }
        public bool enabled { get; set; }
        public bool porportion { get; set; }
        public float diffWeight { get; set; }

        /// <summary>
        /// Default constructor for GEGProperty<T>
        /// </summary>
        /// <param name="propName">Property name (e.g. health)</param>
        /// <param name="value"> Current stored value</param>
        /// <param name="baseValue"> Default property value</param>
        /// <param name="diffWeight">Range(0,10); How important is this property (prop) compared to other props in difficulty evaluation?</param>
        /// <param name="enabled">Include this property in difficulty evaluation?</param>
        /// <param name="porportion">Whether the property value should be proportional to the typeDiff</param>
        public GEGProperty(string propName, T value, T baseValue, float diffWeight = 0f,
                           bool enabled = false, bool porportion = true) {
            name = propName;
            this.value = value;
            this.baseValue = baseValue;
            this.diffWeight = diffWeight;
            this.enabled = enabled;
            this.porportion = porportion;
        }

        /// <summary>
        /// Update current value with new property values generated based on given difficulty level
        /// </summary>
        /// <param name="difficulty">Difficulty level (from 0 to 10)</param>
        /// <returns></returns>
        public virtual void Update(int difficulty) { // Allow for override
            switch (value, baseValue) {
                case (int val, int baseVal):
                    val = (int)(baseVal * diffWeight / 10 * difficulty / 10); // Will this.value be updated?
                    break;
                case (float val, float baseVal):
                    val = (float)baseVal * diffWeight / 10f * difficulty / 10f;
                    break;
                case (double val, double baseVal):
                    val = (double)baseVal * diffWeight / 10f * difficulty / 10f;
                    break;
                default: throw new Exception("Invalid type");
            }
        }

        public override string ToString() => $"{value}";
    }
}