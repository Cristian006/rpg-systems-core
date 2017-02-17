using UnityEngine;
using System.Collections.Generic;
using Systems.Utility.Database;
using Systems.EntitySystem.Enumerations;

namespace Systems.EntitySystem.Database
{
    [System.Serializable]
    public class EntityAsset : BaseDatabaseAsset
    {
        [SerializeField]
        Sprite icon;
        [SerializeField]
        string description;
        [SerializeField]
        EntityType entityClass;
        [SerializeField]
        PlayerType playerType;
        [SerializeField]
        int startingLevel;
        [SerializeField]
        int cost;
        [SerializeField]
        bool locked;

        #region CONSTRUCTORS
        public EntityAsset() : base()
        {
            Icon = null;
            Description = string.Empty;
            EClass = EntityType.None;
            PType = PlayerType.None;
        }

        public EntityAsset(int id) : base(id)
        {
            Icon = null;
            Description = string.Empty;
            EClass = EntityType.None;
            PType = PlayerType.None;
        }

        public EntityAsset(int id, string name) : base(id, name)
        {
            Icon = null;
            Description = string.Empty;
            EClass = EntityType.None;
            PType = PlayerType.None;
        }

        public EntityAsset(int id, string name, string description, EntityType entityClass, PlayerType playerType) : base (id, name)
        {
            Icon = null;
            Description = description;
            EClass = entityClass;
            PType = playerType;
        }
        
        public EntityAsset(int id, string name, string description, EntityType entityClass, PlayerType playerType, int startLevel, int cost, bool locked) : base(id, name)
        {
            Icon = null;
            Description = description;
            EClass = entityClass;
            PType = playerType;
            StartLevel = startLevel;
            Cost = cost;
            Locked = locked;
        }
        #endregion

        #region GETTERS AND SETTERS
        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }

        public Sprite Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        public EntityType EClass
        {
            get
            {
                return entityClass;
            }

            set
            {
                entityClass = value;
            }
        }

        public PlayerType PType
        {
            get
            {
                return playerType;
            }

            set
            {
                playerType = value;
            }
        }

        public int StartLevel
        {
            get
            {
                return startingLevel;
            }

            set
            {
                startingLevel = value;
            }
        }

        public int Cost
        {
            get
            {
                return cost;
            }

            set
            {
                cost = value;
            }
        }

        public bool Locked
        {
            get
            {
                return locked;
            }

            set
            {
                locked = value;
            }
        }
        #endregion
    }
}