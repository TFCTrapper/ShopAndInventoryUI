using Items;
using UnityEngine;

namespace Shop
{
    public interface IShopManager
    {
        public void TryPurchaseItem(ItemSO item);
    }
}
