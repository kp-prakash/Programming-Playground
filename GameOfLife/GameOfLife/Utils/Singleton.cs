using System.Reflection;

namespace Utils
{
    /// <summary>
    /// Singleton Implementation.
    /// </summary>
    public static class Singleton<T> where T : class
    {
        class Nested
        {
            internal static readonly T Instance = typeof(T).InvokeMember(typeof(T).Name,
                           BindingFlags.CreateInstance |
                           BindingFlags.Instance |
                           BindingFlags.NonPublic,
                           null, null, null) as T;
        }

        public static T Instance
        {
            get
            {
                return Nested.Instance;
            }
        }
    }
}