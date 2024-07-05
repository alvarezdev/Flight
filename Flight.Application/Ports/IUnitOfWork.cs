namespace Flight.Application.Ports
{
    public interface IUnitOfWork
    {
        Task SaveAsync(CancellationToken? cancellationToken = null);
    }
}
