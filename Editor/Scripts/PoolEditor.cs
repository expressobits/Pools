using UnityEditor;
using UnityEngine;

namespace ExpressoBits.Pools.Editor
{
    [CustomEditor(typeof(Pool))]
    public class PoolEditor : UnityEditor.Editor
    {

        public static void ShowPool(Pool pool,SerializedObject serializedObject)
        {
            SerializedProperty settingsProperty = serializedObject.FindProperty("settings");
            SerializedProperty prefabProperty = serializedObject.FindProperty("prefab");

            string oldStringValue = pool.name;
            pool.name = EditorGUILayout.TextField("Name", pool.name);
            prefabProperty.objectReferenceValue = (GameObject)EditorGUILayout.ObjectField("Prefab", prefabProperty.objectReferenceValue, typeof(GameObject), false);
            EditorGUILayout.PropertyField(settingsProperty);
            if (pool.name != oldStringValue) AssetDatabase.SaveAssets();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Clear Pool"))
            {
                pool.Clear();
                Debug.Log("Clean all pool disabled objects!");
            }
            EditorGUI.BeginDisabledGroup(!Application.isPlaying);
            if (GUILayout.Button("Instantiate"))
            {
                pool.Instantiate();
                Debug.Log("Instantiate GameObject From Pool!");
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();

        }

        public override void OnInspectorGUI()
        {
            var pool = target as Pool;

            ShowPool(pool,serializedObject);
            

            EditorGUILayout.BeginVertical("Box");
            var origFontStyle = EditorStyles.label.fontStyle;
            EditorStyles.label.fontStyle = FontStyle.Bold;
            EditorGUILayout.LabelField("Basic Pool Information");
            EditorStyles.label.fontStyle = origFontStyle;
            string actualObjects;
            if (pool.Objects != null)
            {
                actualObjects = "Objects in pool: " + pool.Objects.Count;
                foreach (var gameObject in pool.Objects)
                {
                    if (gameObject != null) ShowObject(gameObject);
                }
            }
            else
            {
                actualObjects = "Objects in pool: 0";
            }
            EditorGUILayout.LabelField(actualObjects);
            EditorGUILayout.EndHorizontal();
            serializedObject.ApplyModifiedProperties();
        }

        public static void ShowPool()
        {

        }

        private void ShowObject(GameObject gameObject)
        {
            EditorGUILayout.LabelField(gameObject.name);
        }

    }
}

