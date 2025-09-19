using Inventory;
using Items;
using UI.Windows;
using UnityEngine;

public class InventoryGroup : MonoBehaviour
{
    [SerializeField] private GameObject _itemCardPrefab;
    [SerializeField] private Transform _itemsCardsParent;
    
    private InventoryManager _inventoryManager;

    public void Initialize(
        ItemType itemType,
        InventoryWindow inventoryWindow,
        InventoryManager inventoryManager)
    {
        _inventoryManager = inventoryManager;
        
        foreach (Transform child in _itemsCardsParent)
        {
            Destroy(child.gameObject);
        }
        
        foreach (var inventoryItem in _inventoryManager.InventoryItems)
        {
            if (inventoryItem.ItemSO.ItemType == itemType)
            {
                var inventoryItemCard =
                    Instantiate(_itemCardPrefab, _itemsCardsParent).GetComponent<InventoryItemCard>();
                inventoryItemCard.Initialize(inventoryItem, _inventoryManager);
                inventoryWindow.AddItemCard(inventoryItemCard);
            }
        }
    }
}
