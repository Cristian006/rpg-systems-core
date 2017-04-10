using UnityEngine;
using Systems.EntitySystem.Enumerations;
using Systems.EntitySystem.Database;

namespace Systems.EntitySystem
{
    public class EntityData
    {
        public EntityData()
        {
            Name = string.Empty;
            Description = string.Empty;
            entityType = EntityType.None;
            playerType = PlayerType.None;
            Icon = null;
            ID = 0;
            Locked = false;
            StartLevel = 0;
        }

        public EntityData(EntityAsset entityAsset)
        {
            Name = entityAsset.Name;
            Description = entityAsset.Description;
            entityType = entityAsset.EClass;
            playerType = entityAsset.PType;
            Icon = entityAsset.Icon;
            ID = entityAsset.ID;
            Locked = entityAsset.Locked;
            StartLevel = entityAsset.StartLevel;
        }

        public Sprite Icon;
        public string Name;
        public int ID;
        public int StartLevel;
        public bool Locked;
        public string Description;
        public EntityType entityType;
        public PlayerType playerType;
    }
}