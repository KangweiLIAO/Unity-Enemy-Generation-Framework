using System;
using UnityEngine;

namespace GEGFramework {
    public sealed class IntensityManager : MonoBehaviour {

        public static event Action<float> OnIntensityChanged; // invoked once intensity value changed
        public static event Action<GameMode> OnModeChanged; // invoked once intensity value changed
        public static IntensityManager Instance { get; private set; } // singleton instance

        [SerializeField, Range(0, 100)]
        float _intensity; // an emotional intensity value ranged from 0 to 100
        public float Intensity { get { return _intensity; } private set { _intensity = value; } }

        [SerializeField] GameMode currentMode = GameMode.Easy;
        public GameMode Mode { get { return currentMode; } }

        [SerializeField, Tooltip("Value (in intensity value) at which intensity value decreases per second")]
        float autoDecreaseAmount;

        [SerializeField, Tooltip("The cooling down period (in seconds) before intensity value automatically decrease")]
        float autoDecreaseCooldown;

        [SerializeField, Tooltip("Persistence of easy mode (in waves)")]
        public int easyModeDuration;

        [SerializeField, Tooltip("Persistence of hard mode (in waves)")]
        public int hardModeDuration;

        [SerializeField, Tooltip("The threshold (in intensity value) that triggers the hard mode")]
        float hardModeThreshold;

        [SerializeField, Range(0, 100)]
        float expectedFelxibity; // [expectIntensity - expectedFelxibity, expectIntensity + expectedFelxibity]

        [SerializeField, Range(0, 100)]
        float expectEasyIntensity;

        [SerializeField, Range(0, 100)]
        float expectNormalIntensity;

        [SerializeField, Range(0, 100)]
        float expectHardIntensity;

        [SerializeField, Range(1, 100)]
        float maxAdjustment; // amount of adjustment (in percentage) for each difficulty adjustment for properties

        float coolDownTimer;
        int durationCounter; // counts the number of wave in current game mode

        void Awake() {
            // Initialize singleton
            if (Instance != null && Instance != this) Destroy(this);
            else Instance = this;

            durationCounter = 0;
            coolDownTimer = autoDecreaseCooldown;
            OnIntensityChanged?.Invoke(_intensity);
            OnModeChanged?.Invoke(currentMode);
        }

        void OnEnable() {
            Spawner.OnNewWaveStart += (_) => {
                coolDownTimer = autoDecreaseCooldown;
                UpdateGameMode();
            };
        }

        // Update is called once per frame
        void Update() {
            coolDownTimer -= Time.deltaTime;
            if (coolDownTimer <= 0) {
                _intensity = Mathf.Clamp(_intensity - autoDecreaseAmount * Time.deltaTime,
                    0, 100);
                OnIntensityChanged?.Invoke(_intensity);
            }
        }

        void OnApplicationQuit() {
            foreach (GEGCharacter c in PackedData.Instance.characters) {
                foreach (GEGProperty prop in c.properties) {
                    prop.value = prop.defaultValue; // reset scriptable objects
                }
            }
        }

        /// <summary>
        /// Update the intensity value base on special event triggers
        /// </summary>
        /// <param name="percent">(e.g., currentHealth/maxHealth)</param>
        /// <param name="scaler">Scale up/down the [percent] parameter</param>
        /// <param name="proportional">If true, the intensity will increase as [percent] increases</param>
        public void UpdateIntensity(float percent, float scaler, bool proportional) {
            float contribute = percent * scaler;
            if (proportional) _intensity = Mathf.Clamp(_intensity + contribute, 0, 100);
            else _intensity = Mathf.Clamp(_intensity - contribute, 0, 100);
            OnIntensityChanged?.Invoke(_intensity);
            coolDownTimer = autoDecreaseCooldown;
        }

        void UpdateGameMode() {
            durationCounter++;
            switch (currentMode) {
                case GameMode.Easy:
                    if (durationCounter > easyModeDuration) {
                        durationCounter = 0;
                        currentMode = GameMode.Normal;
                        OnModeChanged?.Invoke(currentMode);
                    }
                    if (_intensity > expectEasyIntensity + expectedFelxibity) { // relax mode is too hard
                        UpdateAllEnemyProperty(false, maxAdjustment);
                    } else if (_intensity < expectEasyIntensity - expectedFelxibity) { // relax mode is too easy
                        UpdateAllEnemyProperty(true, maxAdjustment);
                    } // else within expect intensity
                    //UpdateEnemyQuantity(0, 2);
                    //UpdateEnemyQuantity(1, 0);
                    //UpdateEnemyQuantity(2, 0);
                    break;
                case GameMode.Normal:
                    if (_intensity >= hardModeThreshold) {
                        durationCounter = 0;
                        currentMode = GameMode.Hard;
                        OnModeChanged?.Invoke(currentMode);
                    }
                    if (_intensity > expectNormalIntensity + expectedFelxibity) { // normal mode is too hard
                        UpdateAllEnemyProperty(false, maxAdjustment);
                    } else if (_intensity < expectNormalIntensity - expectedFelxibity) { // normal mode is too easy
                        UpdateAllEnemyProperty(true, maxAdjustment);
                    }
                    //UpdateEnemyQuantity(0, 2);
                    //UpdateEnemyQuantity(1, 2);
                    //UpdateEnemyQuantity(2, 0);
                    break;
                case GameMode.Hard:
                    if (durationCounter > hardModeDuration) {
                        durationCounter = 0;
                        currentMode = GameMode.Easy;
                        OnModeChanged?.Invoke(currentMode);
                    }
                    if (_intensity > expectHardIntensity + expectedFelxibity) { // hard mode is too hard
                        UpdateAllEnemyProperty(false, maxAdjustment);
                    } else if (_intensity < expectHardIntensity - expectedFelxibity) { // hard mode is too easy
                        UpdateAllEnemyProperty(true, maxAdjustment);
                    }
                    //UpdateEnemyQuantity(0, 2);
                    //UpdateEnemyQuantity(1, 2);
                    //UpdateEnemyQuantity(2, 2);
                    break;
            }
        }

        void UpdateAllEnemyProperty(bool increase, float percent) {
            foreach (GEGCharacter c in PackedData.Instance.characters) {
                if (c.type == CharacterType.Enemy) { // if it's an enemy type
                    foreach (GEGProperty prop in c.properties) {
                        if (prop.enabled) { // if the property enabled for evaluation
                            float adjustment = prop.defaultValue * (percent / 100) * (prop.importance / 100);
                            prop.value += increase ? adjustment : -adjustment;
                        }
                    }
                }
            }
        }

        void UpdateEnemyProperty(string propName = null, float? val = null) {
            foreach (GEGCharacter c in PackedData.Instance.characters) {
                if (c.type == CharacterType.Enemy) { // if it's an enemy type
                    foreach (GEGProperty prop in c.properties) {
                        if (prop.enabled && (prop.propertyName == propName || propName == null)) {
                            // if the property enabled for evaluation
                            if (val.HasValue) prop.value = val.Value;
                            else prop.value += prop.value * _intensity / 100 * prop.importance / 100;
                        }
                    }
                }
            }
        }

        void UpdateEnemyQuantity(int index, int quantity) {
            if (index < PackedData.Instance.characters.Count) {
                if (PackedData.Instance.characters[index].type == CharacterType.Enemy)
                    PackedData.Instance.characters[index].numNextWave = quantity;
            }
        }
    }
}
