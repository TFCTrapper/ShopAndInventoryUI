using System;
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

            foreach (ItemType itemType in Enum.GetValues(typeof(ItemType)))
            {
                var inventoryGroup = Instantiate(_inventoryGroupPrefab, _inventoryGroupsParent).GetComponent<InventoryGroup>();
                inventoryGroup.Initialize(itemType, _inventoryWindowData.InventoryManager);
            }

            OnShowComplete();
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
            //TODO
        }
    }
}
