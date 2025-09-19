using Items;
using UnityEngine;

namespace Inventory
{
    public interface IInventoryManager
    {
        public void AddItem(ItemSO item);
        public void UseItem(InventoryManager.InventoryItem inventoryItem);
    }
}
