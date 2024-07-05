using MediatR;

namespace Flight.Application.Invoice.Command
{
    public record InsertInvoiceCommand(
        Guid CustomerId,
        IEnumerable<ProductInvoiceCommand> ProductsInvoice
    ) : IRequest<Guid>;
}
