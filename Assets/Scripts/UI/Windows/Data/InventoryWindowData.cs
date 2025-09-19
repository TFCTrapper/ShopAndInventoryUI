using Inventory;

namespace UI.Windows.Data
{
    public class InventoryWindowData : UIElementData
    {
        public UIManager UIManager { get; set; }
        public InventoryManager InventoryManager { get; set; }

        public InventoryWindowData(UIManager uiManager, InventoryManager inventoryManager)
        {
            UIManager = uiManager;
            InventoryManager = inventoryManager;
        }
    }
}
