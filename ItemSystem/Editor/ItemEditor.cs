using UnityEngine;
using UnityEditor;
using Systems.Config;
using Systems.StatSystem;

namespace Systems.ItemSystem.Editor
{
    public partial class ItemEditor : EditorWindow
    {
        [MenuItem("Window/Systems/Item System/Item Editor %#I")]
        static public void ShowWindow()
        {
            var window = GetWindow<ItemEditor>();
            window.minSize = new Vector2(SystemsConfig.EDITOR_MIN_WINDOW_WIDTH, SystemsConfig.EDITOR_MIN_WINDOW_HEIGHT);
            window.titleContent.text = "Item Editor";
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
            tabState = TabState.ABOUT;
        }

        public void OnGUI()
        {
            EditorStyles.textField.wordWrap = true;
            TabBar();
            GUILayout.BeginHorizontal();
            switch (tabState)
            {
                case TabState.WEAPONS:
                    Weapons();
                    break;
                case TabState.CONSUMABLES:
                    Consumables();
                    break;
                case TabState.QUEST_ITEMS:
                    QuestItems();
                    break;
                default:
                    About();
                    break;
            }

            GUILayout.EndHorizontal();
            StatusBar();
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Create Weapon", EditorStyles.toolbarButton))
            {
                var newAsset = new WeaponAsset(WeaponDatabase.Instance.GetNextId());
                WeaponDatabase.Instance.Add(newAsset);
            }
            if (GUILayout.Button("Create Consumable", EditorStyles.toolbarButton))
            {
                var newAsset = new ConsumableAsset(ConsumableDatabase.Instance.GetNextId());
                ConsumableDatabase.Instance.Add(newAsset);
            }
            if (GUILayout.Button("Create Quest Item", EditorStyles.toolbarButton))
            {
                var newAsset = new QuestAsset(QuestDatabase.Instance.GetNextId());
                QuestDatabase.Instance.Add(newAsset);
            }
            GUILayout.EndHorizontal();
        }

        public virtual void Weapons()
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);

