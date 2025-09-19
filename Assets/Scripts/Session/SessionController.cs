using Inventory;
using Shop;
using UnityEngine;
using UI;
using UI.Layers;
using UI.Windows;
using UI.Windows.Data;

namespace Session
{
public class SessionController : MonoBehaviour
{
    [SerializeField] private UILayersManager _uiLayersManager;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private ShopManager _shopManager;
    [SerializeField] private InventoryManager _inventoryManager;
    
    private void Awake()
    {
        _uiLayersManager.Initialize();
        _uiManager.Initialize();
        
        var mainWindowData = new MainWindowData(_uiManager, _shopManager, _inventoryManager);
        _uiManager.ShowElement<MainWindow>(mainWindowData);
    }
}
}
