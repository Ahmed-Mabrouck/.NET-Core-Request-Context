using Context.ContextProvider.Abstraction;

namespace Context.ContextProvider.Thread
{
    /// <summary>
    /// Context provider service implementation using ThreadLocal.
    /// </summary>
    /// <typeparam name="T">Context type.</typeparam>
    public class ThreadContextProvider<T> : IContextProvider<T> where T : IContext, new()
    {
        private static readonly ThreadLocal<T> context = new();

        public T InitializeContext()
        {
            context.Value = new T();
            return context.Value;
        }

        public T GetContext()
        {
            return context.Value;
        }
    }
}
