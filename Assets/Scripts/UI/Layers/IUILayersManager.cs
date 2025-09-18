using System.Collections.Generic;
using UnityEngine;

namespace UI.Layers
{
    public enum UILayer
    {
        Windows = 0,
        Popups
    }

    public interface IUILayersManager
    {
        public IReadOnlyDictionary<UILayer, Canvas> Layers { get; }
    }
}
