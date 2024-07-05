using Flight.Domain.Invoices.Model.Dto;
using MediatR;

namespace Flight.Application.Invoice.Query
{
    public record GetAllCancelQuery() : IRequest<IEnumerable<SummaryInvoiceDto>>;

}
