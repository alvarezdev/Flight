using Flight.Domain.Common;
using Flight.Domain.Invoices.Port;

namespace Flight.Domain.Invoices.Service
{
    [DomainService]
    public class CancelInvoiceService(IInvoiceRepository invoiceRepository)
    {
        public async Task ExecuteAsync(Guid id)
        {
            var invoice = await invoiceRepository.GetByIdAsync(id, nameof(Customers.Entity.Customer));
            invoice.ValidateNull("the invoice not exist.");
            invoice.IsCancel();
            invoice.Cancel();
            invoiceRepository.Update(invoice);
        }
    }
}
