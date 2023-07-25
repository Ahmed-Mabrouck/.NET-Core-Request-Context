using Context.ConsoleAppWithContext.Context;
using Context.ConsoleAppWithContext.Handlers;
using Context.ConsoleAppWithContext.Handlers.Base;
using Context.ContextProvider.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Context.ConsoleAppWithContext
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Prepare IoC container and inject all services in a transient scope.
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

            // You can choose between AsyncLocal (RequestContext) and ThreadLocal (ThreadContext)
            // by commenting/uncommenting each couple of lines.
            //builder.Services.AddTransient<IContextProvider<HttpRequestContext>, ContextProvider.Request.RequestContextProvider<HttpRequestContext>>();
            //builder.Services.AddTransient<IContextProvider<MessageRequestContext>, ContextProvider.Request.RequestContextProvider<MessageRequestContext>>();
            builder.Services.AddTransient<IContextProvider<HttpRequestContext>, ContextProvider.Thread.ThreadContextProvider<HttpRequestContext>>();
            builder.Services.AddTransient<IContextProvider<MessageRequestContext>, ContextProvider.Thread.ThreadContextProvider<MessageRequestContext>>();

            builder.Services.AddTransient<IRequestHandler<HttpRequestContext>, HttpRequestHandler>();
            builder.Services.AddTransient<IRequestHandler<MessageRequestContext>, MessageRequestHandler>();

            using IHost host = builder.Build();

            // Preparing HTTP Requests and Message Requests Simulation Templates.
            var httpRequests = new List<HttpRequestContext>
            {
                new HttpRequestContext {Verbose = HttpMethod.Get, Url = new Uri("http://www.example.com/getAllExamples")},
                new HttpRequestContext {Verbose = HttpMethod.Post, Url = new Uri("http://www.example.com/createExample")},
                new HttpRequestContext {Verbose = HttpMethod.Put, Url = new Uri("http://www.example.com/updateExample")},
                new HttpRequestContext {Verbose = HttpMethod.Delete, Url = new Uri("http://www.example.com/deleteExample")},
            };
            var messageRequests = new List<MessageRequestContext>
            {
                new MessageRequestContext {MessageBroker = "Com.Example.Readings"},
                new MessageRequestContext {MessageBroker = "Com.Example.Alerts"},
            };

            // Generating n number of randomized requests.
            var n = 10;
            var requestTypeRandom = new Random();
            var requestContentRandom = new Random();
            Parallel.For(0, n, async (c) =>
            {
                if (requestTypeRandom.Next(0, 2) == 0)
                {
                    var context = httpRequests[requestContentRandom.Next(0, 4)];
                    await InvokeHttpRequest(host.Services, context.Verbose, context.Url);
                }
                else
                {
                    var context = messageRequests[requestContentRandom.Next(0, 2)];
                    await InvokeMessageRequest(host.Services, context.MessageBroker);
                }
            });

            // Event loop.
            while (true)
            {
                // You can set breakpoints here to check memory heap.
                Thread.Sleep(5000);
                GC.Collect();
            }
        }

        private static async Task InvokeHttpRequest(IServiceProvider host, HttpMethod verbose, Uri url)
        {
            // Creating a scope for the services.
            using IServiceScope serviceScope = host.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;

            // Initialize HTTP request context.
            var ctxProvider = provider.GetRequiredService<IContextProvider<HttpRequestContext>>();
            var ctx = ctxProvider.InitializeContext();
            ctx.Id = Guid.NewGuid();
            ctx.Verbose = verbose;
            ctx.Url = url;

            // Generate a new thread.
            await Task.Delay(100);

            // Invoke an HTTP request.
            await provider.GetRequiredService<IRequestHandler<HttpRequestContext>>().Handle();
        }
        private static async Task InvokeMessageRequest(IServiceProvider host, string broker)
        {
            // Creating a scope for the services.
            using IServiceScope serviceScope = host.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;

            // Initialize message request context.
            var ctx = provider.GetRequiredService<IContextProvider<MessageRequestContext>>().InitializeContext();
            ctx.Id = Guid.NewGuid();
            ctx.MessageBroker = broker;

            // Generate a new thread.
            await Task.Delay(100);

            // Invoke a message request.
            await provider.GetRequiredService<IRequestHandler<MessageRequestContext>>().Handle();
        }
    }
}