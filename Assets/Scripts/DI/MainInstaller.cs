using UnityEngine;
using DI;
using UI;
using UI.Layers;

public class MainInstaller : Installer
{
    [SerializeField] private GameObject _uiManagerPrefab;
    public override void Install(Container container)
    {
        container.Bind<IUILayersManager>().To<UILayersManager>().FromComponentInNewPrefab(_uiManagerPrefab);
        container.Bind<IUIManager>().To<UIManager>().FromComponentInHierarchy();
    }
}
