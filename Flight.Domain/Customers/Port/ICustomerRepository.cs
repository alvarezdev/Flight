using Flight.Domain.Customers.Entity;

namespace Flight.Domain.Customers.Port
{
    public interface ICustomerRepository
    {
        Task<Customer> AddAsync(Entity.Customer customer);
        Task<Customer> GetByIdAsync(Guid id);
        Task<int> GetCountAsync();
    }
}
