using UnityEngine;
using UnityEditor;
using Systems.ItemSystem.Database;
using Systems.Config;

namespace Systems.ItemSystem.Editor
{
    public class ItemTypeEditorWindow : EditorWindow
    {
        [MenuItem("Window/Systems/Item System/Item Type Editor %#W")]
        static public void ShowWindow()
        {
            var window = GetWindow<ItemTypeEditorWindow>();
            window.minSize = new Vector2(SystemsConfig.EDITOR_MIN_WINDOW_WIDTH, SystemsConfig.EDITOR_MIN_WINDOW_HEIGHT);
            window.titleContent.text = "Item System - Item Types";
            window.Show();
        }

        private Vector2 scrollPosition;
        private int activeID;

        private GUIStyle _toggleButtonStyle;
        private GUIStyle ToggleButtonStyle
        {
            get
            {
                if (_toggleButtonStyle == null)
                {
                    _toggleButtonStyle = new GUIStyle(EditorStyles.toolbarButton);
                    ToggleButtonStyle.alignment = TextAnchor.MiddleLeft;
                }
                return _toggleButtonStyle;
            }
        }

        public void OnEnable()
        {

        }

        public void OnGUI()
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);

            for (int i = 0; i < ItemTypeDatabase.GetAssetCount(); i++)
            {
                var asset = ItemTypeDatabase.GetAt(i);
                if (asset != null)
                {
                    GUILayout.BeginHorizontal(EditorStyles.toolbar);
                    GUILayout.Label(string.Format("ID: {0}", asset.ID.ToString("D3")), GUILayout.Width(60));

                    bool clicked = GUILayout.Toggle(asset.ID == activeID, asset.Name, ToggleButtonStyle);

                    if (clicked != (asset.ID == activeID))
                    {
                        if (clicked)
                        {
                            activeID = asset.ID;
                            GUI.FocusControl(null);
                        }
                        else
                        {
                            activeID = -1;
                        }
                    }

                    if (GUILayout.Button("-", EditorStyles.toolbarButton, GUILayout.Width(30)) && EditorUtility.DisplayDialog("Delete Item Type", "Are you sure you want to delete " + asset.Name + " Item Type?", "Delete", "Cancel"))
                    {
                        ItemTypeDatabase.Instance.RemoveAt(i);
                    }

                    GUILayout.EndHorizontal();

                    if (activeID == asset.ID)
                    {
                        EditorGUI.BeginChangeCheck();

                        //START OF SELECTED VIEW
                        GUILayout.BeginVertical("Box");
                        GUILayout.BeginHorizontal();
                        //SPRITE ON LEFT OF HORIZONTAL
                        GUILayout.BeginVertical(GUILayout.Width(75)); //begin vertical
                        GUILayout.Label("Item Sprite", GUILayout.Width(72));
                        asset.Icon = (Sprite)EditorGUILayout.ObjectField(asset.Icon, typeof(Sprite), false, GUILayout.Width(72), GUILayout.Height(72));
                        GUILayout.EndVertical();   //end vertical

                        //INFO ON RIGHT OF HORIZONTAL
                        GUILayout.BeginVertical(); //begin vertical

                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Name", GUILayout.Width(80));
                        asset.Name = EditorGUILayout.TextField(asset.Name);
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Short Name", GUILayout.Width(80));
                        asset.ShortName = EditorGUILayout.TextField(asset.ShortName);
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Description", GUILayout.Width(80));
                        asset.Description = EditorGUILayout.TextArea(asset.Description, GUILayout.MinHeight(50));
                        GUILayout.EndHorizontal();

                        GUILayout.EndVertical();  //end vertical

                        GUILayout.EndHorizontal();
                        GUILayout.EndVertical();

                        if (EditorGUI.EndChangeCheck())
                        {
                            EditorUtility.SetDirty(ItemTypeDatabase.Instance);
                        }
                    }
                }
            }

            GUILayout.EndScrollView();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Add Type", EditorStyles.toolbarButton))
            {
                var newAsset = new ItemTypeAsset(ItemTypeDatabase.Instance.GetNextId());
                ItemTypeDatabase.Instance.Add(newAsset);
            }

            if (GUILayout.Button("Generate ItemType Enum", EditorStyles.toolbarButton))
            {
                ItemTypeGenerator.CheckAndGenerateFile();
            }

            GUILayout.EndHorizontal();
        }
    }
}