using Flight.Application.Invoice.Command.Factory;
using Flight.Application.Ports;
using Flight.Domain.Invoices.Service;
using MediatR;

namespace Flight.Application.Invoice.Command
{
    internal class InsertInvoiceHandler(InvoiceFactory insertInvoiceFactory, InsertInvoiceService insertInvoiceService, IUnitOfWork unitOfWork) : IRequestHandler<InsertInvoiceCommand, Guid>
    {
        public async Task<Guid> Handle(InsertInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = await insertInvoiceFactory.CreateAsync(request);
            var invoiceId = await insertInvoiceService.ExecuteAsync(invoice);
            await unitOfWork.SaveAsync();
            return invoiceId;
        }
    }
}
