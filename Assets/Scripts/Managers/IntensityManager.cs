using System;
using UnityEngine;

namespace GEGFramework {
    public sealed class IntensityManager : MonoBehaviour {

        public static event Action<float> OnIntensityChanged; // invoked once intensity value changed
        public static event Action<GameMode> OnModeChanged; // invoked once intensity value changed
        public static IntensityManager Instance { get; private set; } // singleton instance

        [SerializeField, Range(0, 100), Tooltip("Emotional intensity of player")]
        float _intensity; // an emotional intensity value ranged from 0 to 100
        public float Intensity { get { return _intensity; } private set { _intensity = value; } }

        [SerializeField] GameMode currentMode = GameMode.Easy;
        public GameMode Mode { get { return currentMode; } }

        [SerializeField, Tooltip("Value (in intensity value) at which intensity value decreases per second")]
        float autoDecreaseAmount;

        [SerializeField, Tooltip("The cooling down period (in seconds) before intensity value " +
            "automatically decrease")]
        float autoDecreaseCooldown;

        [SerializeField, Tooltip("Persistence of easy mode (in waves)")]
        public int easyModeDuration;

        [SerializeField, Tooltip("Persistence of hard mode (in waves)")]
        public int hardModeDuration;

        [SerializeField, Tooltip("The threshold (in intensity value) that triggers the hard mode")]
        float hardEntryThreshold;

        [SerializeField, Range(1, 100), Tooltip("Maximum amount of adjustment (in percentage) for each property" +
            " for each time the difficulty adjustment happens")]
        float maxAdjustment;

        [SerializeField, Range(0, 100), Tooltip("Flexibility of expected intensity; Expected intensity range " +
            "= [expectIntensity - expectedFelxibity, expectIntensity + expectedFelxibity]")]
        float expectedFelxibity; // [expectIntensity - expectedFelxibity, expectIntensity + expectedFelxibity]

        [SerializeField, Range(0, 100), Tooltip("Expected emotional intensity of easy mode")]
        float expectEasyIntensity;
        [SerializeField, Tooltip("Scale up/down the intensity increment in easy mode")]
        float easyIntensityIncScalar;
        [SerializeField, Tooltip("Scale up/down the intensity decrement in easy mode")]
        float easyIntensityDecScalar;

        [SerializeField, Range(0, 100), Tooltip("Expected emotional intensity of normal mode")]
        float expectNormalIntensity;
        [SerializeField, Tooltip("Scale up/down the intensity increment in normal mode")]
        float normalIntensityIncScalar;
        [SerializeField, Tooltip("Scale up/down the intensity decrement in normal mode")]
        float normalIntensityDecScalar;

        [SerializeField, Range(0, 100), Tooltip("Expected emotional intensity of hard mode")]
        float expectHardIntensity;
        [SerializeField, Tooltip("Scale up/down the intensity increment in hard mode")]
        float hardIntensityIncScalar;
        [SerializeField, Tooltip("Scale up/down the intensity decrement in hard mode")]
        float hardIntensityDecScalar;

        [SerializeField, Range(0, 100), Tooltip("The accumulation of difficulty over time")]
        float cumulationRate;

        float cumulation;
        float coolDownTimer;
        int durationCounter; // counts the number of wave in current game mode

        void Awake() {
            // Initialize singleton
            if (Instance != null && Instance != this) Destroy(this);
            else Instance = this;

            cumulation = 1;
            durationCounter = 0;
            coolDownTimer = autoDecreaseCooldown;
            OnIntensityChanged?.Invoke(_intensity);
            OnModeChanged?.Invoke(currentMode);
        }

        void OnEnable() {
            Spawner.OnNewWaveStart += (_) => {
                cumulation *= 1 + cumulationRate / 100;
                coolDownTimer = autoDecreaseCooldown;
                UpdateGameMode(); // Udpate game mode when new wave starts
            };
        }

