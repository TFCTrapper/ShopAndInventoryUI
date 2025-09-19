using System.Collections.Generic;
using Inventory;
using Items;
using UnityEngine;

namespace Shop
{
    public class ShopManager : MonoBehaviour, IShopManager
    {
        public List<ItemSO> Items => _items;

        [SerializeField] private InventoryManager _inventoryManager;
        [SerializeField] private StatsSO _stats;
        [SerializeField] private List<ItemSO> _items;

        public void TryPurchaseItem(ItemSO item)
        {
            if (_stats.CurrencyCount >= item.Price)
            {
                _stats.CurrencyCount -= item.Price;
                _inventoryManager.AddItem(item);
            }
            else
            {
                //TODO: Show fail animation
            }
        }
    }
}
