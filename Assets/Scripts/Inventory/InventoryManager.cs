using System;
using System.Collections.Generic;
using Items;
using UnityEngine;
using UnityEngine.Serialization;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour, IInventoryManager
    {
        [Serializable]
        public class InventoryItem
        {
            public ItemSO ItemSO { get => _itemSO; set => _itemSO = value; }
            public int Count { get => _count; set => _count = value; }
            public float LastUseTime { get => _lastUseTime; set => _lastUseTime = value; }
            
            [SerializeField] private ItemSO _itemSO;
            [SerializeField] private int _count;
            [SerializeField] private float _lastUseTime;
            
            public bool OnCooldown(float currentTime) =>
                _itemSO != null && (currentTime - _lastUseTime) < _itemSO.Cooldown;
        }

        public Action<InventoryItem> InventoryItemCountChangedAction { get; set; }

        public List<InventoryItem> InventoryItems => _inventoryItems;
        
        [SerializeField] private List<InventoryItem> _inventoryItems;

        public void AddItem(ItemSO item)
        {
            InventoryItem inventoryItem = _inventoryItems.Find(x => x.ItemSO == item);
            if (inventoryItem == null)
            {
                _inventoryItems.Add(new InventoryItem { ItemSO = item, Count = item.IsInfinite ? -1 : 1 });
            }
            else
            {
                inventoryItem.Count++;
                InventoryItemCountChangedAction?.Invoke(inventoryItem);
            }
        }

        public InventoryItem GetInventoryItem(ItemSO item)
        {
            return _inventoryItems.Find(x => x.ItemSO == item);
        }

        public void UseItem(InventoryItem inventoryItem)
        {
            if (inventoryItem.Count <= 0)
            {
                return;
            }
            
            if (!inventoryItem.ItemSO.IsInfinite)
            {
                inventoryItem.Count--;
                
                if (inventoryItem.Count <= 0)
                {
                    _inventoryItems.Remove(inventoryItem);
                }
            }

            var useContext = new UseContext();
            if (inventoryItem.ItemSO.CanUse(useContext, inventoryItem, out var reason))
            {
                inventoryItem.ItemSO.Use(useContext, inventoryItem);
            }
        }
    }
}
