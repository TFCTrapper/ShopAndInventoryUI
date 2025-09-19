using System;
using Items;
using UnityEngine;

namespace Inventory
{
    public interface IInventoryManager
    {
        public Action<InventoryItem> InventoryItemCountChangedAction { get; set; }
        public void AddItem(ItemSO item);
        public InventoryItem GetInventoryItem(ItemSO item);
        public void UseItem(InventoryItem inventoryItem);
    }
}
