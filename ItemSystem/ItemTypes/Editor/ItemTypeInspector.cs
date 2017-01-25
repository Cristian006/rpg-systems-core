using UnityEngine;
using UnityEditor;
using Systems.ItemSystem.Database;

namespace Systems.ItemSystem.Editor
{
    [CustomEditor(typeof(ItemTypeDatabase))]
    public class ItemTypeInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            GUILayout.Label("Database that stores all ItemTypes");

            if (GUILayout.Button("Open Editor Window"))
            {
                ItemTypeEditorWindow.ShowWindow();
            }
        }
    }
}