using Flight.Domain.Invoices.Model.Entity;
using Flight.Domain.Invoices.Port;
using Flight.Domain.Invoices.Service;
using Flight.Domain.Tests.Customers.Entity;
using Flight.Domain.Tests.Invoices.Model.Entity;
using FluentAssertions;
using NSubstitute;

namespace Flight.Domain.Tests.Invoices.Service
{
    public class InsertInvoiceServiceTests
    {
        readonly IInvoiceRepository _invoiceRepository;
        readonly InsertInvoiceService _insertInvoiceService;

        public InsertInvoiceServiceTests()
        {
            _invoiceRepository = Substitute.For<IInvoiceRepository>();
            _insertInvoiceService = new InsertInvoiceService(_invoiceRepository);
        }

        [Fact]
        public async Task ExecuteAsync_Success()
        {
            var customer = new CustomerDataBuilder().Build();
            ICollection<ProductInvoice> productsInvoice = [new ProductInvoiceDataBuilder().Build(), new ProductInvoiceDataBuilder().Build()];
            var invoice = new InvoiceDataBuilder()
                .WithCustomer(customer)
                .WithProductsInvoice(productsInvoice)
                .Build();
            var id = Guid.NewGuid();
            _invoiceRepository.AddAsync(Arg.Any<Invoice>()).Returns(id);

            var InvoiceId = await _insertInvoiceService.ExecuteAsync(invoice);

            await _invoiceRepository.Received(1).AddAsync(Arg.Any<Invoice>());
            InvoiceId.Should().Be(id);
        }
    }
}
