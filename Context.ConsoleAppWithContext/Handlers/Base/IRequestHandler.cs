using Context.ContextProvider.Abstraction;

namespace Context.ConsoleAppWithContext.Handlers.Base
{
    /// <summary>
    /// Request handler service base interface.
    /// </summary>
    /// <typeparam name="T">Context type.</typeparam>
    public interface IRequestHandler<T> where T : IContext, new()
    {
        public IContextProvider<T> ContextProvider { get; }
        Task Handle();
    }
}
