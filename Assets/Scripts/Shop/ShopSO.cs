using System.Collections.Generic;
using Items;
using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(fileName = "ShopSO", menuName = "Scriptable Objects/ShopSO")]
    public class ShopSO : ScriptableObject
    {
        public List<ItemSO> Items => _items;
        
        [SerializeField] private List<ItemSO> _items;
    }
}
