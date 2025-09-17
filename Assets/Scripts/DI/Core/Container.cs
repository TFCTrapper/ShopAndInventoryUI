using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace DI
{
    public sealed class Container
    {
        private readonly Dictionary<Type, Binding> _bindings = new();
        private readonly Dictionary<Type, object> _singletons = new();
        private readonly Container _parent;
        private readonly Stack<Type> _resolutionStack = new();

        public Container(Container parent = null) { _parent = parent; }

        // ---------- Binding ----------
        public Binder Bind<TAbstraction>() => new Binder(this, typeof(TAbstraction));
        public Binder Bind(Type abstraction) => new Binder(this, abstraction);

        public sealed class Binder
        {
            private readonly Container _c;
            private readonly Type _abstract;
            private Type _impl;
            private object _instance;

            internal Binder(Container c, Type abs) { _c = c; _abstract = abs; }

            public Binder To<TImpl>() { _impl = typeof(TImpl); return this; }
            public Binder To(Type impl) { _impl = impl; return this; }

            public void AsSingle()
            {
                _c.Register(_abstract, new Binding(_abstract, _impl, _instance, null));
            }

            public void FromInstance(object instance)
            {
                _instance = instance;
                _impl = instance?.GetType();
                _c.Register(_abstract, new Binding(_abstract, _impl, _instance, null));
            }
            
            public void FromComponentInNewPrefab(
                GameObject prefab,
                Transform parent = null,
                bool searchChildren = false,
                bool includeInactive = true,
                string nameOverride = null
            )
            {
                if (prefab == null) throw new ArgumentNullException(nameof(prefab));
                if (_impl == null) throw new InvalidOperationException("Call To<T>() before FromComponentInNewPrefab.");
                if (!typeof(Component).IsAssignableFrom(_impl))
                    throw new InvalidOperationException($"Implementation type '{_impl.Name}' must be a UnityEngine.Component to use FromComponentInNewPrefab.");

                Func<Container, object> creator = (cont) =>
                {
                    var go = UnityEngine.Object.Instantiate(prefab, parent);
                    if (!string.IsNullOrEmpty(nameOverride))
                        go.name = nameOverride;

                    // Find the component
                    Component comp = searchChildren
                        ? go.GetComponentInChildren(_impl, includeInactive)
                        : go.GetComponent(_impl);

                    if (comp == null)
                        throw new InvalidOperationException(
                            $"Prefab '{prefab.name}' does not contain component '{_impl.Name}' " +
                            (searchChildren ? "(searched children)" : "(root only)") + ".");

                    // Inject all components on the new instance (root + children)
                    cont.InjectGameObject(go);

                    return comp;
                };

                _c.Register(_abstract, new Binding(_abstract, _impl, null, creator));
            }
            
            public void FromComponentInHierarchy(
                bool includeInactive = true,
                string name = null,
                string tag = null,
                Func<Component, bool> predicate = null,
                bool requireUnique = true
            )
            {
                if (_impl == null) throw new InvalidOperationException("Call To<T>() before FromComponentInHierarchy.");
                if (!typeof(Component).IsAssignableFrom(_impl))
                    throw new InvalidOperationException($"Implementation type '{_impl.Name}' must be a UnityEngine.Component.");

                Func<Container, object> creator = (cont) =>
                {
                    // Get all candidates in hierarchy
                    var list = new List<Component>();

                    // Unity 2023+: Object.FindObjectsByType
        #if UNITY_2023_1_OR_NEWER
                    var found = UnityEngine.Object.FindObjectsByType(
                        _impl, 
                        includeInactive ? FindObjectsInactive.Include : FindObjectsInactive.Exclude,
                        FindObjectsSortMode.None);
                    list.AddRange(found.Cast<Component>());
        #else
                    // Legacy: Object.FindObjectsOfType(Type, bool)
                    foreach (var c in UnityEngine.Object.FindObjectsOfType(_impl, includeInactive))
                        list.Add((Component)c);
        #endif

                    // Filter by tag / name / predicate if provided
                    if (!string.IsNullOrEmpty(tag))
                        list = list.Where(c => c.CompareTag(tag)).ToList();

                    if (!string.IsNullOrEmpty(name))
                        list = list.Where(c => c.gameObject.name == name).ToList();

                    if (predicate != null)
                        list = list.Where(predicate).ToList();

                    if (list.Count == 0)
                        throw new InvalidOperationException(
                            $"FromComponentInHierarchy could not find '{_impl.Name}' (includeInactive={includeInactive}, name='{name}', tag='{tag}').");

                    if (requireUnique && list.Count > 1)
                        throw new InvalidOperationException(
                            $"FromComponentInHierarchy found multiple '{_impl.Name}' (matches={list.Count}). " +
                            "Narrow by name/tag/predicate or set requireUnique=false.");

                    var comp = list[0];
                    // Inject entire GO (root + children)
                    cont.InjectGameObject(comp.gameObject);
                    return comp;
                };

                _c.Register(_abstract, new Binding(_abstract, _impl, null, creator));
            }
        }

        private void Register(Type abs, Binding b)
        {
            if (abs == null) throw new ArgumentNullException(nameof(abs));
            if (b == null) throw new ArgumentNullException(nameof(b));
            _bindings[abs] = b;
            if (b.Instance != null) _singletons[abs] = b.Instance;
        }

        private record Binding(
            Type Abstraction,
            Type Implementation,
            object Instance,
            Func<Container, object> CustomCreator);

        // ---------- Resolve ----------
        public T Resolve<T>() => (T)Resolve(typeof(T));
        public object Resolve(Type t)
        {
            if (_singletons.TryGetValue(t, out var existing)) return existing;

            if (!_bindings.TryGetValue(t, out var binding))
            {
                if (_parent != null) return _parent.Resolve(t);
                if (!t.IsAbstract && !t.IsInterface)
                    binding = new Binding(t, t, null, null);
                else
                    throw new InvalidOperationException($"No binding for type {t}.");
            }

            if (binding.Instance != null) return binding.Instance;

            object created = binding.CustomCreator == null ? Create(binding.Implementation) : binding.CustomCreator(this);
            
            _singletons[t] = created;
            return created;
        }

        private object Create(Type impl)
        {
            if (impl == null) throw new InvalidOperationException("Binding has no implementation.");

            if (_resolutionStack.Contains(impl))
            {
                var path = string.Join(" → ", _resolutionStack.Reverse().Select(x => x.Name).Concat(new[] { impl.Name }));
                throw new InvalidOperationException($"Circular dependency: {path}");
            }

            _resolutionStack.Push(impl);
            try
            {
                var ctors = impl.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
                var injectCtor = ctors.FirstOrDefault(c => c.GetCustomAttribute<InjectAttribute>() != null);
                var ctor = injectCtor ?? ctors.OrderByDescending(c => c.GetParameters().Length).FirstOrDefault();
                if (ctor == null) throw new InvalidOperationException($"Type {impl} has no public constructor.");

                var args = ctor.GetParameters().Select(p => TryResolveParameter(p)).ToArray();
                if (typeof(UnityEngine.MonoBehaviour).IsAssignableFrom(impl))
                {
                    var go = new GameObject(impl.Name);
                    var component = go.AddComponent(impl);
                    Inject(component);
                    return component;
                }
                else
                {
                    var obj = Activator.CreateInstance(impl, args);
                    Inject(obj);
                    return obj;
                }
            }
            finally { _resolutionStack.Pop(); }
        }

        private object TryResolveParameter(ParameterInfo p)
        {
            try { return Resolve(p.ParameterType); }
            catch when (p.GetCustomAttribute<InjectAttribute>()?.Optional == true) { return null; }
        }

        // ---------- Inject into existing instances ----------
        public void Inject(object instance)
        {
            if (instance == null) return;
            var type = instance.GetType();

            foreach (var f in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var attr = f.GetCustomAttribute<InjectAttribute>();
                if (attr == null) continue;
                try { f.SetValue(instance, Resolve(f.FieldType)); }
                catch when (attr.Optional) { }
            }

            foreach (var p in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                if (!p.CanWrite) continue;
                var attr = p.GetCustomAttribute<InjectAttribute>();
                if (attr == null) continue;
                try { p.SetValue(instance, Resolve(p.PropertyType)); }
                catch when (attr.Optional) { }
            }
        }

        // ---------- Child containers ----------
        public Container CreateChild() => new Container(this);
    }
}
