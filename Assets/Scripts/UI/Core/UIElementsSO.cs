using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName = "UIElementsSO", menuName = "Scriptable Objects/UIElementsSO")]
    public class UIElementsSO : ScriptableObject
    {
        public List<UIElement> Elements => _elements;

        [SerializeField] private List<UIElement> _elements;
    }
}
