using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class Window : UIElement
    {
        [SerializeField] protected CanvasGroup _canvasGroup;
        [SerializeField] protected float _fadeDuration;
        
        private Dictionary<Button, bool> _buttonsInteractable = new Dictionary<Button, bool>();

        public override void Show(UIElementData data = null, bool immediately = false)
        {
            if (immediately)
            {
                base.Show(data, true);
                
                foreach (var button in _buttonsInteractable.Keys)
                {
                    button.interactable = _buttonsInteractable[button];
                }
                
                return;
            }

            gameObject.SetActive(true);
            
            foreach (var button in _buttonsInteractable.Keys)
            {
                button.interactable = false;
            }

            OnShowComplete();
        }

        public override void Hide(bool immediately = false)
        {
            foreach (var button in _buttonsInteractable.Keys)
            {
                button.interactable = false;
            }
            
            if (immediately)
            {
                base.Hide(true);
                return;
            }

            OnHideComplete?.Invoke();
            gameObject.SetActive(false);
        }

        protected virtual void OnShowComplete()
        {
            foreach (var button in _buttonsInteractable.Keys)
            {
                button.interactable = _buttonsInteractable[button];
            }
        }

        private void Awake()
        {
            _buttonsInteractable.Clear();
            
            var buttons = GetComponentsInChildren<Button>().ToList();
            foreach (var button in buttons)
            {
                _buttonsInteractable.Add(button, button.interactable);
            }
        }
    }
}
