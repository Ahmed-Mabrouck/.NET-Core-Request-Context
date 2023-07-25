# .NET Core Request Context
 .NET Core request context examples using AsyncLocal and ThreadLocal which can be used with ASP.NET Core DI IoC.

## How Does It Work ?
### Solution Structure
- [Context.ContextProvider.Abstraction](https://github.com/AhmedMabrouck/.NET-Core-Request-Context/tree/65aa2b970bcb3640f242d92adc99bab582e87ae6/Context.ContextProvider.Abstraction): contains abstraction interfaces for the Context and ContextProvider service.
- [Context.ContextProvider.Request](https://github.com/AhmedMabrouck/.NET-Core-Request-Context/tree/65aa2b970bcb3640f242d92adc99bab582e87ae6/Context.ContextProvider.Request): contains AsyncLocal implementation for ContextProvider service.
- [Context.ContextProvider.Thread](https://github.com/AhmedMabrouck/.NET-Core-Request-Context/tree/65aa2b970bcb3640f242d92adc99bab582e87ae6/Context.ContextProvider.Request): contains ThreadLocal implemenation for ContextProvider service.
- [Context.ConsoleAppWithContext](https://github.com/AhmedMabrouck/.NET-Core-Request-Context/tree/65aa2b970bcb3640f242d92adc99bab582e87ae6/Context.ConsoleAppWithContext):-
  - A simple console application with IoC support to simulate ASP.NET requests, it has to type of requests HTTP [GET, POST, PUT, DELET] and Message [Reading, Alert].
  - It generates n number of random requests using defined requests templates for both HTTP and Message requets.
  - It sets the context of each requets according to randomly selected template and Invokes the requets handler for that requests.
  - All dependencies are supplied through IoC withn HostApplicalBuilder to simulate ASP.NET Core requets.
  - You can choose your ContextProvider service implementation between AsyncLocal, and ThreadLocal.
  - It has an event loop with Garbage Collection trigger every 5 seconds which can be used for debugging the application and investigating memory allocation.
## How to Configure It ?
### Implemenation
You can switch between the AsyncLocal implementation and ThreadLocal implementation by selecting build configuration profile [Async Local, and Thread Local]

![Build Profile](https://github.com/Ahmed-Mabrouck/.NET-Core-Request-Context/assets/13695213/743089f7-f566-4676-9261-4ae3439da18a)

### Number of Requests
You can change number of simulated requests by changing n varibale value in Context.ConsoleAppWithContext Program.cs [[Line 45]](https://github.com/Ahmed-Mabrouck/.NET-Core-Request-Context/blob/db7a22b0068567854ff877eab4723963c1e8c3f3/Context.ConsoleAppWithContext/Program.cs#L45C21-L45C21)

## What Was Testing Results ?
- AsyncLocal can be considered as one of the best implementation for request contexts.
- AsyncLocal is perfect implementation for a concurrent request as it can keep the context within an async execution path with internally multi-threaded execution [Multiple Async Method].
- AsyncLocal does not suffer of any memory leakage issues as the cotext data get collected after the async execution context ends.
- ThreadLocal can be used with a single threaded requests and it can keep all your context data within a thread execution context.
- Using ThreadLocal with multiple-threaded requests will lead to data leaks between requests or invalid data.
- ThreadLocal can suffer from memory leakage issues due to cross-threaded async calls.
