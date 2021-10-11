using UnityEngine;
using UnityEditor;

namespace ExpressoBits.Pools.Editor
{
    [CustomEditor(typeof(Pooler))]
    public class PoolerEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
        }

        [MenuItem("GameObject/Pool/Pooler",false,49)]
        public static void CreatePooler(){
            GameObject go = new GameObject("Pooler Object");
            go.AddComponent(typeof(Pooler));
        }
    }
}
