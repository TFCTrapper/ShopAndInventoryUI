using Inventory;
using Shop;

namespace UI.Windows.Data
{
    public class MainWindowData : UIElementData
    {
        public UIManager UIManager { get; private set; }
        public ShopManager ShopManager { get; private set; }
        public InventoryManager InventoryManager { get; private set; }

        public MainWindowData(UIManager uiManager, ShopManager shopManager, InventoryManager inventoryManager)
        {
            UIManager = uiManager;
            ShopManager = shopManager;
            InventoryManager = inventoryManager;
        }
    }
}
