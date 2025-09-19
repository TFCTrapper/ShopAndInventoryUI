using Inventory;
using Shop;

namespace UI.Windows.Data
{
    public class ShopWindowData : UIElementData
    {
        public UIManager UIManager { get; set; }
        public ShopManager ShopManager { get; set; }
        public InventoryManager InventoryManager { get; set; }

        public ShopWindowData(UIManager uiManager, ShopManager shopManager, InventoryManager inventoryManager)
        {
            UIManager = uiManager;
            ShopManager = shopManager;
            InventoryManager = inventoryManager;
        }
    }
}
