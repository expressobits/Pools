using UnityEngine;

public class FPS : MonoBehaviour
{
    private float m_DeltaTime = 0.0f;

    private void Update()
    {
        m_DeltaTime += (Time.unscaledDeltaTime - m_DeltaTime) * 0.1f;
    }

    private void OnGUI()
    {
        // GUIStyle style = new GUIStyle();
        // int w = Screen.width, h = Screen.height;
        // Rect rect = new Rect(0, 0, w, h * 4 / 100);
        // style.alignment = TextAnchor.UpperLeft;
        // style.fontSize = h * 4 / 100;
        // style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
        // var msec = m_DeltaTime * 1000.0f;
        // var fps = 1.0f / m_DeltaTime;
        // var text = $"{msec:000.0} MS {fps:0.} FPS";
        // GUILayout.Label(text);
    }
}