using UnityEngine;

namespace DI
{
    public static class DiExtensions
    {
        public static T InstantiateWithDI<T>(this Container c, T prefab) where T : Object
        {
            var clone = Object.Instantiate(prefab);
            if (clone is GameObject go)
            {
                foreach (var mb in go.GetComponentsInChildren<MonoBehaviour>(true))
                    c.Inject(mb);
            }
            else
            {
                c.Inject(clone);
            }

            return clone;
        }

        public static void InjectGameObject(this Container c, GameObject go)
        {
            foreach (var mb in go.GetComponentsInChildren<MonoBehaviour>(true))
                c.Inject(mb);
        }
    }
}
