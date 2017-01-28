using UnityEngine;
using UnityEditor;
using Systems.EntitySystem.Database;

namespace Systems.EntitySystem.Editor
{
    public class EntityTypeDialog : EditorWindow
    {
        const string windowTitle = "Entity Types";

        public delegate void SelectEvent(EntityTypeAsset asset);

        public SelectEvent OnAssetSelect;

        private Vector2 scroll;

        static public void Display(SelectEvent del)
        {
            var window = GetWindow<EntityTypeDialog>(true, windowTitle, true);
            window.OnAssetSelect = del;
            window.Show();
        }

        public void OnGUI()
        {
            scroll = GUILayout.BeginScrollView(scroll);
            for (int i = 0; i < EntityTypeDatabase.GetAssetCount(); i++)
            {
                var asset = EntityTypeDatabase.GetAt(i);
                if (asset != null)
                {
                    if (GUILayout.Button(asset.Name, EditorStyles.toolbarButton))
                    {
                        if (OnAssetSelect != null)
                        {
                            OnAssetSelect(asset);
                        }
                        this.Close();
                    }
                }
            }

            GUILayout.EndScrollView();
        }
    }
}