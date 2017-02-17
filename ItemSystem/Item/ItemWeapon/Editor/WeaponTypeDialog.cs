using UnityEngine;
using UnityEditor;
using Systems.ItemSystem.Database;

namespace Systems.WeaponSystem.Editor
{
    public class WeaponTypeDialog : EditorWindow
    {
        const string windowTitle = "Weapon Types";

        public delegate void SelectEvent(WeaponTypeAsset asset);

        public SelectEvent OnAssetSelect;

        private Vector2 scroll;

        static public void Display(SelectEvent del)
        {
            var window = GetWindow<WeaponTypeDialog>(true, windowTitle, true);
            window.OnAssetSelect = del;
            window.Show();
        }

        public void OnGUI()
        {
            scroll = GUILayout.BeginScrollView(scroll);
            for (int i = 0; i < WeaponTypeDatabase.GetAssetCount(); i++)
            {
                var asset = WeaponTypeDatabase.GetAt(i);
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