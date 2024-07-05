using Flight.Domain.Exceptions;
using Flight.Domain.Invoices.Port;
using Flight.Domain.Invoices.Service;
using Flight.Domain.Tests.Customers.Entity;
using Flight.Domain.Tests.Invoices.Model.Entity;
using FluentAssertions;
using NSubstitute;

namespace Flight.Domain.Tests.Invoices.Service
{
    public class CancelInvoiceServiceTests
    {
        readonly IInvoiceRepository _invoiceRepository;
        readonly CancelInvoiceService _cancelInvoiceService;

        public CancelInvoiceServiceTests()
        {
            _invoiceRepository = Substitute.For<IInvoiceRepository>();
            _cancelInvoiceService = new CancelInvoiceService(_invoiceRepository);
        }

        [Fact]
        public async Task ExecuteAsync_Success()
        {
            var invoice = new InvoiceDataBuilder().Build();
            _invoiceRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<string>()).Returns(invoice);
            await _cancelInvoiceService.ExecuteAsync(invoice.Id);

            await _invoiceRepository.Received().GetByIdAsync(Arg.Is(invoice.Id), "Customer");
            _invoiceRepository.Received(1).Update(Arg.Is(invoice));
            invoice.State.Should().Be(Domain.Invoices.Model.Entity.InvoiceState.Canceled);
        }

        [Fact]
        public async Task ExecuteAsync_InvoiceNotExist_RequiredException()
        {
            var invoice = new InvoiceDataBuilder().Build();

            var act = () => _cancelInvoiceService.ExecuteAsync(invoice.Id);

            await act.Should()
                  .ThrowAsync<RequiredException>()
                  .WithMessage("the invoice not exist.");
        }

        [Fact]
        public async Task ExecuteAsync_InvoiceIsCacel_RequiredException()
        {
            var invoice = new InvoiceDataBuilder()
                .WithState(Domain.Invoices.Model.Entity.InvoiceState.Canceled)
                .Build();
            _invoiceRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<string>()).Returns(invoice);

            var act = () => _cancelInvoiceService.ExecuteAsync(invoice.Id);

            await act.Should()
                  .ThrowAsync<CoreBusinessException>()
                  .WithMessage("the invoice is already canceled.");
        }

        [Fact]
        public async Task ExecuteAsync_CustomerIsCommon_RequiredException()
        {
            var invoice = new InvoiceDataBuilder()
                .WithCustomer(new CustomerDataBuilder().WithType(Domain.Customers.Entity.TypeCustomer.Common).Build())
                .Build();
            _invoiceRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<string>()).Returns(invoice);

            var act = () => _cancelInvoiceService.ExecuteAsync(invoice.Id);

            await act.Should()
                  .ThrowAsync<CoreBusinessException>()
                  .WithMessage("you cannot cancel the invoice of a common customer.");
        }
    }
}
