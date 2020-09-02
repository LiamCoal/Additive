using System.Collections.Generic;
using Additive.Exceptions;

namespace Additive
{
    public class ResourceManager
    {
        private struct Traceable<TOriginal, TTrace>
        {
            private TOriginal _value;
            private TTrace _trace;

            public Traceable(TOriginal value, TTrace trace)
            {
                _value = value;
                _trace = trace;
            }

            public TOriginal Value
            {
                get => _value;
                set => _value = value;
            }

            public TTrace Trace
            {
                get => _trace;
                set => _trace = value;
            }
        }
        
        private Dictionary<string, object?> _objects = new Dictionary<string, object?>();
        private static Dictionary<string, Traceable<object?, string>> _globalObjects = new Dictionary<string, Traceable<object?, string>>();
        private string _addonName;

        public delegate object? CompilableResource(string addonName, string name);

        internal ResourceManager(string addonName)
        {
            _addonName = addonName;
        }
        
        internal void Load()
        {
            Compile();
            Append();
        }

        public void Add(string name, object? resource)
        {
            _objects[name] = resource;
        }
        
        // Provided because.
        public void Add(string name, CompilableResource resource)
        {
            _objects[name] = resource;
        }

        private void Compile()
        {
            foreach (var (key, value) in new Dictionary<string, object?>(_objects))
            {
                if (value is CompilableResource compile)
                {
                    _objects[key] = compile(_addonName, key);
                }
            }
        }

        private void Append()
        {
            Additive.Logging.LineText = $"Resources: {_addonName}: Appending to global...";
            foreach (var (key, value) in _objects)
            {
                Additive.Logging.LineText = $"Resources: {_addonName}: Appending to global... {_objects.Count - new List<string?>(_objects.Keys).IndexOf(key)} object left";
                if(_globalObjects.ContainsKey(key))
                    throw new ResourceConflictException($"Conflicting resource: {key} [original provider: {_globalObjects[key].Trace}, duplicate provider: {_addonName}]");
                _globalObjects[key] = new Traceable<object?, string> {Value = value, Trace = _addonName};
            }
        }

        public static object? Get(string id)
        {
            return _globalObjects[id].Value;
        }
    }
}