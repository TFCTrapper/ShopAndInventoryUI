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
    
    private void Awake()
    {
        _uiLayersManager.Initialize();
        _uiManager.Initialize();
        
        var mainWindowData = new MainWindowData("", _uiManager);
        _uiManager.ShowElement<MainWindow>(mainWindowData);
    }
}
}
