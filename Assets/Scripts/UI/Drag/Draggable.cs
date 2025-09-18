using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DI;

namespace UI.Drag
{
    public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Inject] private IDragController _dragController;
        
        private RectTransform _rectTransform;
        private GraphicRaycaster _raycaster;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _raycaster = GetCanvas().GetComponent<GraphicRaycaster>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / GetCanvas().scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _rectTransform.localPosition = Vector3.zero;
            
            List<RaycastResult> results = new List<RaycastResult>();

            _raycaster.Raycast(eventData, results);

            results.ForEach(r =>
            {
                RectTransform rectTransform = r.gameObject.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    _dragController.OnDrop?.Invoke(this, r.gameObject);
                }
            });
        }

        private Canvas GetCanvas()
        {
            return GetComponentInParent<Canvas>();
        }
    }
}