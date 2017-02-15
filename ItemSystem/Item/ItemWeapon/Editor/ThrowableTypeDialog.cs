using UnityEngine;
using UnityEditor;
using Systems.ItemSystem.Database;

namespace Systems.ItemSystem.Editor
{
    public class ThrowableTypeDialog : EditorWindow
    {
        const string windowTitle = "Throwable Types";

        public delegate void SelectEvent(ThrowableTypeAsset asset);

        public SelectEvent OnAssetSelect;

        private Vector2 scroll;

        static public void Display(SelectEvent del)
        {
            var window = GetWindow<ThrowableTypeDialog>(true, windowTitle, true);
            window.OnAssetSelect = del;
            window.Show();
        }

        public void OnGUI()
        {
            scroll = GUILayout.BeginScrollView(scroll);
            for(int i = 0; i < ThrowableTypeDatabase.GetAssetCount(); i++)
            {
                var asset = ThrowableTypeDatabase.GetAt(i);
                if(asset != null)
                {
                    if (GUILayout.Button(asset.Name, EditorStyles.toolbarButton))
                    {
                        if(OnAssetSelect != null)
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
