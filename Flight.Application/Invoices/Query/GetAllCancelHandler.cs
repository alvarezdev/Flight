using Flight.Domain.Invoices.Model.Dto;
using Flight.Domain.Invoices.Port;
using MediatR;

namespace Flight.Application.Invoice.Query
{
    internal class GetAllCancelHandler(IInvoiceSimpleQueryRepository invoiceSimpleQueryRepository) : IRequestHandler<GetAllCancelQuery, IEnumerable<SummaryInvoiceDto>>
    {
        public async Task<IEnumerable<SummaryInvoiceDto>> Handle(GetAllCancelQuery request, CancellationToken cancellationToken)
        {
            return await invoiceSimpleQueryRepository.GetAllCancelAsync();
        }
    }
}
