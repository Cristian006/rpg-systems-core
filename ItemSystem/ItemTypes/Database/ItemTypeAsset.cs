using UnityEngine;
using Systems.Utility.Database;

namespace Systems.ItemSystem.Database
{
    [System.Serializable]
    public class ItemTypeAsset : BaseDatabaseAsset
    {
        [SerializeField]
        private string _shortName;

        [SerializeField]
        private string _description;

        [SerializeField]
        private Sprite _icon;

        public string ShortName
        {
            get
            {
                return _shortName;
            }

            set
            {
                _shortName = value;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                _description = value;
            }
        }

        public Sprite Icon
        {
            get
            {
                return _icon;
            }

            set
            {
                _icon = value;
            }
        }

        public ItemTypeAsset() : base()
        {
            this.ShortName = string.Empty;
            this.Description = string.Empty;
            this.Icon = null;
        }

        public ItemTypeAsset(int id) : base(id)
        {
            this.ShortName = string.Empty;
            this.Description = string.Empty;
            this.Icon = null;
        }

        public ItemTypeAsset(int id, string name) : base(id, name)
        {
            this.ShortName = string.Empty;
            this.Description = string.Empty;
            this.Icon = null;
        }

        public ItemTypeAsset(int id, string name, string shortName, string description, Sprite icon) : base(id, name)
        {
            this.ShortName = shortName;
            this.Description = description;
            this.Icon = icon;
        }
    }
}