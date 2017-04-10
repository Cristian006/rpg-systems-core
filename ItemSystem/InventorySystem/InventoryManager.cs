using UnityEngine;
using System;
using System.Collections.Generic;
using Byte.ItemSystem;

//TODO: Event handlers for weapon change
//TODO: Keep stacks updated over time so when you use up one the next is equipped
namespace Systems.ItemSystem.InventorySystem
{
    public abstract class InventoryManager : MonoBehaviour
    {
        [SerializeField]
        private Inventory _inventory;

        public EventHandler OnPrimaryChange;
        public EventHandler OnSecondaryChange;
        public EventHandler OnTertiaryChange;
        public EventHandler OnEquippedChange;

        public EventHandler OnItemRemovedSuccess;
        public EventHandler OnItemRemovdFailure;

        public EventHandler OnItemAddedSuccess;
        public EventHandler OnItemAddedFailure;

        public EventHandler OnInventoryChange;

        private int _primaryIndex = -1;
        private int _secondaryIndex = -1;
        private int _tertiaryIndex = -1;
        
        #region GETTERS AND SETTERS
        private Inventory inventory
        {
            get
            {
                if (_inventory == null)
                {
                    _inventory = new Inventory();
                }
                return _inventory;
            }
            set
            {
                _inventory = value;
            }
        }

        public int PrimaryIndex
        {
            get
            {
                return _primaryIndex;
            }
            set
            {
                _primaryIndex = value;
            }
        }

        public int SecondaryIndex
        {
            get
            {
                return _secondaryIndex;
            }

            set
            {
                _secondaryIndex = value;
            }
        }

        public int TertiaryIndex
        {
            get
            {
                return _tertiaryIndex;
            }

            set
            {
                _tertiaryIndex = value;
            }
        }

        public bool PrimarySlotEquipped
        {
            get
            {
                return PrimaryIndex >= 0 ? true : false;
            }
        }

        public bool SecondarySlotEquipped
        {
            get
            {
                return SecondaryIndex >= 0 ? true : false;
            }
        }

        public bool TertiarySlotEquipped
        {
            get
            {
                return TertiaryIndex >= 0 ? true : false;
            }
        }

        public virtual bool MultipleItemStacks
        {
            get
            {
                return true;
            }
        }
        #endregion

        #region PROPERTIES
        public Weapon Primary
        {
            get
            {
                return _primaryIndex >= 0 ? inventory.Objects<Weapon>().GetAt(_primaryIndex) : null;
            }
        }

        public Weapon Secondary
        {
            get
            {
                return _secondaryIndex >= 0 ? inventory.Objects<Weapon>().GetAt(_secondaryIndex) : null;
            }
        }

        public QuestItem Tertiary
        {
            get
            {
                return _tertiaryIndex >= 0 ? inventory.Objects<QuestItem>().GetAt(_tertiaryIndex) : null;
            }
        }

        public InventoryList<Weapon> Weapons
        {
            get
            {
                return inventory.Weapons;
            }
        }

        public InventoryList<Consumable> Consumables
        {
            get
            {
                return inventory.Consumables;
            }
        }

        public InventoryList<QuestItem> QuestItems
        {
            get
            {
                return inventory.QuestItems;
            }
        }

        /// <summary>
        /// The inventory's current weight
        /// </summary>
        public int CurrentWeight
        {
            get
            {
                return inventory.Weight;
            }
        }

        /// <summary>
        /// The inventory's Max Weight
        /// </summary>
        public abstract int MaxWeight
        {
            get;
        }

        /// <summary>
        /// The inventory's available weight
        /// </summary>
        public int AvailableWeight
        {
            get
            {
                return (MaxWeight - CurrentWeight);
            }
        }

        /// <summary>
        /// The respective inventory list count
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public int Count<T>() where T : Item
        {
            return inventory.Objects<T>().Count;
        }
        #endregion

