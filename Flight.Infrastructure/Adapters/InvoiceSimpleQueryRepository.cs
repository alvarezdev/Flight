using Flight.Domain.Invoices.Model.Dto;
using Flight.Domain.Invoices.Model.Entity;
using Flight.Domain.Invoices.Port;
using Flight.Infrastructure.DataSource;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Flight.Infrastructure.Adapters
{
    [Repository]
    public class InvoiceSimpleQueryRepository(DataContext dataContext) : IInvoiceSimpleQueryRepository
    {
        private IDbConnection DbConnection => dataContext.Database.GetDbConnection();

        public async Task<IEnumerable<SummaryInvoiceDto>> GetAllCancelAsync()
        {
            var invoices = await DbConnection
                .QueryAsync<SummaryInvoiceDto>(@"select id, ValueTotal, State from Invoice where State = @State",
                    new { State = InvoiceState.Canceled }
                    );
            return invoices;
        }
    }
}
