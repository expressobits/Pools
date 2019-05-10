using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace ExpressoBits.PoolSimply.Editor
{
    [CustomEditor(typeof(Pools))]
    public class PoolsEditor : UnityEditor.Editor
    {

        public override void OnInspectorGUI()
        {

            if (GUILayout.Button("Clean Pools Data"))
            {
                Pools.Instance.Clear();
                Debug.Log("Clean all pools data!");
            }

            if (Pools.Instance != null)
            {
                List<string> keys = Pools.Instance.getKeys();
                foreach (string key in keys)
                {
                    EditorGUILayout.LabelField("Key:" + key + " " + Pools.Instance.CountOfKey(key));
                }
            }


        }



    }
}


