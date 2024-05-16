using Meijvogel.ModularArcadeKarts.Base;
using UnityEditor;

namespace Meijvogel.ModularArcadeKarts.Editor
{
    [CustomEditor(typeof(KartStats))]
    public class KartStatsEditor : UnityEditor.Editor
    {
        SerializedProperty _speedBasedSteering;
        SerializedProperty _turnModel;

        void OnEnable()
        {
            _speedBasedSteering = serializedObject.FindProperty("_speedBasedSteering");
            _turnModel = serializedObject.FindProperty("_turnModel");
        }

        public override void OnInspectorGUI()
        {
            

            serializedObject.Update();

            EditorGUILayout.LabelField("Speed Settings", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_maxSpeed"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_backwardMaxSpeed"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_boostSpeed"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_accelerationTime"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_decelerationTime"));

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Steering Settings", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_speedBasedSteering);

            if (!_speedBasedSteering.boolValue)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_steerPower"));
            }
            else
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_steerSpeedThreshold"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_sharpSteerPower"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_wideSteerPower"));
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Drift Settings", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_driftSpeedThreshold"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_driftPower"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_inwardDriftFactor"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_outwardDriftFactor"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_outwardsDriftForce"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_driftTimeThresholds"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_boostPhaseDurations"), true);
            EditorGUILayout.PropertyField(_turnModel);
            if (_turnModel.boolValue)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_turnSpeed"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_visualDriftAngle"));
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Gravity Settings", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_gravity"));

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Raycast Settings", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_layerMask"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_raycastDistance"));

            serializedObject.ApplyModifiedProperties();
        }

        public override bool RequiresConstantRepaint()
        {
            return false;
        }
    }
}