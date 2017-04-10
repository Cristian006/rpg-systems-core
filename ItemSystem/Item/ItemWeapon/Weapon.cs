using Systems.ItemSystem.Interfaces;

namespace Systems.ItemSystem
{
    public class Weapon : Item, IDestructible, IEquipable
    {
        private int _durability;
        private int _currentDurablity;
        private int _power;
        private bool _equipable;
        private WeaponType _weaponType;

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

        public int Power
        {
            get
            {
                return _power;
            }
            set
            {
                _power = value;
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
            Durability = wa.Durability;
            WeaponType = wa.WType;
            Power = wa.Power;
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