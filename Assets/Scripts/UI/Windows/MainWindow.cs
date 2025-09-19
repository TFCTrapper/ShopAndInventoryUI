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
            _inventoryButton.onClick.AddListener(OnInventoryClick);
            _shopButton.onClick.AddListener(OnShopClick);
        }

        private void OnDisable()
        {
            _inventoryButton.onClick.RemoveListener(OnInventoryClick);
            _shopButton.onClick.RemoveListener(OnShopClick);
        }

        private void OnShopClick()
        {
            UIElementData shopWindowData = new ShopWindowData(
                _mainWindowData.UIManager,
                _mainWindowData.ShopManager,
                _mainWindowData.InventoryManager
            );
            _mainWindowData.UIManager.ShowElement<ShopWindow>(shopWindowData);
        }

        private void OnInventoryClick()
        {
            UIElementData inventoryWindowData = new InventoryWindowData(
                _mainWindowData.UIManager,
                _mainWindowData.InventoryManager
            );
            _mainWindowData.UIManager.ShowElement<InventoryWindow>(inventoryWindowData);
        }
    }
}
