using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
    public class ItemSO : ScriptableObject
    {
        public ItemType ItemType => _itemType;
        public string Name => _name;
        public string Description => _description;
        public Sprite Icon => _icon;
        public float Price => _price;
        public bool IsInfinite => _isInfinite;
    
        [SerializeField] private ItemType _itemType;
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _price;
        [SerializeField] private bool _isInfinite;
    }
}
