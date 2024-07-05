using Flight.Domain.Invoices.Model.Entity;
using Flight.Domain.Invoices.Port;
using Flight.Infrastructure.Ports;

namespace Flight.Infrastructure.Adapters
{
    [Repository]
    public class InvoiceRepository(IRepository<Invoice> invoiceRepository) : IInvoiceRepository
    {
        public async Task<Invoice> GetByIdAsync(Guid id) => await invoiceRepository.GetOneAsync(id);

        public async Task<Guid> AddAsync(Invoice invoice)
        {
            var invoiceInsert = await invoiceRepository.AddAsync(invoice);
            return invoiceInsert.Id;
        }

        public void Update(Invoice invoice) => invoiceRepository.UpdateAsync(invoice);

        public Task<Invoice> GetByIdAsync(Guid id, string? include = default)
        {
            return invoiceRepository.GetOneAsync(id, include);
        }
    }
}
