using System;
using UI.Layers;
using UnityEngine;

namespace UI
{
    public interface IUIElement
    {
        public Action OnHideComplete { get; set; }
        public GameObject GameObject { get; }
        public UILayer Layer { get; }
        public UIElementData Data { get; }
        public bool IsOpened { get; }
        public void Initialize(UIElementData data);
        public void Show(UIElementData data, bool immediately);
        public void Hide(bool immediately);
    }
}
