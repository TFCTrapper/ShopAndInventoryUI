using System;
using Items;

namespace Shop
{
    public interface IShopManager
    {
        public Action<float> CurrencyCountChangedAction { get; set; }
        public bool TryPurchaseItem(ItemSO item);
    }
}
