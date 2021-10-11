using UnityEditor;
using UnityEngine;

namespace ExpressoBits.Pools.Editor
{
    [CustomEditor(typeof(PoolGroup))]
    public class PoolGroupEditor : UnityEditor.Editor
    {

        private string newName = "New Pool";
        private GameObject newPrefab;
        private int newIncreaseValue = 1;

        private bool[] actives;

        private SerializedProperty poolsProperty;

        private void OnEnable()
        {
            poolsProperty = serializedObject.FindProperty("pools");
        }

        public override void OnInspectorGUI()
        {
            PoolGroup poolGroup = (PoolGroup)target;

            var origFontStyle = EditorStyles.label.fontStyle;
            EditorStyles.label.fontStyle = FontStyle.Bold;
            EditorGUILayout.LabelField("Pool Group Functions");
            EditorStyles.label.fontStyle = origFontStyle;

            if (GUILayout.Button("Instantiate a Random"))
            {
                poolGroup.InstantiateARandom();
            }

            if (GUILayout.Button("Clear all pools"))
            {
                poolGroup.Clear();
            }
            EditorGUILayout.Space(32);

            Show(poolsProperty);

            EditorGUILayout.BeginVertical("box");
            EditorStyles.label.fontStyle = FontStyle.Bold;
            EditorGUILayout.LabelField("New Pool");
            //EditorGUI.indentLevel++;
            EditorStyles.label.fontStyle = origFontStyle;
            newName = EditorGUILayout.TextField("Name", newName);
            newPrefab = (GameObject)EditorGUILayout.ObjectField("Prefab", newPrefab, typeof(GameObject), false);
            newIncreaseValue = EditorGUILayout.IntField("Increase Value", newIncreaseValue);
            EditorGUI.BeginDisabledGroup(newName.Length == 0 || newPrefab == null);
            if (GUILayout.Button("Add New Pool"))
            {
                MakeNewPool(poolGroup, newName, newIncreaseValue, newPrefab);
            }
            EditorGUI.EndDisabledGroup();
            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }

        public static void MakeNewPool(PoolGroup poolGroup, string name, int increaseValue, GameObject prefab)
        {
            Pool pool = ScriptableObject.CreateInstance<Pool>();
            pool.name = name;
            // TODO add pool values to pool SO
            pool.Setup(new PoolSettings() { IncreaseSize = increaseValue }, prefab);
            poolGroup.AddPoolToList(pool);

            AssetDatabase.AddObjectToAsset(pool, poolGroup);
            AssetDatabase.SaveAssets();

            EditorUtility.SetDirty(poolGroup);
            EditorUtility.SetDirty(pool);
        }

        public void Show(SerializedProperty list)
        {
            for (int i = 0; i < list.arraySize; i++)
            {

                SerializedProperty property = list.GetArrayElementAtIndex(i);

                EditorGUILayout.BeginVertical();
                DrawPool(i, ref property);
                EditorGUILayout.EndVertical();
            }
            EditorUtils.DrawSplitter(false);
        }

        private void DrawPool(int index, ref SerializedProperty poolProperty)
        {
            SerializedObject nestedObject = new SerializedObject(poolProperty.objectReferenceValue);
            SerializedProperty prefabPropery = nestedObject.FindProperty("prefab");

            Pool pool = (Pool)poolProperty.objectReferenceValue;

            EditorUtils.DrawSplitter(false);
            bool displayContent = EditorUtils.DrawHeaderToggle(pool.name, poolProperty, pos => OnContextClick(pos, index));

            if (displayContent)
            {
                PoolEditor.ShowPool(pool, nestedObject);
                EditorGUILayout.Space(16);
            }
            nestedObject.ApplyModifiedProperties();
        }

        private void MoveComponent(int id, int offset)
        {
            Undo.SetCurrentGroupName("Move Pool");
            serializedObject.Update();
            poolsProperty.MoveArrayElement(id, id + offset);
            serializedObject.ApplyModifiedProperties();

            // Force save / refresh
            ForceSave();
        }

        private void OnContextClick(Vector2 position, int id)
        {
            var menu = new GenericMenu();

            if (id == 0)
                menu.AddDisabledItem(EditorGUIUtility.TrTextContent("Move Up"));
            else
                menu.AddItem(EditorGUIUtility.TrTextContent("Move Up"), false, () => MoveComponent(id, -1));

            if (id == poolsProperty.arraySize - 1)
                menu.AddDisabledItem(EditorGUIUtility.TrTextContent("Move Down"));
            else
                menu.AddItem(EditorGUIUtility.TrTextContent("Move Down"), false, () => MoveComponent(id, 1));

            menu.AddSeparator(string.Empty);
            menu.AddItem(EditorGUIUtility.TrTextContent("Remove"), false, () => RemoveComponent(id));

            menu.DropDown(new Rect(position, Vector2.zero));
        }

        private void RemoveComponent(int id)
        {
            SerializedProperty property = poolsProperty.GetArrayElementAtIndex(id);
            Object component = property.objectReferenceValue;
            property.objectReferenceValue = null;

            Undo.SetCurrentGroupName(component == null ? "Remove Pool" : $"Remove {component.name}");

            // remove the array index itself from the list
            poolsProperty.DeleteArrayElementAtIndex(id);
            //UpdateEditorList();
            serializedObject.ApplyModifiedProperties();

            // Destroy the setting object after ApplyModifiedProperties(). If we do it before, redo
            // actions will be in the wrong order and the reference to the setting object in the
            // list will be lost.
            if (component != null)
            {
                Undo.DestroyObjectImmediate(component);
            }

            // Force save / refresh
            ForceSave();
        }

        private void ForceSave()
        {
            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssets();
        }
    }
}

