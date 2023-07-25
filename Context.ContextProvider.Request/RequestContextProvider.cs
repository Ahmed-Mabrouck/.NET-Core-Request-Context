using Context.ContextProvider.Abstraction;

namespace Context.ContextProvider.Request
{
    /// <summary>
    /// Context provider service implementation using AsyncLocal.
    /// </summary>
    /// <typeparam name="T">Context type.</typeparam>
    public class RequestContextProvider<T> : IContextProvider<T> where T : IContext, new()
    {
        private static readonly AsyncLocal<T> context = new();

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
