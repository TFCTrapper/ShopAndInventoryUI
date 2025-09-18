using UI;

namespace UI.Windows.Data
{
    public class SomeWindowData : UIElementData
    {
        public string SomeText { get; private set; }

        public SomeWindowData(string name, string someText) : base(name)
        {
            Name = name;
            SomeText = someText;
        }
    }
}
