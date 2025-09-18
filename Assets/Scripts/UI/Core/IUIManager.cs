using UI.Layers;

namespace UI
{
    public interface IUIManager
    {
        public IUIElement GetElement<T>() where T : IUIElement;
        public T ShowElement<T>(UIElementData data = null, bool immediately = false) where T : IUIElement;
        public IUIElement ShowElement(IUIElement element, UIElementData data = null, bool immediately = false);
        public void HideElement<T>(bool immediately = false) where T : IUIElement;
        public void HideAllElements(UILayer layer, bool immediately = false);
    }
}
