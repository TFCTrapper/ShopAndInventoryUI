using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName = "UIData", menuName = "Data/UIData")]
    public class UIData : ScriptableObject
    {
        public List<UIElement> Elements => _elements;

        [SerializeField] private List<UIElement> _elements;
    }
}
