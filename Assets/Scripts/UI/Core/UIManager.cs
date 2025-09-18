using System;
using System.Collections.Generic;
using System.Linq;
using UI.Layers;
using UnityEngine;
using DI;

namespace UI
{
    public class UIManager : MonoBehaviour, IUIManager
    {
        private const string DataPath = "ScriptableObjects/UIData";

        [Inject] private IUILayersManager _uiLayersManager;

        private List<IUIElement> _existingElements;
        private List<IUIElement> _spawnedElements = new List<IUIElement>();
        
        public void Initialize()
        {
            var uiData = Resources.Load<UIData>(DataPath);
            _existingElements = uiData.Elements.Cast<IUIElement>().ToList();
        }

        public IUIElement GetElement<T>() where T : IUIElement
        {
            var element = _spawnedElements.FirstOrDefault(element => element.GetType() == typeof(T));
            return element ?? SpawnElement<T>();
        }

        public T ShowElement<T>(UIElementData data = null, bool immediately = false) where T : IUIElement
        {
            var element = GetElement<T>();
            if (element != null)
            {
                foreach (var e in _spawnedElements.Where(e => e.Layer == element.Layer))
                {
                    if (e != element && e.IsOpened)
                    {
                        e.Hide(true);
                    }
                }

                element.Show(data, immediately);
            }

            return (T) element;
        }

        public IUIElement ShowElement(IUIElement element, UIElementData data = null, bool immediately = false)
        {
            element.Show(data, immediately);
            return element;
        }

        public void HideElement<T>(bool immediately) where T : IUIElement
        {
            var element = GetElement<T>();
            if (element != null)
            {
                element.Hide(immediately);
            }
        }

        public void HideAllElements(UILayer layer, bool immediately)
        {
            foreach (var e in _spawnedElements.Where(e => e.IsOpened && e.Layer == layer))
            {
                if (e.IsOpened)
                {
                    e.Hide(immediately);
                }
            }
        }

        private void Awake()
        {
            ProjectContext.Container.Inject(this);
            Initialize();
        }

        private IUIElement SpawnElement<T>() where T : IUIElement
        {
            var prefab = _existingElements.FirstOrDefault(element => element.GetType() == typeof(T));

            if (prefab == default)
            {
                throw new NullReferenceException(
                    $"{typeof(T)} is missing. Add {typeof(T)} prefab to Resources/ScriptableObjects/UIData");
            }

            var instance = Instantiate(prefab.GameObject, _uiLayersManager.Layers[prefab.Layer].transform)
                .GetComponent<IUIElement>();
            instance.Hide(true);

            _spawnedElements.Add(instance);

            return instance;
        }
    }
}
