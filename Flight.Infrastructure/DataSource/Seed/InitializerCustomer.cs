using Flight.Application.Ports;
using Flight.Domain.Customers.Entity;
using Flight.Domain.Customers.Port;

namespace Flight.Infrastructure.DataSource.Seed
{
    public class InitializerCustomer(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        public async Task CreateAsync()
        {
            var count = await customerRepository.GetCountAsync();
            if (count > 0) return;

            Customer customer = new() { Id = Guid.NewGuid(), Name = "Ceiba Software", TypeCustomer = TypeCustomer.Common };
            await customerRepository.AddAsync(customer);
            await unitOfWork.SaveAsync();
        }

    }
}
