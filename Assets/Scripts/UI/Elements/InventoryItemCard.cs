using Inventory;
using TMPro;
using UnityEngine;

public class InventoryItemCard : ItemCard
{
    [SerializeField] private TextMeshProUGUI _countText;
    
    private InventoryManager _inventoryManager;
    
    public void Initialize(InventoryManager.InventoryItem inventoryItem, InventoryManager inventoryManager)
    {
        base.Initialize(inventoryItem.Item);
        
        _inventoryManager = inventoryManager;
        
        _countText.text = inventoryItem.Count > 0 ? inventoryItem.Count.ToString() : "\u221e";
    }

    public void UpdateCount(InventoryManager.InventoryItem inventoryItem)
    {
        _countText.text = inventoryItem.Count.ToString();
    }
}
