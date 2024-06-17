namespace Carrefas.Core.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
