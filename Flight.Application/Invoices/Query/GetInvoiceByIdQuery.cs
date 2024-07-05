using Flight.Application.Invoice.Query.Dto;
using MediatR;

namespace Flight.Application.Invoice.Query
{
    public record GetInvoiceByIdQuery(Guid id) : IRequest<InvoiceDto>;
}
