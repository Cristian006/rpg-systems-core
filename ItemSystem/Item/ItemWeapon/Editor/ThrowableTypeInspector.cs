using UnityEngine;
using UnityEditor;
using Systems.ItemSystem.Database;

namespace Systems.ItemSystem.Editor
{
    [CustomEditor(typeof(ThrowableTypeDatabase))]
    public class ThrowableTypeInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            GUILayout.Label("Database that stores all ThrowableTypes");

            if (GUILayout.Button("Open Editor Window"))
            {
                ThrowableTypeEditorWindow.ShowWindow();
            }
        }
    }
}