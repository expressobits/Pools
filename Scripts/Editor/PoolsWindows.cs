using UnityEngine;
using UnityEditor;

namespace ExpressoBits.Pools
{
    public class PoolsWindows : EditorWindow
    {
        public static Texture2D logo;

        PoolManager m_PoolManager;

        [MenuItem("Window/Analysis/Pools")]
        private static void ShowWindow()
        {
            var window = GetWindow<PoolsWindows>();
            logo =
                EditorGUIUtility.Load("Assets/3rd-Party/Expresso Bits/PoolSimply/Textures/Editor/PoolIcon.png") as
                    Texture2D;
            window.titleContent = new GUIContent("Pools", logo);
            window.Show();
        }

        private void OnGUI()
        {
            Texture2D tex = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            tex.SetPixel(0, 0, new Color(1f, 0.8f, 0.1f));
            tex.Apply();
            GUI.DrawTexture(new Rect(0, 0, maxSize.x, maxSize.y), tex, ScaleMode.StretchToFill);
            GUILayout.Label("Pools", EditorStyles.centeredGreyMiniLabel);

            DrawStats();

            GUILayout.FlexibleSpace();
            DrawInfo();
        }

        private void DrawInfo()
        {
            EditorGUILayout.BeginVertical();
            GUILayout.Label("GitHub", EditorStyles.boldLabel);
            if (GUILayout.Button("https://github.com/ExpressoBits/PoolSimply >>>"))
            {
                Application.OpenURL("https://github.com/ExpressoBits/PoolSimply");
            }

            EditorGUILayout.EndVertical();
        }

        private void DrawStats()
        {
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("Pools Stats", EditorStyles.boldLabel, GUILayout.Width(Screen.width));
            logo =
                EditorGUIUtility.Load("Assets/3rd-Party/Expresso Bits/PoolSimply/Textures/Editor/PoolIcon.png") as
                    Texture2D;

            if (PoolManager.instance == null)
            {
                GUILayout.Label("Must be in play mode", EditorStyles.largeLabel);
            }
            else
            {
                float win = Screen.width * 0.95f;
                float w1 = win * 0.10f;
                float w2 = win * 0.20f;
                float w3 = win * 0.25f;

                GUILayout.BeginHorizontal();
                DrawLineTable(logo, "Pooler", w1, "Objects", w2, EditorStyles.boldLabel, Color.black);
                GUILayout.Space(w3);
                GUILayout.Space(w3);
                GUILayout.EndHorizontal();

                for (int i = 0; i < PoolManager.Instance().ids.Count; i++)
                {
                    int key = PoolManager.Instance().ids[i];
                    PoolData poolData = EditorUtility.InstanceIDToObject(key) as PoolData;

                    Pool pool;
                    PoolManager.Instance().pools.TryGetValue(key, out pool);
                    int count = pool.objects.Count;
                    GUILayout.BeginHorizontal();
                    DrawLineTable(logo, poolData.name + "", w1, count + "", w2, EditorStyles.label, poolData.Color);
                    if (GUILayout.Button("Clear"))
                    {
                        pool.Clear();
                    }

                    if (GUILayout.Button("Inc"))
                    {
                        //pool.IncreaseAmount();
                    }

                    GUILayout.EndHorizontal();
                }
            }

            EditorGUILayout.EndVertical();
            DrawButtons();
        }

        private void DrawLineTable(Texture2D tex, string s1, float w1, string s2, float w2, GUIStyle style, Color color)
        {
            GUILayout.Label(tex, GUILayout.Width(w1));
            GUIStyle styleTitle = EditorStyles.label;
            Color lastcolor = styleTitle.normal.textColor;
            styleTitle.normal.textColor = color;
            GUILayout.Label(s1, styleTitle, GUILayout.Width(w2));
            styleTitle.normal.textColor = lastcolor;
            GUILayout.Label(s2, style, GUILayout.Width(w2));
        }

        //TODO dasdas
        private void DrawButtons()
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Clear All"))
            {
                for (int i = 0; i < PoolManager.Instance().ids.Count; i++)
                {
                    int key = PoolManager.Instance().ids[i];
                    Pool pool;
                    if (PoolManager.Instance().pools.TryGetValue(key, out pool))
                    {
                        pool.Clear();
                    }
                }
            }

            if (GUILayout.Button("Increase All"))
            {
                for (int i = 0; i < PoolManager.Instance().ids.Count; i++)
                {
                    int key = PoolManager.Instance().ids[i];
                    Pool pool;
                    if (PoolManager.Instance().pools.TryGetValue(key, out pool))
                    {
                        //pool.IncreaseAmount();
                    }
                }
            }

            GUILayout.EndHorizontal();
        }

        private void Update()
        {
            Repaint();
        }
    }
}