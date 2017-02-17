using UnityEditor;
using UnityEngine;
using Systems.ItemSystem.InventorySystem.Database;
using Systems.Config;

namespace Systems.ItemSystem.InventorySystem.Editor
{
    public class InventoryEditor : EditorWindow
    {
        [MenuItem("Window/Systems/Item System/Inventory System/Inventory Editor")]
        static public void ShowWindow()
        {
            var window = GetWindow<InventoryEditor>();
            window.minSize = new Vector2(SystemsConfig.EDITOR_MIN_WINDOW_WIDTH, SystemsConfig.EDITOR_MIN_WINDOW_HEIGHT);
            window.titleContent.text = "Inventory Editor";
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

            for (int i = 0; i < InventoryDatabase.GetAssetCount(); i++)
            {
                InventoryAsset asset = InventoryDatabase.GetAt(i);
                if(asset != null)
                {
                    GUILayout.BeginHorizontal(EditorStyles.toolbar);
                    GUILayout.Label(string.Format("ID: {0}", asset.ID.ToString("D3")), GUILayout.Width(60));

                    bool clicked = GUILayout.Toggle(asset.ID == activeID, asset.Name, ToggleButtonStyle);

                    if(clicked != (asset.ID == activeID))
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

                    if(GUILayout.Button("-", EditorStyles.toolbarButton, GUILayout.Width(30)) && EditorUtility.DisplayDialog("Delete Inventory", "Are you sure you want to delete " + asset.Name + "'s default Inventory?", "Delete", "Cancel"))
                    {
                        InventoryDatabase.Instance.RemoveAt(i);
                    }

                    GUILayout.EndHorizontal();

                    if(activeID == asset.ID)
                    {
                        EditorGUI.BeginChangeCheck();

                        //START OF SELECTED VIEW
                        GUILayout.BeginVertical("Box");  //a

                        //SPRITE ON LEFT OF HORIZONTAL
                        
                        GUILayout.BeginVertical(); //d

                        GUILayout.BeginHorizontal();  //e
                        GUILayout.Label("Entity's Name", GUILayout.Width(80));
                        asset.Name = EditorGUILayout.TextField(asset.Name);
                        GUILayout.EndHorizontal();   //e

                        GUILayout.Space(10);

                        GUILayout.BeginVertical("Box");

                        GUILayout.Label(string.Format("Inventory: Contains {0} Item(s)", asset.Inventory.Count));

                        GUILayout.Space(2);

                        for (int d = 0; d < asset.Inventory.Count; d++)
                        {
                            GUILayout.BeginVertical(EditorStyles.helpBox);

                            GUILayout.BeginHorizontal();
                            GUILayout.Label(string.Format("{0}:Item Name", d.ToString("D2"), GUILayout.Width(60)));
                            asset.Inventory[d].Name = EditorGUILayout.TextField(asset.Inventory[d].Name, GUILayout.MinWidth(300));
                            GUILayout.FlexibleSpace();
                            if (GUILayout.Button("x", EditorStyles.toolbarButton, GUILayout.Width(30)) && EditorUtility.DisplayDialog("Delete Item", "Are you sure you want to delete " + d + ": " + asset.Inventory[d].Name + " Item?", "Delete", "Cancel"))
                            {
                                asset.Inventory.RemoveAt(d);
                                return;
                            }
                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();
                            GUILayout.Label("Item Type", GUILayout.Width(80));
                            asset.Inventory[d].ItemType = (ItemType)EditorGUILayout.EnumPopup(asset.Inventory[d].ItemType);
                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();
                            GUILayout.Label("Item Count", GUILayout.Width(80));
                            asset.Inventory[d].ItemCount = EditorGUILayout.IntSlider(asset.Inventory[d].ItemCount, 1, 99);
                            GUILayout.EndHorizontal();

                            GUILayout.EndVertical();
                        }
                        GUILayout.EndVertical();

                        GUILayout.Space(20);

                        
                        GUILayout.EndVertical();  //d

                        GUILayout.BeginHorizontal();
                        if (GUILayout.Button("Add Defualt Item to Inventory", EditorStyles.toolbarButton))
                        {
                            asset.Inventory.Add(new InventoryItem());
                        }
                        GUILayout.EndHorizontal();

                        GUILayout.EndVertical();  //a
                        
                        if (EditorGUI.EndChangeCheck())
                        {
                            EditorUtility.SetDirty(InventoryDatabase.Instance);
                        }
                    }
                }
            }

            GUILayout.EndScrollView();

            GUILayout.BeginHorizontal();
            if(GUILayout.Button("New Inventory", EditorStyles.toolbarButton))
            {
                var newAsset = new InventoryAsset(InventoryDatabase.Instance.GetNextId());
                InventoryDatabase.Instance.Add(newAsset);
            }
            GUILayout.EndHorizontal();
        }
    }
}