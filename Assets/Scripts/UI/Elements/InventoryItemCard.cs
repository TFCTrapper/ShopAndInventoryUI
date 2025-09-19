using Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemCard : ItemCard
{
    [SerializeField] private Button _useButton;
    [SerializeField] private TextMeshProUGUI _countText;
    
    private InventoryManager _inventoryManager;
    private InventoryManager.InventoryItem _inventoryItem;
    
    public void Initialize(InventoryManager.InventoryItem inventoryItem, InventoryManager inventoryManager)
    {
        base.Initialize(inventoryItem.ItemSO);
        _inventoryItem = inventoryItem;
        
        _inventoryManager = inventoryManager;
        
        _countText.text = inventoryItem.Count > 0 ? inventoryItem.Count.ToString() : "\u221e";
    }

    public void UpdateCount(InventoryManager.InventoryItem inventoryItem)
    {
        _countText.text = inventoryItem.Count.ToString();
    }
    
    private void OnEnable()
    {
        _useButton.onClick.AddListener(OnUseClick);
    }
    
    private void OnDisable()
    {
        _useButton.onClick.RemoveListener(OnUseClick);
    }

    private void OnUseClick()
    {
        _inventoryManager.UseItem(_inventoryItem);
    }
}
