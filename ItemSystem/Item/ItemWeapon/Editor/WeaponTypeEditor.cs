using UnityEditor;
using UnityEngine;
using Systems.ItemSystem.Database;
using Systems.Config;

namespace Systems.ItemSystem.Editor
{
    public class WeaponTypeEditor : EditorWindow
    {
        [MenuItem("Window/Systems/Item System/Weapon System/Weapon Type Editor")]
        static public void ShowWindow()
        {
            var window = GetWindow<WeaponTypeEditor>();
            window.minSize = new Vector2(SystemsConfig.EDITOR_MIN_WINDOW_WIDTH, SystemsConfig.EDITOR_MIN_WINDOW_HEIGHT);
            window.titleContent.text = "Weapon Types";
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
            if (WeaponTypeDatabase.GetAssetCount() == 0)
            {
                Initialize();
            }
        }

        void Initialize()
        {
            WeaponTypeDatabase.Instance.Add(new WeaponTypeAsset(WeaponTypeDatabase.Instance.GetNextId(), "Primary"));
            WeaponTypeDatabase.Instance.Add(new WeaponTypeAsset(WeaponTypeDatabase.Instance.GetNextId(), "Secondary"));
            WeaponTypeDatabase.Instance.Add(new WeaponTypeAsset(WeaponTypeDatabase.Instance.GetNextId(), "Throwable"));
            WeaponTypeGenerator.CheckAndGenerateFile();
        }

        public void OnGUI()
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);

            for (int i = 0; i < WeaponTypeDatabase.GetAssetCount(); i++)
            {
                var asset = WeaponTypeDatabase.GetAt(i);
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

                    if(i > 2)
                    {
                        if (GUILayout.Button("-", EditorStyles.toolbarButton, GUILayout.Width(30)) && EditorUtility.DisplayDialog("Delete Weapon Type", "Are you sure you want to delete " + asset.Name + " Weapon Type?", "Delete", "Cancel"))
                        {
                            WeaponTypeDatabase.Instance.RemoveAt(i);
                        }
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
                        GUILayout.Label("Weapon Emblem", GUILayout.Width(72));
                        asset.Icon = (Sprite)EditorGUILayout.ObjectField(asset.Icon, typeof(Sprite), false, GUILayout.Width(72), GUILayout.Height(72));
                        GUILayout.EndVertical();   //end vertical

                        //INFO ON RIGHT OF HORIZONTAL
                        GUILayout.BeginVertical(); //begin vertical

                        if (asset.ID < 3)
                        {
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("Name", GUILayout.Width(80));
                            GUILayout.Label(asset.Name);
                            GUILayout.EndHorizontal();
                        }
                        else
                        {
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("Name", GUILayout.Width(80));
                            asset.Name = EditorGUILayout.TextField(asset.Name);
                            GUILayout.EndHorizontal();
                        }

                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Alias", GUILayout.Width(80));
                        asset.Alias = EditorGUILayout.TextField(asset.Alias);
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
                            EditorUtility.SetDirty(WeaponTypeDatabase.Instance);
                        }
                    }
                }
            }

            GUILayout.EndScrollView();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Add Type", EditorStyles.toolbarButton))
            {
                var newAsset = new WeaponTypeAsset(WeaponTypeDatabase.Instance.GetNextId());
                WeaponTypeDatabase.Instance.Add(newAsset);
            }

            if (GUILayout.Button("Generate WeaponType Enum", EditorStyles.toolbarButton))
            {
                WeaponTypeGenerator.CheckAndGenerateFile();
            }

            GUILayout.EndHorizontal();
        }
    }
}

