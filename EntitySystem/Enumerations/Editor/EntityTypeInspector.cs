using UnityEngine;
using UnityEditor;
using Systems.EntitySystem.Database;

namespace Systems.EntitySystem.Editor
{
    [CustomEditor(typeof(EntityTypeDatabase))]
    public class EntityTypeInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            GUILayout.Label("Database that stores all EntityTypes");

            if (GUILayout.Button("Open Editor Window"))
            {
                EntityTypeEditorWindow.ShowWindow();
            }
        }
    }
}