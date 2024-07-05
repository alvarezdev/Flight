using MediatR;

namespace Flight.Application.Invoice.Command
{
    public record CancelInvoiceCommand(Guid id) : IRequest;

}
