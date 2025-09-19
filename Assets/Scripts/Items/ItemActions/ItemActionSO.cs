using Inventory;
using UnityEngine;

namespace Items.Actions
{
    [CreateAssetMenu(fileName = "ItemActionSO", menuName = "Scriptable Objects/ItemActions/ItemActionSO")]
    public abstract class ItemActionSO : ScriptableObject
    {
        public abstract bool CanExecute(UseContext ctx, InventoryManager.InventoryItem inventoryItem, out string reason);
        public abstract void Execute(UseContext useContext, InventoryManager.InventoryItem inventoryItem);
    }
}
