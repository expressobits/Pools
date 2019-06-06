using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ExpressoBits.PoolSimply.Editor
{
    [CustomEditor(typeof(Pooler))]
    public class PoolerEditor : UnityEditor.Editor
    {

        private void SetIcons()
        {
            // this sets the icon on the game object containing our behaviour
            (target as Pooler).gameObject.SetIcon("Floater", Properties.Resources.Floater);

            // this sets the icon on the script (which normally shows the blank page icon)
            MonoScript.FromMonoBehaviour(target as Pooler).SetIcon("Floater", Properties.Resources.Floater);
        }

        protected virtual void Awake()
        {
            SetIcons();
        }


        public override void OnInspectorGUI()
        {
            var pooler = target as Pooler;

            if(Pools.Instance == null)
                EditorGUILayout.HelpBox("Not found Pools instance!\nAdd one component 'Pools' on your Scene!", MessageType.Error);

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