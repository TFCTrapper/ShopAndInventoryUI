using UnityEngine;

namespace DI
{
    public sealed class ProjectContext : MonoBehaviour
    {
        public Installer[] installers;
        public static Container Container { get; private set; }

        void Awake()
        {
            Container = new Container();
            foreach (var i in installers) i?.Install(Container);
        }
    }
}
