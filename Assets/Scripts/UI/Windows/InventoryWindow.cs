using System;
using System.Collections.Generic;
using Inventory;
using Items;
using UI.Windows.Data;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class InventoryWindow : Window
    {
        [SerializeField] private Button _mainWindowButton;
        [SerializeField] private GameObject _inventoryGroupPrefab;
        [SerializeField] private Transform _inventoryGroupsParent;

        private InventoryWindowData _inventoryWindowData;
        private List<InventoryItemCard> _itemCards = new List<InventoryItemCard>();
        
        public override void Show(UIElementData data = null, bool immediately = false)
        {
            base.Show(data, immediately);

            if (data != null && _inventoryWindowData != data)
            {
                _inventoryWindowData = data as InventoryWindowData;
                _inventoryWindowData.InventoryManager.InventoryItemCountChangedAction += OnInventoryItemCountChanged;
            }
            
            foreach (Transform child in _inventoryGroupsParent)
            {
                Destroy(child.gameObject);
            }
            
            _itemCards.Clear();

            foreach (ItemType itemType in Enum.GetValues(typeof(ItemType)))
            {
                var inventoryGroup = Instantiate(_inventoryGroupPrefab, _inventoryGroupsParent).GetComponent<InventoryGroup>();
                inventoryGroup.Initialize(itemType, this, _inventoryWindowData.InventoryManager);
            }

            OnShowComplete();
        }

        public void AddItemCard(InventoryItemCard inventoryItemCard)
        {
            _itemCards.Add(inventoryItemCard);
        }
        
        private void OnEnable()
        {
            _mainWindowButton.onClick.AddListener(OnMainWindowButtonClick);
        }
        
        private void OnDisable()
        {
            _mainWindowButton.onClick.RemoveListener(OnMainWindowButtonClick);
        }

        private void OnMainWindowButtonClick()
        {
            _inventoryWindowData.UIManager.ShowElement<MainWindow>();
        }
        
        private void OnInventoryItemCountChanged(InventoryManager.InventoryItem inventoryItem)
        {
            var inventoryItemCard = _itemCards.Find(ic => ic.ItemSO == inventoryItem.ItemSO);
            if (inventoryItemCard != null)
            {
                inventoryItemCard.UpdateCount(inventoryItem);
                if (inventoryItem.Count <= 0)
                {
                    Destroy(inventoryItemCard.gameObject);
                }
            }

        }
    }
}
