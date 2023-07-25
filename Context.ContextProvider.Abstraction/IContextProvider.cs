namespace Context.ContextProvider.Abstraction
{
    /// <summary>
    /// Context provider service base interface.
    /// </summary>
    /// <typeparam name="T">Context type.</typeparam>
    public interface IContextProvider<T> where T : IContext, new()
    {
        public T InitializeContext();

        public T GetContext();
    }
}
