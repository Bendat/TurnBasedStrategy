namespace Assets.TurnBasedStrategy.Scripts.Utils
{
    public class AbstraceSingleton<T>
        where T : AbstraceSingleton<T>, new()
    {
        private static T _instance = new T();
        public static T Inst => _instance;

    }
}