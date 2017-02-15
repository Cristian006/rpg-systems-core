using UnityEngine;
using Systems.Utility.Database;

namespace Systems.ItemSystem.Database
{
    [System.Serializable]
    public class ThrowableTypeAsset : BaseDatabaseAsset
    {
        [SerializeField]
        private string _alias;

        [SerializeField]
        private string _description;

        [SerializeField]
        private Sprite _icon;

        public string Alias
        {
            get
            {
                return _alias;
            }

            set
            {
                _alias = value;
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

        public ThrowableTypeAsset() : base()
        {
            this.Alias = string.Empty;
            this.Description = string.Empty;
            this.Icon = null;
        }

        public ThrowableTypeAsset(int id) : base(id)
        {
            this.Alias = string.Empty;
            this.Description = string.Empty;
            this.Icon = null;
        }

        public ThrowableTypeAsset(int id, string name) : base(id, name)
        {
            this.Alias = string.Empty;
            this.Description = string.Empty;
            this.Icon = null;
        }

        public ThrowableTypeAsset(int id, string name, string shortName, string description, Sprite icon) : base(id, name)
        {
            this.Alias = shortName;
            this.Description = description;
            this.Icon = icon;
        }
    }
}