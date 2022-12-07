using UnityEditor;
using UnityEngine;

namespace GEGFramework {
    [CustomEditor(typeof(IntensityManager))]
    public class IntensityManagerEditor : Editor {
        #region SerializedProperties
        SerializedProperty _intensity;
        SerializedProperty currentMode;
        SerializedProperty autoDecreaseAmount;
        SerializedProperty autoDecreaseCooldown;
        SerializedProperty easyModeDuration;
        SerializedProperty hardModeDuration;
        SerializedProperty hardEntryThreshold;
        SerializedProperty expectedFlexibility;
        SerializedProperty expectEasyIntensity;
        SerializedProperty easyIntensityIncScalar;
        SerializedProperty easyIntensityDecScalar;
        SerializedProperty expectNormalIntensity;
        SerializedProperty normalIntensityIncScalar;
        SerializedProperty normalIntensityDecScalar;
        SerializedProperty expectHardIntensity;
        SerializedProperty hardIntensityIncScalar;
        SerializedProperty hardIntensityDecScalar;
        SerializedProperty adjustment;
        SerializedProperty cumulationRate;
        #endregion

        IntensityManager _target;

        private void OnEnable() {
            _target = target as IntensityManager;

            _intensity = serializedObject.FindProperty("_intensity");
            currentMode = serializedObject.FindProperty("currentMode");
            autoDecreaseAmount = serializedObject.FindProperty("autoDecreaseAmount");
            autoDecreaseCooldown = serializedObject.FindProperty("autoDecreaseCooldown");
            easyModeDuration = serializedObject.FindProperty("easyModeDuration");
            hardModeDuration = serializedObject.FindProperty("hardModeDuration");
            hardEntryThreshold = serializedObject.FindProperty("hardEntryThreshold");
            expectedFlexibility = serializedObject.FindProperty("expectedFlexibility");
            adjustment = serializedObject.FindProperty("adjustment");
            cumulationRate = serializedObject.FindProperty("cumulationRate");

            expectEasyIntensity = serializedObject.FindProperty("expectEasyIntensity");
            easyIntensityIncScalar = serializedObject.FindProperty("easyIntensityIncScalar");
            easyIntensityDecScalar = serializedObject.FindProperty("easyIntensityDecScalar");

            expectNormalIntensity = serializedObject.FindProperty("expectNormalIntensity");
            normalIntensityIncScalar = serializedObject.FindProperty("normalIntensityIncScalar");
            normalIntensityDecScalar = serializedObject.FindProperty("normalIntensityDecScalar");

            expectHardIntensity = serializedObject.FindProperty("expectHardIntensity");
            hardIntensityIncScalar = serializedObject.FindProperty("hardIntensityIncScalar");
            hardIntensityDecScalar = serializedObject.FindProperty("hardIntensityDecScalar");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_intensity);
            EditorGUILayout.PropertyField(currentMode);
            EditorGUILayout.PropertyField(autoDecreaseAmount);
            EditorGUILayout.PropertyField(autoDecreaseCooldown);
            EditorGUILayout.PropertyField(expectedFlexibility);
            EditorGUILayout.PropertyField(adjustment);
            EditorGUILayout.PropertyField(cumulationRate);

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Duration Settings", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Easy Mode", GUILayout.Width(70));
            GUILayout.FlexibleSpace();
            EditorGUILayout.PropertyField(easyModeDuration, GUIContent.none, true, GUILayout.MinWidth(70));
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Hard Mode", GUILayout.Width(70));
            GUILayout.FlexibleSpace();
            EditorGUILayout.PropertyField(hardModeDuration, GUIContent.none, true, GUILayout.MinWidth(70));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Easy Mode Settings", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(expectEasyIntensity);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Intensity Increment Scalar");
            EditorGUILayout.PropertyField(easyIntensityIncScalar, GUIContent.none, true);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Intensity Decrement Scalar");
            EditorGUILayout.PropertyField(easyIntensityDecScalar, GUIContent.none, true);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Normal Mode Settings", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(expectNormalIntensity);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Intensity Increment Scalar");
            EditorGUILayout.PropertyField(normalIntensityIncScalar, GUIContent.none, true);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Intensity Decrement Scalar");
            EditorGUILayout.PropertyField(normalIntensityDecScalar, GUIContent.none, true);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Hard Mode Settings", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Entry Threshold");
            EditorGUILayout.PropertyField(hardEntryThreshold, GUIContent.none, true);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.PropertyField(expectHardIntensity);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Intensity Increment Scalar");
            EditorGUILayout.PropertyField(hardIntensityIncScalar, GUIContent.none, true);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Intensity Decrement Scalar");
            EditorGUILayout.PropertyField(hardIntensityDecScalar, GUIContent.none, true);
            EditorGUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();
        }
    }
}