using Flight.Domain.Invoices.Model.Dto;

namespace Flight.Domain.Invoices.Port
{
    public interface IInvoiceSimpleQueryRepository
    {
        Task<IEnumerable<SummaryInvoiceDto>> GetAllCancelAsync();
    }
}
