using System;
using UnityEngine;

namespace UI.Drag
{
    public class DragController : MonoBehaviour, IDragController
    {
        public Action<Draggable, GameObject> OnDrop { get; set; }
    }
}
