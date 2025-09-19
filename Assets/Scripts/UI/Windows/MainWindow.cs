using System;
using UnityEngine;
using UnityEngine.UI;
using UI;
using UI.Windows;
using UI.Windows.Data;

namespace UI.Windows
{
    public class MainWindow : Window
    {
        [SerializeField] private Button _inventoryButton;
        [SerializeField] private Button _shopButton;

        private void OnEnable()
        {
            _inventoryButton.onClick.AddListener(OnInventoryButtonClick);
            _shopButton.onClick.AddListener(OnShopButtonClick);
        }

        private void OnDisable()
        {
            _inventoryButton.onClick.RemoveListener(OnInventoryButtonClick);
            _shopButton.onClick.RemoveListener(OnShopButtonClick);
        }

        private void OnShopButtonClick()
        {
            (Data as MainWindowData).UIManager.ShowElement<ShopWindow>();
        }

        private void OnInventoryButtonClick()
        {
            (Data as MainWindowData).UIManager.ShowElement<InventoryWindow>();
        }
    }
}
