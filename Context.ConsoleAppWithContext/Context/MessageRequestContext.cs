using Context.ContextProvider.Abstraction;

namespace Context.ConsoleAppWithContext.Context
{
    /// <summary>
    /// Message request context implementation.
    /// </summary>
    public class MessageRequestContext : IContext
    {
        public Guid Id { get; set; }
        public String MessageBroker { get; set; } = String.Empty;
    }
}
