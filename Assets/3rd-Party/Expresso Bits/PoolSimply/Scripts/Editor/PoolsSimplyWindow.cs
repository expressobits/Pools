using UnityEngine;
using UnityEditor;
using ExpressoBits.PoolSimply;
using System.Collections.Generic;

public class PoolsSimplyWindow : EditorWindow {

    Pools pools;

    [MenuItem("Window/Analysis/Pools")]
    private static void ShowWindow() {
        var window = GetWindow<PoolsSimplyWindow>();
        Texture2D m_Logo = EditorGUIUtility.Load("Assets/3rd-Party/Expresso Bits/PoolSimply/Textures/Editor/PoolIcon.png") as Texture2D;
        window.titleContent = new GUIContent("Pools",m_Logo);
        window.Show();
    }

    private void OnGUI() {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        DrawStats();
    }

    private void DrawStats(){
        EditorGUILayout.BeginVertical("box");
        GUILayout.Label("Objects Stats", EditorStyles.boldLabel);
        
        if(Pools.instance == null){
            GUILayout.Label("Must be in play mode", EditorStyles.largeLabel);
        }else{
            
            for (int i = 0; i < Pools.Instance().keys.Count; i++)
            {
                string key = Pools.Instance().keys[i];
                Pool pool;
                Pools.Instance().dictionary.TryGetValue(key, out pool);
                int count = pool.objects.Count;
                GUILayout.Label("Objects Key:"+ key + " Count:"+count, EditorStyles.miniLabel);
            }
            
        }
        EditorGUILayout.EndVertical();
    }
}