using System.Collections.Generic;
using DI;
using Inventory;
using Items;
using UnityEngine;

namespace Shop
{
    public class ShopManager : MonoBehaviour, IShopManager
    {
        public List<ItemSO> Items => _items;

        [SerializeField] private StatsSO _stats;
        [SerializeField] private List<ItemSO> _items;
        
        [Inject] private IInventoryManager _inventoryManager;

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
