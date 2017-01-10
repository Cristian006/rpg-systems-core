﻿using UnityEngine;
using Systems.StatSystem;
using UnityEngine.UI;
using System.Collections;
using Systems.EntitySystem.Interfaces;
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

    public class Entity : MonoBehaviour, ITarget
    {
        private StatCollection stats;
        private EntityLevel level;
        private EntityData data;
        private Target _target;

        public StatCollection Stats
        {
            get
            {
                if(stats == null)
                {
                    stats = GetComponent<StatCollection>();
                }

                return stats;
            }
            set { stats = value; }
        }

        public Target target
        {
            get
            {
                if (_target == null)
                {
                    _target = new Target();
                }
                return _target;
            }
        }

        public EntityLevel Level
        {
            get
            {
                if(level == null)
                {
                    level = GetComponent<EntityLevel>();
                }
                return level;
            }
            set
            {
                level = value;
            }
        }

        public EntityData Data {
            get
            {
                if(data == null)
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

        void Awake()
        {
            
        }

        public void TakeDamage(int amount)
        {
            if (stats.GetStat<StatVital>(StatType.Armor).CurrentValue > 0)
            {
                int amountToTakeFromArmor = (int)(amount * (0.01f * stats.GetStat<StatAttribute>(StatType.ArmorProtection).Value));
                int amountToTakeFromHealth = (amount - amountToTakeFromArmor);
                int amountLeft = 0;

                if (stats.GetStat<StatVital>(StatType.Armor).CurrentValue >= amountToTakeFromArmor)
                {
                    stats.GetStat<StatVital>(StatType.Armor).CurrentValue -= amountToTakeFromArmor;
                }
                else
                {
                    amountLeft = amountToTakeFromArmor - stats.GetStat<StatVital>(StatType.Armor).CurrentValue;
                    stats.GetStat<StatVital>(StatType.Armor).CurrentValue = 0;
                }

                stats.GetStat<StatVital>(StatType.Health).CurrentValue -= (amountToTakeFromHealth + amountLeft);
            }
        }

        public void RestoreHealth(int amount)
        {
            stats.GetStat<StatRegeneratable>(StatType.Health).CurrentValue += amount;
        }

        public void RestoreHealth()
        {
            stats.GetStat<StatRegeneratable>(StatType.Health).RestoreCurrentValueToMax();
        }
    }
}