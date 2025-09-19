using System;
using Items;
using UnityEngine;

namespace Inventory
{
    public interface IInventoryManager
    {
        public Action<InventoryManager.InventoryItem> InventoryItemCountChangedAction { get; set; }
        public void AddItem(ItemSO item);
        public InventoryManager.InventoryItem GetInventoryItem(ItemSO item);
        public void UseItem(InventoryManager.InventoryItem inventoryItem);
    }
}
