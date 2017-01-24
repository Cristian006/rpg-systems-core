using UnityEngine;
using Systems.EntitySystem.Enumerations;
using Systems.EntitySystem.Database;

namespace Systems.EntitySystem
{
    public class EntityData
    {
        public EntityData()
        {
            entityName = string.Empty;
            entityType = EntityType.None;
            playerType = PlayerType.None;
        }

        public EntityData(EntityAsset entityAsset)
        {
            entityName = entityAsset.Name;
            entityDescription = entityAsset.Description;
            entityType = entityAsset.EClass;
            playerType = entityAsset.PType;
            entityImage = entityAsset.Icon;
        }

        public Sprite entityImage;
        public string entityName;
        public string entityDescription;
        public EntityType entityType;
        public PlayerType playerType;
    }

    //Entity is simply a script that has an associated StatCollection
    //Gives children a method to get the stats it needs
    public abstract class Entity : MonoBehaviour
    {
        private EntityData data;

        public EntityData Data
        {
            get
            {
                if (data == null)
                {
                    data = new EntityData();
                }
                return data;
            }
            set
            {
                data = value;
            }
        }
    }
}