using UnityEngine;

namespace Systems.ItemSystem
{
    public class Item
    {
        private int _id;
        private string _name;
        private ItemType _iType;
        private string _description;
        private float _weight;
        private bool _stackable;
        private int _stackSize;
        private int _maxStack;
        private Sprite _icon;
        private int _level;
        private int _cost;

        #region GETTERS AND SETTERS
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public bool Stackable
        {
            get { return _stackable; }
            set { _stackable = value; }
        }

        public ItemType IType
        {
            get
            {
                return _iType;
            }

            set
            {
                _iType = value;
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

        public float Weight
        {
            get
            {
                if (Stackable)
                {
                    if(StackSize > 0)
                    {
                        return Mathf.Clamp(Mathf.FloorToInt(StackSize * _weight), 1, float.MaxValue);
                    }
                    else
                    {
                        Debug.Log("THIS ITEM SHOULD NOT BE HERE - LOOK AT CODE AND FIX THIS");
                        return _weight;
                    }
                }
                else
                {
                    return Mathf.FloorToInt(_weight);
                }
            }
            set
            {
                _weight = value;
            }
        }

        public int Cost
        {
            get
            {
                return _cost;
            }

            set
            {
                _cost = value;
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

        public int StackSize
        {
            get
            {
                return _stackSize;
            }
            set
            {
                _stackSize = value;
            }
        }

        public int MaxStack
        {
            get
            {
                return _maxStack;
            }
            set
            {
                _maxStack = value;
            }
        }
        #endregion

        #region CONSTRUCTORS
        public Item()
        {
            //Empty Constructor
        }

        public Item(ItemAsset ia)
        {
            ID = ia.ID;
            Name = ia.Name;
            IType = ia.IType;
            Weight = ia.Weight;
            Description = ia.Description;
            Stackable = ia.Stackable;
            MaxStack = ia.StackSize;
            Icon = ia.Icon;
            Level = ia.Level;
            Cost = ia.Cost;
        }
        #endregion
    }
}