using UnityEngine;
using System.Collections;
using Systems.ItemSystem.Interfaces;

namespace Systems.ItemSystem
{
    public class Weapon : Item, IDestructible, IEquipable
    {
        private int _durability;
        private int _currentDurablity;
        private int _damage;
        private bool _equipable;
        private WeaponType _weaponType;
        private float _force;

        #region GETTERS AND SETTERS
        public int CurrentDurability
        {
            get
            {
                return _currentDurablity;
            }
            set
            {
                _currentDurablity = value;
                if (_currentDurablity <= 0)
                {
                    _currentDurablity = 0;
                    Break();
                }
            }
        }

        public int Durability
        {
            get
            {
                return _durability;
            }

            set
            {
                _durability = value;
            }
        }

        public int Damage
        {
            get
            {
                return _damage;
            }
            set
            {
                _damage = value;
            }
        }

        public bool Equipable
        {
            get
            {
                return _equipable;
            }
            protected set
            {
                _equipable = value;
            }
        }

        public float Force
        {
            get
            {
                return _force;
            }
            protected set
            {
                _force = value;
            }
        }

        public WeaponType WeaponType
        {
            get
            {
                return _weaponType;
            }

            protected set
            {
                _weaponType = value;
            }
        }
        #endregion

        public Weapon(WeaponAsset wa) : base((ItemAsset)wa)
        {
            this.Durability = wa.Durability;
            this.WeaponType = wa.WType;
            this.Damage = wa.WeaponDamage;
            this.Force = wa.Force;
        }

        public void Break()
        {
            _equipable = false;
        }

        public void Repair()
        {
            _equipable = true;
            CurrentDurability = Durability;
        }

        public void TakeDamage(int amount)
        {
            CurrentDurability -= amount;
        }
    }
}