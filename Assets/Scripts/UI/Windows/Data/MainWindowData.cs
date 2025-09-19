using UI;

namespace UI.Windows.Data
{
    public class MainWindowData : UIElementData
    {
        public UIManager UIManager { get; private set; }

        public MainWindowData(string name, UIManager uiManager) : base(name)
        {
            Name = name;
            UIManager = uiManager;
        }
    }
}
