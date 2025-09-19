using System;
using System.Collections.Generic;
using Inventory;
using Items;
using UnityEngine;

namespace Shop
{
    public class ShopManager : MonoBehaviour, IShopManager
    {
        public Action<float> CurrencyCountChangedAction { get; set; }

        public List<ItemSO> Items => _shopSO.Items;
        public float CurrencyCount => _stats.CurrencyCount;

        [SerializeField] private InventoryManager _inventoryManager;
        [SerializeField] private StatsSO _stats;
        [SerializeField] private ShopSO _shopSO;

        public bool TryPurchaseItem(ItemSO item)
        {
            if (_stats.CurrencyCount >= item.Price)
            {
                _stats.CurrencyCount -= item.Price;
                CurrencyCountChangedAction?.Invoke(_stats.CurrencyCount);
                
                _inventoryManager.AddItem(item);
                return true;
            }

            return false;
        }
    }
}
