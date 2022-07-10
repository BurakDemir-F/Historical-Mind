using UnityEngine;

namespace Utilities
{
    public static class UnityEngineObjectExtensions
    {
        public static bool IsNotNull(this Object obj)
        {
            return obj != null;
        }

        public static T GetComponentFromAllTransform<T>(this GameObject obj, T defaultValue)
        {
            var component = obj.GetComponent<T>();
            if (component != null) return component;

            component = obj.GetComponentInParent<T>();
            if (component != null) return component;

            component = obj.GetComponentInChildren<T>();
            if (component != null) return component;

            return defaultValue;
        }
    }
}