        #region INVENTORY METHODS
        /// <summary>
        /// Add Item to inventory if there's enough room.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        public void Add<T>(T item) where T : Item
        {
            if (item.Stackable)
            {
                if (Contains<T>(item.ID))
                {
                    T i = GetOpenStack<T>(item.ID);
                    if(i == null)
                    {
                        //no available stack - Create a new one?
                        if (MultipleItemStacks)
                        {
                            if (item.Weight <= AvailableWeight)
                            {
                                //Create new stack if available weight;
                                inventory.Objects<T>().Add(item);
                                TriggerOnItemAdded(true);
                                return;
                            }
                            else
                            {
                                TriggerOnItemAdded(false);
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (item.Weight <= AvailableWeight)
                        {
                            int leftOver = (i.StackSize + item.StackSize) - i.MaxStack;
                            Debug.Log("LEFT OVER!: " + leftOver);
                            if(leftOver > 0 && MultipleItemStacks)
                            {
                                i.StackSize = i.MaxStack;
                                //Create new stacks of that item with the leftoverstacks
                                inventory.Objects<T>().Add((T)ItemFactory.itemFactory.GetItem<T>(item.Name, leftOver));
                                TriggerOnItemAdded(true);
                                i = null;
                                return;
                            }
                            else
                            {
                                i.StackSize = i.MaxStack;
                                //disgard rest of stack since we are not allowed to create multilple stacks of the same item
                                TriggerOnItemAdded(true);
                                i = null;
                                return;
                            }
                        }
                        else
                        {
                            TriggerOnItemAdded(false);
                            i = null;
                            return;
                        }
                    }        
                }
                else
                {
                    if (item.Weight <= AvailableWeight)
                    {
                        inventory.Objects<T>().Add(item);
                        TriggerOnItemAdded(true);
                        return;
                    }
                    else
                    {
                        TriggerOnItemAdded(false);
                        return;
                    }
                }
            }
            else
            {
                if (item.Weight <= AvailableWeight)
                {
                    inventory.Objects<T>().Add(item);
                    TriggerOnItemAdded(true);
                    return;
                }
                else
                {
                    TriggerOnItemAdded(false);
                    Debug.Log("Cannot add anymore Items, too much weight!");
                    return;
                }
            }
        }

        /// <summary>
        /// Remove Item from the inventory
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        public void Remove<T>(T item) where T : Item
        {
            if (item != null)
            {
                if (isEquipped(item))
                {
                    //un equip item
                    Equip(item, false);
                }
                UpdateEquippedIndexes(inventory.Objects<T>().Objects.IndexOf(item));
                inventory.Objects<T>().Remove(item);
                TriggerOnItemRemoved(true);
            }
            else
            {
                TriggerOnItemRemoved(false);
            }
        }

        /// <summary>
        /// Remove Item from the inventory at that index
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="index"></param>
        public void RemoveAt<T>(int index) where T : Item
        {
            CheckForUnEquip(index);
            UpdateEquippedIndexes(index);
            inventory.Objects<T>().RemoveAt(index);
            TriggerOnItemRemoved(true);
        }

        /// <summary>
        /// Replace the Item at that index if the difference in weight of the two items fit in the Inventory
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Replace<T>(int index, T item) where T : Item
        {
            float differnceInWeight = inventory.Objects<T>().GetAt(index).Weight - item.Weight;

            if ((inventory.Weight - differnceInWeight) <= MaxWeight)
            {
                inventory.Objects<T>().Replace(index, item);
                TriggerOnItemRemoved(true);
            }
            else
            {
                TriggerOnItemRemoved(false);
                Debug.Log("CANNOT REPLACE ITEM, TOO MUCH WEIGHT");
            }
        }

        public virtual void CheckForUnEquip(int index)
        {
            if (index == PrimaryIndex || index == SecondaryIndex)
            {
                //un equip item 
                Equip<Weapon>(index, false);
            }
            else if(index == TertiaryIndex)
            {
                Equip<QuestItem>(index, false);
            }
        }

        public virtual void UpdateEquippedIndexes(int index)
        {
            if(PrimaryIndex > index)
            {
                PrimaryIndex -= 1;
            }
            else if(SecondaryIndex > index)
            {
                SecondaryIndex -= 1;
            }
            else if(TertiaryIndex > index)
            {
                TertiaryIndex -= 1;
            }
        }

        public bool Contains<T>(T item) where T : Item
        {
            return inventory.Objects<T>().Contains(item);
        }

        public bool Contains<T>(int id) where T : Item
        {
            return inventory.Objects<T>().Contains(id);
        }

        public bool Contains<T>(string name) where T : Item
        {
            return inventory.Objects<T>().Contains(name);
        }

        public T GetAt<T>(int index) where T : Item
        {
            return inventory.Objects<T>().GetAt(index);
        }

        public T GetBy<T>(string name) where T : Item
        {
            return inventory.Objects<T>().GetBy(name);
        }

        public T GetBy<T>(int id) where T : Item
        {
            return inventory.Objects<T>().GetBy(id);
        }

        public T GetOpenStack<T>(string name) where T : Item
        {
            List<T> list = inventory.Objects<T>().GetAll(name);
            foreach(var i in list)
            {
                if (i.Stackable)
                {
                    if(i.StackSize < i.MaxStack)
                    {
                        return i;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    //don't need a list of them 
                    return null;
                }
            }
            return null;
        }

        public T GetOpenStack<T>(int id) where T : Item
        {
            List<T> list = inventory.Objects<T>().GetAll(id);
            foreach (var i in list)
            {
                if (i.Stackable)
                {
                    if (i.StackSize < i.MaxStack)
                    {
                        return i;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    //don't need a list of them 
                    return null;
                }
            }
            return null;
        }
        #endregion

        #region EQUIPPING METHODS
        public virtual bool isEquipped(Item item)
        {
            switch (item.IType)
            {
                case ItemType.Weapon:
                    if (((Weapon)item).WeaponType == WeaponType.Primary)
                    {
                        return PrimaryIndex == Weapons.Objects.IndexOf((Weapon)item);
                    }
                    else
                    {
                        return SecondaryIndex == Weapons.Objects.IndexOf((Weapon)item);
                    }
                case ItemType.Consumable:
                    return false;
                case ItemType.Quest:
                    return TertiaryIndex == QuestItems.Objects.IndexOf((QuestItem)item);
                default:
                    return false;
            }
        }

        public void Equip<T>(int index, bool equip = true) where T : Item
        {
            Equip(GetAt<T>(index), index, equip);
        }

        public void Equip(Item item, bool equip = true)
        {
            switch (item.IType)
            {
                case ItemType.Weapon:
                    Equip<Weapon>((Weapon)item, Weapons.Objects.IndexOf((Weapon)item), equip);
                    break;
                case ItemType.Consumable:
                    Equip<Consumable>((Consumable)item, Consumables.Objects.IndexOf((Consumable)item), equip);
                    break;
                case ItemType.Quest:
                    Equip<QuestItem>((QuestItem)item, QuestItems.Objects.IndexOf((QuestItem)item), equip);
                    break;
            }
        }
        
        /// <summary>
        /// Equip an Item
        /// false to unequip
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="index"></param>
        /// <param name="equip"></param>
        public virtual void Equip<T>(T item, int index, bool equip) where T : Item
        {
            if (item.IType == ItemType.Weapon)
            {
                //Debug.Log("Item trying to equip is a weapon");
                if ((item as Weapon).WeaponType == WeaponType.Primary)
                {
                    //Debug.Log("Setting it as a Primary Weapon");
                    PrimaryIndex = equip ? index : -1;
                    TriggerPrimaryChange();
                    TriggerOnEquippedChange();
                }
                else
                {
                    SecondaryIndex = equip ? index : -1;
                    TriggerSecondaryChange();
                    TriggerOnEquippedChange();
                }
            }
            else if (item.IType == ItemType.Quest)
            {
                TertiaryIndex = equip ? index : -1;
                TriggerTertiaryChange();
                TriggerOnEquippedChange();
            }
            else
            {
                Debug.Log("CANNOT EQUIP THIS TYPE OF ITEM");
            }
        }

        public void UnEquip(bool primary)
        {
            if (primary)
            {
                PrimaryIndex = -1;
                TriggerPrimaryChange();
                TriggerOnEquippedChange();
            }
            else
            {
                SecondaryIndex = -1;
                TriggerSecondaryChange();
                TriggerOnEquippedChange();
            }
        }
        #endregion

        #region EVENT HANDLERS
        protected void TriggerPrimaryChange()
        {
            if (OnPrimaryChange != null)
            {
                OnPrimaryChange(this, null);
            }
        }

        protected void TriggerSecondaryChange()
        {
            if (OnSecondaryChange != null)
            {
                OnSecondaryChange(this, null);
            }
        }

        protected void TriggerTertiaryChange()
        {
            if (OnTertiaryChange != null)
            {
                OnTertiaryChange(this, null);
            }
        }

        protected void TriggerOnItemAdded(bool itemWasAdded)
        {
            if (itemWasAdded)
            {
                TriggerInventoryChange();
                if (OnItemAddedSuccess != null)
                {
                    OnItemAddedSuccess(this, null);
                }
            }
            else
            {
                if (OnItemAddedFailure != null)
                {
                    OnItemAddedFailure(this, null);
                }
            }
        }
        
        protected void TriggerOnItemRemoved(bool itemWasRemoved)
        {
            if (itemWasRemoved)
            {
                TriggerInventoryChange();
                if (OnItemRemovedSuccess != null)
                {
                    OnItemRemovedSuccess(this, null);
                }
            }
            else
            {
                if (OnItemRemovdFailure != null)
                {
                    OnItemRemovedSuccess(this, null);
                }
            }

        }

        protected void TriggerInventoryChange()
        {
            if (OnInventoryChange != null)
            {
                OnInventoryChange(this, null);
            }
        }

        protected void TriggerOnEquippedChange()
        {
            if (OnEquippedChange != null)
            {
                OnEquippedChange(this, null);
            }
        }
        #endregion
    }
}