        // Update is called once per frame
        void Update() {
            coolDownTimer -= Time.deltaTime;
            if (coolDownTimer <= 0) {
                switch (currentMode) {
                    case GameMode.Easy:
                        _intensity = Mathf.Clamp(_intensity - autoDecreaseAmount * easyIntensityDecScalar
                            * Time.deltaTime, 0, 100);
                        break;
                    case GameMode.Normal:
                        _intensity = Mathf.Clamp(_intensity - autoDecreaseAmount * normalIntensityDecScalar
                            * Time.deltaTime, 0, 100);
                        break;
                    case GameMode.Hard:
                        _intensity = Mathf.Clamp(_intensity - autoDecreaseAmount * hardIntensityDecScalar
                            * Time.deltaTime, 0, 100);
                        break;
                }
                OnIntensityChanged?.Invoke(_intensity);
            }
        }

        void OnApplicationQuit() {
            ResetProperties();
        }

        /// <summary>
        /// Update the intensity value base on special event triggers
        /// </summary>
        /// <param name="percent">(e.g., currentHealth/maxHealth)</param>
        /// <param name="scalar">Scale up/down the [percent] parameter</param>
        /// <param name="increase">If true, the intensity will increase as [percent] increases</param>
        public void UpdateIntensity(float percent, float scalar, bool increase) {
            float contribute = 0;
            switch (currentMode) {
                case GameMode.Easy:
                    contribute = increase ? percent * scalar * easyIntensityIncScalar
                        : percent * scalar * easyIntensityDecScalar;
                    break;
                case GameMode.Normal:
                    contribute = increase ? percent * scalar * normalIntensityIncScalar
                        : percent* scalar * normalIntensityDecScalar;
                    break;
                case GameMode.Hard:
                    contribute = increase ? percent * scalar * hardIntensityIncScalar 
                        : percent * scalar * hardIntensityDecScalar;
                    break;
            }
            if (increase) _intensity = Mathf.Clamp(_intensity + contribute, 0, 100);
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
                        easyIntensityDecScalar *= 1 + maxAdjustment / 100; // since difficulty decreased, scalar should decrease too
                    } else if (_intensity < expectEasyIntensity - expectedFelxibity) { // relax mode is too easy
                        UpdateAllEnemyProperty(true, maxAdjustment);
                        easyIntensityIncScalar *= 1 + maxAdjustment / 100; // since difficulty increase, scalar should increase too
                    } // else within expect intensity
                    //UpdateEnemyQuantity(0, 2);
                    //UpdateEnemyQuantity(1, 0);
                    //UpdateEnemyQuantity(2, 0);
                    break;
                case GameMode.Normal:
                    if (_intensity >= hardEntryThreshold) {
                        durationCounter = 0;
                        currentMode = GameMode.Hard;
                        OnModeChanged?.Invoke(currentMode);
                    }
                    if (_intensity > expectNormalIntensity + expectedFelxibity) { // normal mode is too hard
                        UpdateAllEnemyProperty(false, maxAdjustment);
                        normalIntensityDecScalar *= 1 + maxAdjustment / 100;
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
                        hardIntensityDecScalar *= 1 + maxAdjustment / 100;
                    } else if (_intensity < expectHardIntensity - expectedFelxibity) { // hard mode is too easy
                        UpdateAllEnemyProperty(true, maxAdjustment);
                    }
                    //UpdateEnemyQuantity(0, 2);
                    //UpdateEnemyQuantity(1, 2);
                    //UpdateEnemyQuantity(2, 2);
                    break;
            }
        }

        void UpdateAllEnemyProperty(bool increase, float percent, float? scaler = null) {
            foreach (GEGCharacter c in PackedData.Instance.characters) {
                if (c.type == CharacterType.Enemy) { // if it's an enemy type
                    foreach (GEGProperty prop in c.properties) {
                        if (prop.enabled) { // if the property enabled for evaluation
                            float adjustment = increase ? 
                                prop.defaultValue * ((percent + cumulation) / 100) * (prop.importance / 100)
                                : prop.defaultValue * (percent / 100) * (prop.importance / 100);
                            prop.value += increase ? adjustment : -adjustment; // update property's value
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

        public static void ResetProperties() {
            foreach (GEGCharacter c in PackedData.Instance.characters) {
                foreach (GEGProperty prop in c.properties) {
                    prop.value = prop.defaultValue; // reset scriptable objects
                }
            }
        }
    }
}
