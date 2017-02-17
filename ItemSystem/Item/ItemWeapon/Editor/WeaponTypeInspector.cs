using UnityEngine;
using UnityEditor;
using Systems.ItemSystem.Database;

namespace Systems.ItemSystem.Editor
{
    [CustomEditor(typeof(WeaponTypeDatabase))]
    public class WeaponTypeInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            GUILayout.Label("Database that stores all WeaponTypes");

            if (GUILayout.Button("Open Editor Window"))
            {
                WeaponTypeEditor.ShowWindow();
            }
        }
    }
}