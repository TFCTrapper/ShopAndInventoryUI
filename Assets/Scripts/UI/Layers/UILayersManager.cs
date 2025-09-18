using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DI;

namespace UI.Layers
{
    public class UILayersManager : MonoBehaviour, IUILayersManager
    {
        public IReadOnlyDictionary<UILayer, Canvas> Layers => _layers;

        private const string LAYER_CANVAS_POSTFIX = "LayerCanvas";

        [SerializeField] private Canvas _emptyLayer = default;
        [SerializeField] private float _uiCameraPlaneDistanceOffset;

        private Dictionary<UILayer, Canvas> _layers = new Dictionary<UILayer, Canvas>();

        public void Initialize()
        {
            foreach (UILayer layer in Enum.GetValues(typeof(UILayer)))
            {
                if (!_layers.ContainsKey(layer))
                {
                    var layerInstance = Instantiate(_emptyLayer, transform);
                    layerInstance.name = layer + LAYER_CANVAS_POSTFIX;
                    layerInstance.sortingOrder = (int) layer;
                    var canvas = layerInstance.GetComponent<Canvas>();

                    if (layer == UILayer.Windows)
                    {
                        UpdateCanvasCamera(canvas);
                    }

                    _layers.Add(layer, layerInstance);
                }
            }
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            UpdateCanvasCamera(_layers[UILayer.Windows]);
        }

        private void UpdateCanvasCamera(Canvas canvas)
        {
            canvas.worldCamera = Camera.main;
            if (Camera.main.transform.eulerAngles.x == 0f)
            {
                canvas.planeDistance = 1f;
            }
            else
            {
                canvas.planeDistance = Camera.main.transform.position.y - _uiCameraPlaneDistanceOffset;
            }
        }
    }
}
