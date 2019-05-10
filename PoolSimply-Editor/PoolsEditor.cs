using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace ExpressoBits.PoolSimply.Editor
{
    [CustomEditor(typeof(Pools))]
    public class PoolsEditor : UnityEditor.Editor
    {

        private void SetIcons()
        {
            // this sets the icon on the game object containing our behaviour
            //(target as Pools).gameObject.SetIcon("Pool", Properties.Resources.Pool);

            // this sets the icon on the script (which normally shows the blank page icon)
            MonoScript.FromMonoBehaviour(target as Pools).SetIcon("Pool", Properties.Resources.Pool);
        }

        protected virtual void Awake()
        {
            SetIcons();
            Pools.Instance = target as Pools;
        }


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


