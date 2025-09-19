using System;
using Items;
using UnityEngine;

namespace Inventory
{
    [Serializable]
    public class InventoryItem
    {
        public ItemSO ItemSO { get => _itemSO; set => _itemSO = value; }
        public int Count { get => _count; set => _count = value; }
        public float LastUseTime { get => _lastUseTime; set => _lastUseTime = value; }
            
        [SerializeField] private ItemSO _itemSO;
        [SerializeField] private int _count;
        [SerializeField] private float _lastUseTime;
            
        public bool OnCooldown(float currentTime) =>
            _itemSO != null && (currentTime - _lastUseTime) < _itemSO.Cooldown;
    }
}
