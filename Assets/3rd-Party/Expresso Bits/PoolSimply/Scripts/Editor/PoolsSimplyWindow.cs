using UnityEngine;
using UnityEditor;
using ExpressoBits.PoolSimply;
using System.Collections.Generic;

public class PoolsSimplyWindow : EditorWindow {

    public static Texture2D logo;

    Pools pools;

    [MenuItem("Window/Analysis/Pools")]
    private static void ShowWindow() {
        var window = GetWindow<PoolsSimplyWindow>();
        logo = EditorGUIUtility.Load("Assets/3rd-Party/Expresso Bits/PoolSimply/Textures/Editor/PoolIcon.png") as Texture2D;
        window.titleContent = new GUIContent("Pools",logo);
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
        GUILayout.Label("Pools Stats", EditorStyles.boldLabel,GUILayout.Width(Screen.width));
        logo = EditorGUIUtility.Load("Assets/3rd-Party/Expresso Bits/PoolSimply/Textures/Editor/PoolIcon.png") as Texture2D;
        
        if(Pools.instance == null){
            GUILayout.Label("Must be in play mode", EditorStyles.largeLabel);
        }else{

            float win=Screen.width*0.95f;
            float w1=win*0.10f; float w2=win*0.20f; float w3=win*0.20f;

            GUILayout.BeginHorizontal();
            DrawLineTable(logo,"Pooler",w1,"Objects",w2,EditorStyles.boldLabel);
            GUILayout.Space(w3);
            GUILayout.Space(w3);
            GUILayout.EndHorizontal();
            
            for (int i = 0; i < Pools.Instance().keys.Count; i++)
            {
                string key = Pools.Instance().keys[i];
                Pool pool;
                Pools.Instance().dictionary.TryGetValue(key, out pool);
                int count = pool.objects.Count;
                GUILayout.BeginHorizontal();
                DrawLineTable(logo,key+"",w1,count+"",w2,EditorStyles.label);
                if(GUILayout.Button("Clear",GUILayout.Width(w3))){
                    pool.Clear();
                }
                if(GUILayout.Button("Inc",GUILayout.Width(w3))){
                    pool.IncreaseAmount();
                }
                GUILayout.EndHorizontal();
            }
            
        }
        EditorGUILayout.EndVertical();
        DrawButtons();
    }

    private void DrawLineTable(Texture2D tex,string s1,float w1,string s2, float w2,GUIStyle style){
        GUILayout.Label(tex,GUILayout.Width(w1));
        GUILayout.Label(s1,style,GUILayout.Width(w2));
        GUILayout.Label(s2,style,GUILayout.Width(w2));
    }


    private void DrawButtons(){
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Clear All",GUILayout.Width(Screen.width*0.48f))){
            for (int i = 0; i < Pools.Instance().keys.Count; i++){
                string key = Pools.Instance().keys[i];
                Pool pool;
                if(Pools.Instance().dictionary.TryGetValue(key, out pool)){
                    pool.Clear();
                }
                
            }
        }
        if(GUILayout.Button("Increase All",GUILayout.Width(Screen.width*0.48f))){
            for (int i = 0; i < Pools.Instance().keys.Count; i++){
                string key = Pools.Instance().keys[i];
                Pool pool;
                if(Pools.Instance().dictionary.TryGetValue(key, out pool)){
                    pool.IncreaseAmount();
                }
                
            }
        }
        GUILayout.EndHorizontal();
    }

    private void Update() {
        Repaint();
    }
}