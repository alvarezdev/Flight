using Flight.Domain.Exceptions;
using Flight.Domain.Tests.Customers.Entity;
using FluentAssertions;

namespace Flight.Domain.Tests.Invoices.Model.Entity
{
    public class InvoiceTests
    {
        [Fact]
        public void Invoice_WithCustomerNull_RequiredException()
        {
            FluentActions.Invoking(() => new InvoiceDataBuilder().WithCustomer(default!).Build())
                .Should().Throw<RequiredException>()
                .WithMessage("the customer should not be null.");
        }

        [Fact]
        public void Invoice_WithProductsInvoiceNull_RequiredException()
        {
            FluentActions.Invoking(() => new InvoiceDataBuilder().WithProductsInvoice(default!).Build())
                .Should().Throw<RequiredException>()
                .WithMessage("the products should not be null.");
        }

        [Fact]
        public void Invoice_WithProductsInvoiceEmpty_RequiredException()
        {
            FluentActions.Invoking(() => new InvoiceDataBuilder().WithProductsInvoice(new List<Domain.Invoices.Model.Entity.ProductInvoice>()).Build())
                .Should().Throw<RequiredException>()
                .WithMessage("the products should not be empty.");
        }

        [Fact]
        public void Invoice_WithStateInvalid_RequiredException()
        {
            FluentActions.Invoking(() => new InvoiceDataBuilder().WithState((Domain.Invoices.Model.Entity.InvoiceState)9).Build())
                .Should().Throw<RequiredException>()
                .WithMessage("the invoice state is not valid.");
        }

        [Fact]
        public void Invoice_CustomerIsSpecial_Success()
        {
            ICollection<Domain.Invoices.Model.Entity.ProductInvoice> productsInvoice = [new ProductInvoiceDataBuilder().Build(), new ProductInvoiceDataBuilder().Build()];

            var invoice = new InvoiceDataBuilder()
                .WithProductsInvoice(productsInvoice)
                .Build();

            invoice.ValueTotal.Should().Be(642.6M);
        }

        [Fact]
        public void Invoice_CustomerIsPreferential_Success()
        {
            var customer = new CustomerDataBuilder().WithType(Domain.Customers.Entity.TypeCustomer.Preferential).Build();
            ICollection<Domain.Invoices.Model.Entity.ProductInvoice> productsInvoice = [new ProductInvoiceDataBuilder().Build(), new ProductInvoiceDataBuilder().Build()];

            var invoice = new InvoiceDataBuilder()
                .WithCustomer(customer)
                .WithProductsInvoice(productsInvoice)
                .Build();

            invoice.ValueTotal.Should().Be(571.2M);
        }

        [Fact]
        public void Invoice_CustomerIsCommun_Success()
        {
            var customer = new CustomerDataBuilder().WithType(Domain.Customers.Entity.TypeCustomer.Common).Build();
            ICollection<Domain.Invoices.Model.Entity.ProductInvoice> productsInvoice = [new ProductInvoiceDataBuilder().Build(), new ProductInvoiceDataBuilder().Build()];

            var invoice = new InvoiceDataBuilder()
                .WithCustomer(customer)
                .WithProductsInvoice(productsInvoice)
                .Build();

            invoice.ValueTotal.Should().Be(714M);
        }
    }
}
