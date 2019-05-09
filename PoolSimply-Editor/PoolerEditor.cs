using UnityEngine;
using UnityEditor;

namespace ExpressoBits.PoolSimply
{
    [CustomEditor(typeof(Pooler))]
    public class PoolerEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            var pooler = target as Pooler;

            EditorGUILayout.LabelField("Pool ID", EditorStyles.boldLabel);
            pooler.id = EditorGUILayout.TextField("IDExample", pooler.id);

            EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel);

            pooler.initialAmount = EditorGUILayout.IntSlider("Initial amount allocated", pooler.initialAmount, 1, 100);
            pooler.willGrow = GUILayout.Toggle(pooler.willGrow, "Will Grow?");
            if (pooler.willGrow)
            {
                pooler.increaseAmount = EditorGUILayout.IntSlider("Increase Amount", pooler.increaseAmount, 1, 100);
            }

            EditorGUILayout.Separator();


            EditorGUILayout.LabelField("Event From Pool", EditorStyles.boldLabel);

            this.serializedObject.Update();
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("OnEnableFromPool"), true);
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("OnDisableFromPool"), true);
            this.serializedObject.ApplyModifiedProperties();

        }
    }

}