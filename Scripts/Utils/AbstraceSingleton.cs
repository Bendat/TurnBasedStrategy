namespace Assets.TurnBasedStrategy.Scripts.Utils
{
    /// <summary>
    /// Abstract base for for Singleton type classes. Only allows one instance of each class to exist.
    /// </summary>
    /// <typeparam name="T">Generic Type which inherits AbstractSingleton and is of type object.</typeparam>
    public abstract class AbstraceSingleton<T>
        where T : AbstraceSingleton<T>, new()
    {
        /// <summary>
        /// The living instance object of the class T.
        /// </summary>
        public static T Inst { get; } = new T();
    }
}