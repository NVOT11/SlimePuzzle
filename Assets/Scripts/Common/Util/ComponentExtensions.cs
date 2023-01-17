using UnityEngine;

namespace Common
{
    public static class ComponentExtensions
    {
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        => gameObject.GetComponent<T>() ?? gameObject.AddComponent<T>();

        public static bool HasComponent<T>(this GameObject gameObject) where T : Component
         => gameObject.GetComponent<T>() != null;
    }
}