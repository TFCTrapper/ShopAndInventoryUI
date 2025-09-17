using UnityEngine;

namespace DI
{
    public abstract class Installer : MonoBehaviour
    {
        public abstract void Install(Container c);
    }
}
