using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName = "UIElements", menuName = "Scriptable Objects/UIElements")]
    public class UIElementsSO : ScriptableObject
    {
        public List<UIElement> Elements => _elements;

        [SerializeField] private List<UIElement> _elements;
    }
}
