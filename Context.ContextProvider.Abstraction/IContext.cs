namespace Context.ContextProvider.Abstraction
{
    /// <summary>
    /// Request context base interface.
    /// </summary>
    public interface IContext
    {
        Guid Id { get; set; }
    }
}
