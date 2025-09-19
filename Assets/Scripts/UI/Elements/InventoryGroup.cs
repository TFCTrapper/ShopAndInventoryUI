using Inventory;
using Items;
using UnityEngine;

public class InventoryGroup : MonoBehaviour
{
    [SerializeField] private GameObject _itemCardPrefab;
    [SerializeField] private Transform _itemsCardsParent;
    
    private InventoryManager _inventoryManager;

    public void Initialize(ItemType itemType, InventoryManager inventoryManager)
    {
        _inventoryManager = inventoryManager;
        
        foreach (Transform child in _itemsCardsParent)
        {
            Destroy(child.gameObject);
        }
        
        foreach (var inventoryItem in _inventoryManager.InventoryItems)
        {
            var itemCard = Instantiate(_itemCardPrefab, _itemsCardsParent).GetComponent<ItemCard>();
            itemCard.Initialize(inventoryItem.Item);
        }
    }
}
