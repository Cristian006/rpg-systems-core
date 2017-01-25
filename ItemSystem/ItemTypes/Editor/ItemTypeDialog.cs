using UnityEngine;
using UnityEditor;
using Systems.ItemSystem.Database;

namespace Systems.ItemSystem.Editor
{
    public class ItemTypeDialog : EditorWindow
    {
        const string windowTitle = "Item Types";

        public delegate void SelectEvent(ItemTypeAsset asset);

        public SelectEvent OnAssetSelect;

        private Vector2 scroll;

        static public void Display(SelectEvent del)
        {
            var window = GetWindow<ItemTypeDialog>(true, windowTitle, true);
            window.OnAssetSelect = del;
            window.Show();
        }

        public void OnGUI()
        {
            scroll = GUILayout.BeginScrollView(scroll);
            for (int i = 0; i < ItemTypeDatabase.GetAssetCount(); i++)
            {
                var asset = ItemTypeDatabase.GetAt(i);
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