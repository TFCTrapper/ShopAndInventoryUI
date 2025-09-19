using System;
using UI.Layers;
using UnityEngine;

namespace UI
{
    public class UIElement : MonoBehaviour, IUIElement
    {
        public Action OnHideComplete { get; set; }

        public GameObject GameObject => gameObject;
        public UILayer Layer => _layer;
        public UIElementData Data { get; private set; }

        public bool IsOpened => gameObject.activeInHierarchy;

        [SerializeField] private UILayer _layer;

        public virtual void Initialize(UIElementData data)
        {
            Data = data;
        }

        public virtual void Show(UIElementData data = null, bool immediately = false)
        {
            if (data != null)
            {
                Data = data;
            }

            gameObject.SetActive(true);
        }

        public virtual void Hide(bool immediately = false)
        {
            OnHideComplete?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
