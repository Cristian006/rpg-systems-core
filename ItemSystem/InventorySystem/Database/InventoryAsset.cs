using UnityEngine;
using System.Collections.Generic;
using Systems.Utility.Database;

namespace Systems.ItemSystem.InventorySystem.Database
{
    [System.Serializable]
    public class InventoryItem
    {        
        [SerializeField]
        private string name;
        [SerializeField]
        private ItemType itemType;
        [SerializeField]
        private int itemCount;

        public InventoryItem()
        {
            Name = string.Empty;
            ItemType = ItemType.None;
            ItemCount = 1;
        }

        public InventoryItem(string name, ItemType it, int itemCount)
        {
            Name = name;
            ItemType = it;
            ItemCount = itemCount;
        }


        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public ItemType ItemType
        {
            get
            {
                return itemType;
            }

            set
            {
                itemType = value;
            }
        }

        public int ItemCount
        {
            get
            {
                return itemCount;
            }

            set
            {
                itemCount = value;
            }
        }
    }

    [System.Serializable]
    public class InventoryAsset : BaseDatabaseAsset
    {
        [SerializeField]
        List<InventoryItem> inventory;

        #region CONSTRUCTORS
        public InventoryAsset() : base()
        {
            Inventory = new List<InventoryItem>();
        }

        public InventoryAsset(int id) : base(id)
        {
            Inventory = new List<InventoryItem>();
        }

        public InventoryAsset(int id, string name) : base(id, name)
        {
            Inventory = new List<InventoryItem>();
        }

        public InventoryAsset(int id, string name, List<InventoryItem> inv) : base (id, name)
        {
            Inventory = inv;
        }
        #endregion

        #region GETTERS AND SETTERS
        public List<InventoryItem> Inventory
        {
            get
            {
                return inventory;
            }

            set
            {
                inventory = value;
            }
        }
        #endregion
    }
}