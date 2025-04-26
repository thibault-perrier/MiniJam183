using UnityEngine;

namespace Extensions
{
    public static class ComponentExtensions
    {
        public static bool TryGetComponentInParent<T>(this Component _component, out T _result)
        {
            _result = _component.GetComponentInParent<T>();
            return _result != null;
        }
        
        public static bool TryGetComponentsInChildren<T>(this Component _component, out T[] _results)
        {
            _results = _component.GetComponentsInChildren<T>();
            return _results != null && _results.Length > 0;
        }
    }
}