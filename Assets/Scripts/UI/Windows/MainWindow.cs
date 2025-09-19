using UnityEngine;
using UnityEngine.UI;
using UI.Windows.Data;

namespace UI.Windows
{
    public class MainWindow : Window
    {
        [SerializeField] private Button _inventoryButton;
        [SerializeField] private Button _shopButton;
        
        private MainWindowData _mainWindowData;
        
        public override void Show(UIElementData data = null, bool immediately = false)
        {
            base.Show(data, immediately);

            if (data != null)
            {
                _mainWindowData = data as MainWindowData;
            }

            OnShowComplete();
        }

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
            UIElementData shopWindowData = new ShopWindowData(
                _mainWindowData.UIManager,
                _mainWindowData.ShopManager,
                _mainWindowData.InventoryManager
            );
            _mainWindowData.UIManager.ShowElement<ShopWindow>(shopWindowData);
        }

        private void OnInventoryButtonClick()
        {
            UIElementData inventoryWindowData = new InventoryWindowData(
                (Data as MainWindowData).UIManager,
                (Data as MainWindowData).InventoryManager
            );
            (Data as MainWindowData).UIManager.ShowElement<InventoryWindow>(inventoryWindowData);
        }
    }
}
