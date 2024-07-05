using Flight.Application.Ports;
using Flight.Domain.Invoices.Service;
using MediatR;

namespace Flight.Application.Invoice.Command
{
    internal class CancelInvoiceHandler(CancelInvoiceService cancelInvoiceService, IUnitOfWork unitOfWork) : IRequestHandler<CancelInvoiceCommand>
    {
        public async Task<Unit> Handle(CancelInvoiceCommand request, CancellationToken cancellationToken)
        {
            await cancelInvoiceService.ExecuteAsync(request.id);
            await unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
