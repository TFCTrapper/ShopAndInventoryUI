using System.Collections.Generic;
using Inventory;
using Items.Actions;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Objects/ItemSO")]
    public class ItemSO : ScriptableObject
    {
        public ItemType ItemType => _itemType;
        public string Name => _name;
        public string Description => _description;
        public Sprite Icon => _icon;
        public float Price => _price;
        public bool IsInfinite => _isInfinite;
        public float Cooldown => _cooldown;
    
        [SerializeField] private ItemType _itemType;
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _price;
        [SerializeField] private bool _isInfinite;
        [SerializeField] private float _cooldown;

        [Header("Behaviour")]
        [SerializeField] private List<ItemActionSO> _actions;

        public bool CanUse(UseContext useContext, InventoryItem inventoryItem, out string reason)
        {
            foreach (var action in _actions)
            {
                if (!action.CanExecute(useContext, inventoryItem, out reason))
                {
                    return false;
                }
            }

            reason = null;
            return true;
        }

        public void Use(UseContext useContext, InventoryItem inventoryItem)
        {
            foreach (var action in _actions)
            {
                action.Execute(useContext, inventoryItem);
            }

            inventoryItem.LastUseTime = Time.time;
        }
    }
}
