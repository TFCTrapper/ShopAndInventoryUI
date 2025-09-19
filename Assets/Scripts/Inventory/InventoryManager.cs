using System;
using System.Collections.Generic;
using Items;
using UnityEngine;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour, IInventoryManager
    {
        public Action<InventoryItem> InventoryItemCountChangedAction { get; set; }

        public List<InventoryItem> InventoryItems => _inventorySO.InventoryItems;
        
        [SerializeField] private InventorySO _inventorySO;

        public void AddItem(ItemSO item)
        {
            InventoryItem inventoryItem = InventoryItems.Find(x => x.ItemSO == item);
            if (inventoryItem == null)
            {
                InventoryItems.Add(new InventoryItem { ItemSO = item, Count = item.IsInfinite ? -1 : 1 });
            }
            else
            {
                inventoryItem.Count++;
            }
        }

        public InventoryItem GetInventoryItem(ItemSO item)
        {
            return InventoryItems.Find(x => x.ItemSO == item);
        }

        public void UseItem(InventoryItem inventoryItem)
        {
            if (inventoryItem.Count <= 0)
            {
                return;
            }

            var useContext = new UseContext();
            if (inventoryItem.ItemSO.CanUse(useContext, inventoryItem, out var reason))
            {
                if (!inventoryItem.ItemSO.IsInfinite)
                {
                    inventoryItem.Count--;
                
                    InventoryItemCountChangedAction?.Invoke(inventoryItem);
                
                    if (inventoryItem.Count <= 0)
                    {
                        InventoryItems.Remove(inventoryItem);
                    }
                }
                
                inventoryItem.ItemSO.Use(useContext, inventoryItem);
            }
        }
    }
}
