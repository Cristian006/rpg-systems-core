using UnityEngine;
using UnityEditor;
using Systems.StatSystem.Database;

namespace Systems.StatSystem.Editor
{
    [CustomEditor(typeof(StatTypeDatabase))]
    public class StatTypeInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            GUILayout.Label("Database that stores all StatTypes");

            if (GUILayout.Button("Open Editor Window"))
            {
                StatTypeEditor.ShowWindow();
            }
        }
    }
}