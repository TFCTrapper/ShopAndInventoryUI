using System;
using UnityEngine;

namespace UI.Drag
{
    public interface IDragController
    {
        public Action<Draggable, GameObject> OnDrop { get; set; }
    }
}
