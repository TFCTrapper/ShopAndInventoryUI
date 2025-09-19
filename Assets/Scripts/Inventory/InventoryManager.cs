using System;
using System.Collections.Generic;
using Items;
using UnityEngine;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour, IInventoryManager
    {
        [Serializable]
        public class InventoryItem
        {
            public ItemSO Item { get => _item; set => _item = value; }
            public int Count { get => _count; set => _count = value; }
            
            [SerializeField] private ItemSO _item;
            [SerializeField] private int _count;
        }

        public Action<InventoryItem> InventoryItemCountChangedAction { get; set; }

        public List<InventoryItem> InventoryItems => _inventoryItems;
        
        [SerializeField] private List<InventoryItem> _inventoryItems;

        public void AddItem(ItemSO item)
        {
            InventoryItem inventoryItem = _inventoryItems.Find(x => x.Item == item);
            if (inventoryItem == null)
            {
                _inventoryItems.Add(new InventoryItem { Item = item, Count = item.IsInfinite ? -1 : 1 });
            }
            else
            {
                inventoryItem.Count++;
                InventoryItemCountChangedAction?.Invoke(inventoryItem);
            }
        }

        public InventoryItem GetInventoryItem(ItemSO item)
        {
            return _inventoryItems.Find(x => x.Item == item);
        }

        public void UseItem(InventoryItem inventoryItem)
        {
            if (!inventoryItem.Item.IsInfinite)
            {
                inventoryItem.Count--;
                //TODO: Show -- animation
                if (inventoryItem.Count <= 0)
                {
                    _inventoryItems.Remove(inventoryItem);
                    //TODO: Show removal animation
                }
            }
        }
    }
}
