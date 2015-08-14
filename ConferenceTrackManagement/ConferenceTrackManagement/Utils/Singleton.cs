using System.Reflection;

namespace Utils
{
    /// <summary>
    /// Generic singleton implementation.
    /// </summary>
    public static class Singleton<T> where T : class
    {
        /// <summary>
        /// Nested class to create a fully lazy instantiation.
        /// </summary>
        class Nested
        {
            //Use reflection to make this singleton class truly a singleton.
            internal static readonly T instance = typeof(T).InvokeMember(typeof(T).Name,
                           BindingFlags.CreateInstance |
                           BindingFlags.Instance |
                           BindingFlags.NonPublic,
                           null, null, null) as T;
        };

        /// <summary>
        /// Singleton Instance.
        /// </summary>
        /// <value>
        /// The singleton instance.
        /// </value>
        public static T Instance
        {
            get
            {
                return Nested.instance;
            }
        }
    }
}