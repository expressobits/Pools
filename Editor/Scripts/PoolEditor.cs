using System.Xml.Linq;
using UnityEditor;
using UnityEngine;

namespace ExpressoBits.Pools
{
    [CustomEditor(typeof(Pool))]
    public class PoolEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            var pool = target as Pool;

            base.OnInspectorGUI();



            if (GUILayout.Button("Destroy All Disabled Objects"))
            {
                pool.Clear();
                Debug.Log("Clean all pool disabled objects!");
            }

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
                    if(gameObject != null) ShowObject(gameObject);
                }
            }
            else
            {
                actualObjects = "Objects in pool: 0";
            }
            EditorGUILayout.LabelField(actualObjects);
            EditorGUILayout.EndHorizontal();
        }

        private void ShowObject(GameObject gameObject)
        {
            EditorGUILayout.LabelField(gameObject.name);
        }

    }
}