            for (int i = 0; i < WeaponDatabase.GetAssetCount(); i++)
            {
                WeaponAsset asset = WeaponDatabase.GetAt(i);
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

                    if (GUILayout.Button("x", EditorStyles.toolbarButton, GUILayout.Width(30)) && EditorUtility.DisplayDialog("Delete Stat Type", "Are you sure you want to delete " + asset.Name + " Weapon?", "Delete", "Cancel"))
                    {
                        WeaponDatabase.Instance.RemoveAt(i);
                    }

                    GUILayout.EndHorizontal();

                    if (activeID == asset.ID)
                    {
                        EditorGUI.BeginChangeCheck();

                        GUILayout.BeginVertical("Box");

                        GUILayout.BeginHorizontal();
                        //ITEM SPRITE
                        GUILayout.BeginVertical(GUILayout.Width(75)); //begin vertical
                        asset.Icon = (Sprite)EditorGUILayout.ObjectField(asset.Icon, typeof(Sprite), false, GUILayout.Width(72), GUILayout.Height(72));
                        GUILayout.Label("Item Sprite", GUILayout.Width(72));
                        GUILayout.EndVertical();   //end vertical

                        //ITEM CLASS
                        GUILayout.BeginVertical(); //begin vertical
                        GUILayout.Label("Item Class: " + asset.IType.ToString(), EditorStyles.boldLabel);

                        //NAME
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Name", GUILayout.Width(80));
                        asset.Name = EditorGUILayout.TextField(asset.Name);
                        GUILayout.EndHorizontal();

                        //DESCRIPTION
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Description", GUILayout.Width(80));
                        asset.Description = EditorGUILayout.TextArea(asset.Description, GUILayout.MinHeight(30));
                        GUILayout.EndHorizontal();

                        //COST
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Cost", GUILayout.Width(80));
                        asset.Cost = EditorGUILayout.IntField(asset.Cost);
                        GUILayout.EndHorizontal();

                        //STACKABLE
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Stackable", GUILayout.Width(80));
                        GUILayout.BeginVertical();
                        asset.Stackable = EditorGUILayout.Toggle(asset.Stackable);
                        if (asset.Stackable)
                        {
                            GUILayout.Box("Set the weight of this item to be the weight of what would be a \"full stack\"", EditorStyles.helpBox);
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("Max Stack Size", GUILayout.Width(80));
                            asset.StackSize = EditorGUILayout.IntSlider(asset.StackSize, 2, 99);
                            GUILayout.EndHorizontal();
                        }
                        GUILayout.EndVertical();
                        GUILayout.EndHorizontal();

                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Level", GUILayout.Width(80));
                        asset.Level = EditorGUILayout.IntSlider(asset.Level, 1, 99);
                        GUILayout.EndHorizontal();

                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Weight", GUILayout.Width(80));
                        asset.Weight = EditorGUILayout.IntSlider(asset.Weight, 0, 99);
                        GUILayout.EndHorizontal();

                        GUILayout.Label("Weapon Variables", EditorStyles.boldLabel);

                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Weap Type", GUILayout.Width(80));
                        asset.WType = (WeaponType)EditorGUILayout.EnumPopup(asset.WType);
                        GUILayout.EndHorizontal();

                        switchWeaponType(asset);
                        
                        GUILayout.Space(10);
                        GUILayout.EndVertical();

                        GUILayout.EndHorizontal();
                        
                        GUILayout.EndVertical();
                        if (EditorGUI.EndChangeCheck())
                        {
                            EditorUtility.SetDirty(WeaponDatabase.Instance);
                        }
                    }
                }
            }
            GUILayout.EndScrollView();
        }

        partial void switchWeaponType(WeaponAsset asset);

        partial void Primary(WeaponAsset asset);
        
        partial void Secondary(WeaponAsset asset);

        partial void Throw(WeaponAsset asset);

        public virtual void Consumables()
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);

            for (int i = 0; i < ConsumableDatabase.GetAssetCount(); i++)
            {
                ConsumableAsset asset = ConsumableDatabase.GetAt(i);
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

                    if (GUILayout.Button("x", EditorStyles.toolbarButton, GUILayout.Width(30)) && EditorUtility.DisplayDialog("Delete Consumable Item", "Are you sure you want to delete " + asset.Name + " Consumable?", "Delete", "Cancel"))
                    {
                        ConsumableDatabase.Instance.RemoveAt(i);
                    }

                    GUILayout.EndHorizontal();

                    if (activeID == asset.ID)
                    {
                        EditorGUI.BeginChangeCheck();

                        GUILayout.BeginVertical("Box");

                        GUILayout.BeginHorizontal();
                        //ITEM SPRITE
                        GUILayout.BeginVertical(GUILayout.Width(75)); //begin vertical
                        asset.Icon = (Sprite)EditorGUILayout.ObjectField(asset.Icon, typeof(Sprite), false, GUILayout.Width(72), GUILayout.Height(72));
                        GUILayout.Label("Item Sprite", GUILayout.Width(72));
                        GUILayout.EndVertical();   //end vertical

                        //ITEM CLASS
                        GUILayout.BeginVertical(); //begin vertical
                        GUILayout.Label("Item Class: " + asset.IType.ToString(), EditorStyles.boldLabel);

                        //NAME
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Name", GUILayout.Width(80));
                        asset.Name = EditorGUILayout.TextField(asset.Name);
                        GUILayout.EndHorizontal();

                        //DESCRIPTION
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Description", GUILayout.Width(80));
                        asset.Description = EditorGUILayout.TextArea(asset.Description, GUILayout.MinHeight(30));
                        GUILayout.EndHorizontal();

                        //COST
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Cost", GUILayout.Width(80));
                        asset.Cost = EditorGUILayout.IntField(asset.Cost);
                        GUILayout.EndHorizontal();

                        //STACKABLE
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Stackable", GUILayout.Width(80));
                        GUILayout.BeginVertical();
                        asset.Stackable = EditorGUILayout.BeginToggleGroup("Enabled", asset.Stackable);
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Stack Size", GUILayout.Width(80));
                        asset.StackSize = EditorGUILayout.IntSlider(asset.StackSize, 2, 64);
                        GUILayout.EndHorizontal();
                        GUILayout.EndVertical();
                        GUILayout.EndHorizontal();
                        EditorGUILayout.EndToggleGroup();

                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Level", GUILayout.Width(80));
                        asset.Level = EditorGUILayout.IntSlider(asset.Level, 1, 99);
                        GUILayout.EndHorizontal();

                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Weight", GUILayout.Width(80));
                        asset.Weight = EditorGUILayout.IntSlider(asset.Weight, 0, 99);
                        GUILayout.EndHorizontal();

                        GUILayout.Label("Consumable Variables", EditorStyles.boldLabel);

                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Stat To Effect", GUILayout.Width(80));
                        asset.StatToEffect = (StatType)EditorGUILayout.EnumPopup(asset.StatToEffect);
                        GUILayout.EndHorizontal();

                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Effect Amount", GUILayout.Width(80));
                        asset.EffectAmount = EditorGUILayout.IntField(asset.EffectAmount);
                        GUILayout.EndHorizontal();

                        GUILayout.EndVertical();

                        GUILayout.EndHorizontal();

                        GUILayout.EndVertical();
                        if (EditorGUI.EndChangeCheck())
                        {
                            EditorUtility.SetDirty(ConsumableDatabase.Instance);
                        }
                    }
                }
            }
            GUILayout.EndScrollView();
        }

        public virtual void QuestItems()
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);

            for (int i = 0; i < QuestDatabase.GetAssetCount(); i++)
            {
                QuestAsset asset = QuestDatabase.GetAt(i);
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

                    if (GUILayout.Button("x", EditorStyles.toolbarButton, GUILayout.Width(30)) && EditorUtility.DisplayDialog("Delete Quest Item", "Are you sure you want to delete " + asset.Name + " Consumable?", "Delete", "Cancel"))
                    {
                        QuestDatabase.Instance.RemoveAt(i);
                    }

                    GUILayout.EndHorizontal();

                    if (activeID == asset.ID)
                    {
                        EditorGUI.BeginChangeCheck();

                        GUILayout.BeginVertical("Box");

                        GUILayout.BeginHorizontal();
                        //ITEM SPRITE
                        GUILayout.BeginVertical(GUILayout.Width(75)); //begin vertical
                        asset.Icon = (Sprite)EditorGUILayout.ObjectField(asset.Icon, typeof(Sprite), false, GUILayout.Width(72), GUILayout.Height(72));
                        GUILayout.Label("Item Sprite", GUILayout.Width(72));
                        GUILayout.EndVertical();   //end vertical

                        //ITEM CLASS
                        GUILayout.BeginVertical(); //begin vertical
                        GUILayout.Label("Item Class: " + asset.IType.ToString(), EditorStyles.boldLabel);

                        //NAME
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Name", GUILayout.Width(80));
                        asset.Name = EditorGUILayout.TextField(asset.Name);
                        GUILayout.EndHorizontal();

                        //DESCRIPTION
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Description", GUILayout.Width(80));
                        asset.Description = EditorGUILayout.TextArea(asset.Description, GUILayout.MinHeight(30));
                        GUILayout.EndHorizontal();

                        //COST
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Cost", GUILayout.Width(80));
                        asset.Cost = EditorGUILayout.IntField(asset.Cost);
                        GUILayout.EndHorizontal();

                        //STACKABLE
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Stackable", GUILayout.Width(80));
                        GUILayout.BeginVertical();
                        asset.Stackable = EditorGUILayout.BeginToggleGroup("Enabled", asset.Stackable);
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Stack Size", GUILayout.Width(80));
                        asset.StackSize = EditorGUILayout.IntSlider(asset.StackSize, 2, 64);
                        GUILayout.EndHorizontal();
                        GUILayout.EndVertical();
                        GUILayout.EndHorizontal();
                        EditorGUILayout.EndToggleGroup();

                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Level", GUILayout.Width(80));
                        asset.Level = EditorGUILayout.IntSlider(asset.Level, 1, 99);
                        GUILayout.EndHorizontal();

                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Weight", GUILayout.Width(80));
                        asset.Weight = EditorGUILayout.IntSlider(asset.Weight, 0, 99);
                        GUILayout.EndHorizontal();

                        GUILayout.Label("QuestItem Variables", EditorStyles.boldLabel);
                        
                        GUILayout.EndVertical();

                        GUILayout.EndHorizontal();

                        GUILayout.EndVertical();
                        if (EditorGUI.EndChangeCheck())
                        {
                            EditorUtility.SetDirty(QuestDatabase.Instance);
                        }
                    }
                }
            }
            GUILayout.EndScrollView();
        }

        public void About()
        {
            EditorGUILayout.LabelField("This is an Item Editor to create the three different types of items and save them to there corresponding asset databases using scriptable objects.");
        }
    }
}