using UnityEngine;
using DI;

public class MainInstaller : Installer
{
    [SerializeField] private GameObject _uiManagerPrefab;
    public override void Install(Container container)
    {
        container.Bind<IUIManager>().To<UIManager>().FromComponentInHierarchy();
    }
}
