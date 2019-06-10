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
        Texture2D tex = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        tex.SetPixel(0, 0, new Color(1f, 0.8f, 0.1f));
        tex.Apply();
        GUI.DrawTexture(new Rect(0, 0, maxSize.x, maxSize.y), tex, ScaleMode.StretchToFill);
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
                GUILayout.Label("\n["+ key + "] - count("+count+")\n", EditorStyles.miniLabel);
            }
            
        }
        EditorGUILayout.EndVertical();
    }
}