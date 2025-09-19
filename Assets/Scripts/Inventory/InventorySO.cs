using System.Collections.Generic;
using Inventory;
using UnityEngine;

[CreateAssetMenu(fileName = "InventorySO", menuName = "Scriptable Objects/InventorySO")]
public class InventorySO : ScriptableObject
{
    public List<InventoryItem> InventoryItems => _inventoryItems;
    
    [SerializeField] private List<InventoryItem> _inventoryItems;
}
