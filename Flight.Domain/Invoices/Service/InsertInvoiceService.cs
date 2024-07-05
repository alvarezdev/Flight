using Flight.Domain.Common;
using Flight.Domain.Invoices.Model.Entity;
using Flight.Domain.Invoices.Port;

namespace Flight.Domain.Invoices.Service
{
    [DomainService]
    public class InsertInvoiceService(IInvoiceRepository invoiceRepository)
    {
        public async Task<Guid> ExecuteAsync(Invoice invoice)
        {
            var invoiceId = await invoiceRepository.AddAsync(invoice);

            return invoiceId;
        }
    }
}
