using Context.ContextProvider.Abstraction;

namespace Context.ConsoleAppWithContext.Context
{
    /// <summary>
    /// HTTP request context implemenation.
    /// </summary>
    public class HttpRequestContext : IContext
    {
        public Guid Id { get; set; }
        public HttpMethod Verbose { get; set; } = HttpMethod.Get;
        public Uri Url { get; set; }
    }
}
