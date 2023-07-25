using Context.ConsoleAppWithContext.Context;
using Context.ConsoleAppWithContext.Handlers.Base;
using Context.ContextProvider.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace Context.ConsoleAppWithContext.Handlers
{
    /// <summary>
    /// Message request handler implementation.
    /// </summary>
    public class MessageRequestHandler : IRequestHandler<MessageRequestContext>
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public IContextProvider<MessageRequestContext> ContextProvider { get; private set; }

        private readonly Random random = new();

        public MessageRequestHandler(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public async Task Handle()
        {
            // Getting a new instance to prove transient behavior of ContextProvider.
            ContextProvider = ServiceProvider.GetRequiredService<IContextProvider<MessageRequestContext>>();

            if (ContextProvider.GetContext() == null)
            {
                Console.WriteLine($"Invalid Message Request: Context Is Empty.");
                return;
            }

            Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId:D2} - Context Identifier {ContextProvider.GetContext()?.GetHashCode():D8}: Handling Message Request with ID '{ContextProvider.GetContext()?.Id}' from '{ContextProvider.GetContext()?.MessageBroker}'.");

            // Generating a new thread.
            await Task.Delay(100);

            if (ContextProvider.GetContext()?.MessageBroker == "Com.Example.Readings")
            {
                await StoreReading();
            }
            else if (ContextProvider.GetContext()?.MessageBroker == "Com.Example.Alerts")
            {
                await GenerateAlert();
            }
            else
            {
                Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId:D2} - Context Identifier {ContextProvider.GetContext()?.GetHashCode():D8}: Invalid Message Request '{ContextProvider.GetContext()?.Id}' Unknown Message Type with No Registered Handler.");
            }

            // Getting a new instance to prove transient behavior of ContextProvider.
            ContextProvider = ServiceProvider.GetRequiredService<IContextProvider<MessageRequestContext>>();

            // Generating a new thread.
            await Task.Delay(100);

            if (ContextProvider.GetContext() == null)
            {
                Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId:D2}: Exiting an Invalid Message Request from Handler.");
                return;
            }

            Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId:D2} - Context Identifier {ContextProvider.GetContext()?.GetHashCode():D8}: Exiting Message Request from '{ContextProvider.GetContext()?.MessageBroker}' Handler.");
        }
        private async Task StoreReading()
        {
            // Getting a new instance to prove transient behavior of ContextProvider.
            ContextProvider = ServiceProvider.GetRequiredService<IContextProvider<MessageRequestContext>>();

            if (ContextProvider.GetContext() == null)
            {
                Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId:D2}: Exiting Invalid Message Request Handler Because Context Is Empty.");
                return;
            }

            Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId:D2} - Context Identifier {ContextProvider.GetContext()?.GetHashCode():D8}: Message Request '{ContextProvider.GetContext()?.Id}' Storing Reading from '{ContextProvider.GetContext()?.MessageBroker}.");
            await Task.Delay(random.Next(2000, 5000));
            Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId:D2} - Context Identifier {ContextProvider.GetContext()?.GetHashCode():D8}: Handling '{ContextProvider.GetContext()?.Id}' Message Request Succeeded.");

        }

        private async Task GenerateAlert()
        {
            // Getting a new instance to prove transient behavior of ContextProvider.
            ContextProvider = ServiceProvider.GetRequiredService<IContextProvider<MessageRequestContext>>();

            if (ContextProvider.GetContext() == null)
            {
                Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId:D2}: Invalid Message Request Because Context Is Empty.");
                return;
            }

            Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId:D2} - Context Identifier {ContextProvider.GetContext()?.GetHashCode():D8}: Message Request '{ContextProvider.GetContext()?.Id}' Generating Alert from '{ContextProvider.GetContext()?.MessageBroker}.");
            await Task.Delay(random.Next(2000, 5000));
            Console.WriteLine($"Thread: {Thread.CurrentThread.ManagedThreadId:D2} - Context Identifier {ContextProvider.GetContext()?.GetHashCode():D8}: Handling '{ContextProvider.GetContext()?.Id}' Message Request Succeeded.");
        }
    }
}
