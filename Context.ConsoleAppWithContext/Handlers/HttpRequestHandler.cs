using Context.ConsoleAppWithContext.Context;
using Context.ConsoleAppWithContext.Handlers.Base;
using Context.ContextProvider.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Context.ConsoleAppWithContext.Handlers
{
    /// <summary>
    /// HTTP request handler implementation.
    /// </summary>
    public class HttpRequestHandler : IRequestHandler<HttpRequestContext>
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public IContextProvider<HttpRequestContext> ContextProvider { get; private set; }

        private readonly Random random = new();

        public HttpRequestHandler(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public async Task Handle()
        {
            // Getting a new instance to prove transient behavior of ContextProvider.
            ContextProvider = ServiceProvider.GetRequiredService<IContextProvider<HttpRequestContext>>();

            if (ContextProvider.GetContext() == null)
            {
                Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId:D2}: Invalid HTTP Request Context Is Empty.");
                return;
            }

            Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId:D2} - Context Provider Identifier {ContextProvider?.GetHashCode():D8}: Handling HTTP '{ContextProvider.GetContext()?.Verbose}' Request with ID '{ContextProvider.GetContext()?.Id}' on '{ContextProvider.GetContext()?.Url}' URL.");

            // Generating a new thread.
            await Task.Delay(100);

            if (ContextProvider.GetContext() == null)
            {
                Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId:D2}: Invalid HTTP Request Context Is Empty.");
                return;
            }

            await (Task)GetType()
                    .GetMethod($"{ContextProvider.GetContext()?.Verbose.Method[0].ToString()?.ToUpper()}{ContextProvider.GetContext()?.Verbose.Method[1..].ToLower()}"
                    , BindingFlags.Instance | BindingFlags.NonPublic)?
                    .Invoke(this, null);

            // Getting a new instance to prove transient behavior of ContextProvider.
            ContextProvider = ServiceProvider.GetRequiredService<IContextProvider<HttpRequestContext>>();

            // Generating a new thread.
            await Task.Delay(100);

            if (ContextProvider.GetContext() == null)
            {
                Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId:D2}: Exiting Invalid HTTP Request Because Context Is Empty.");
                return;
            }

            Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId:D2} - Context Provider Identifier {ContextProvider?.GetHashCode():D8}: Exiting HTTP Request '{ContextProvider.GetContext()?.Verbose}' Handler Handle.");
        }

        private async Task Get()
        {
            // Getting a new instance to prove transient behavior of ContextProvider.
            ContextProvider = ServiceProvider.GetRequiredService<IContextProvider<HttpRequestContext>>();

            if (ContextProvider.GetContext() == null)
            {
                Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId:D2}: Invalid HTTP Request Because Context Is Empty.");
                return;
            }

            Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId:D2} - Context Provider Identifier {ContextProvider?.GetHashCode():D8}: HTTP Request {ContextProvider.GetContext()?.Id}: Getting All Examples Through '{ContextProvider.GetContext()?.Url}.");
            await Task.Delay(random.Next(2000, 5000));
            Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId:D2} - Context Provider Identifier {ContextProvider?.GetHashCode():D8}: Handling '{ContextProvider.GetContext()?.Id}' HTTP Request Succeeded.");

        }
        private async Task Post()
        {
            // Getting a new instance to prove transient behavior of ContextProvider.
            ContextProvider = ServiceProvider.GetRequiredService<IContextProvider<HttpRequestContext>>();

            if (ContextProvider.GetContext() == null)
            {
                Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId:D2}: Invalid HTTP Request Because Context Is Empty.");
                return;
            }

            Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId:D2} - Context Provider Identifier {ContextProvider?.GetHashCode():D8}: HTTP Request {ContextProvider.GetContext()?.Id}: Creating a New Example Through '{ContextProvider.GetContext()?.Url}.");
            await Task.Delay(random.Next(2000, 5000));
            Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId:D2} - Context Provider Identifier {ContextProvider?.GetHashCode():D8}: Handling '{ContextProvider.GetContext()?.Id}' HTTP Request Succeeded.");
        }
        private async Task Put()
        {
            // Getting a new instance to prove transient behavior of ContextProvider.
            ContextProvider = ServiceProvider.GetRequiredService<IContextProvider<HttpRequestContext>>();

            if (ContextProvider.GetContext() == null)
            {
                Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId:D2}: Invalid HTTP Request Because Context Is Empty.");
                return;
            }

            Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId:D2} - Context Provider Identifier {ContextProvider?.GetHashCode():D8}: HTTP Request {ContextProvider.GetContext()?.Id}: Updating an Existing Example Through '{ContextProvider.GetContext()?.Url}.");
            await Task.Delay(random.Next(2000, 5000));
            Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId:D2} - Context Provider Identifier {ContextProvider?.GetHashCode():D8}: Handling '{ContextProvider.GetContext()?.Id}' HTTP Request Succeeded.");
        }
        private async Task Delete()
        {
            // Getting a new instance to prove transient behavior of ContextProvider.
            ContextProvider = ServiceProvider.GetRequiredService<IContextProvider<HttpRequestContext>>();

            if (ContextProvider.GetContext() == null)
            {
                Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId:D2}: Invalid HTTP Request Because Context Is Empty.");
                return;
            }

            Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId:D2} - Context Provider Identifier {ContextProvider?.GetHashCode():D8}: HTTP Request {ContextProvider.GetContext()?.Id}: Deleting an Existing Example Through '{ContextProvider.GetContext()?.Url}.");
            await Task.Delay(random.Next(2000, 5000));
            Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId:D2} - Context Provider Identifier {ContextProvider?.GetHashCode():D8}: Handling '{ContextProvider.GetContext()?.Id}' HTTP Request Succeeded.");
        }
    }
}
