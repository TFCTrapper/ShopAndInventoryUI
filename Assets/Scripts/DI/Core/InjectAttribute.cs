using System;

namespace DI
{
    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class InjectAttribute : Attribute
    {
        public bool Optional { get; }
        public InjectAttribute(bool optional = false) => Optional = optional;
    }
}



