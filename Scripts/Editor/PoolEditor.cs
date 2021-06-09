using UnityEngine;
using UnityEditor;

namespace ExpressoBits.Pools
{
    [CustomEditor(typeof(Pool))]
    public class PoolEditor : Editor {

        private string actualObjects;
        //TODO Update string actual objects

        public override void OnInspectorGUI() {
            // var pool = new Pool(new GameObject(), new PoolData());//target as Pool;
            base.OnInspectorGUI();
            
            // // if(pool.objects != null){
            // //    actualObjects = "In pool objects: "+pool.objects.Count;
            // // }else{
            // //     actualObjects = "In pool objects: 0";
            // // }

            // // if (GUILayout.Button("Clean Pools Data"))
            // // {
            // //     pool.Clear();
            // //     Debug.Log("Clean all pools data!");
            // // }
            
            // EditorGUILayout.BeginVertical("box");
            // EditorGUILayout.LabelField(actualObjects);
            // EditorGUILayout.EndHorizontal(); 
        }

        [MenuItem("GameObject/Pool/Pool",false,49)]
        public static void CreatePooler(){
            GameObject go = new GameObject("Pool Object");
            go.AddComponent(typeof(Pool));
        }
        

    }
}

