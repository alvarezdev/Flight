using Flight.Domain.Exceptions;
using FluentAssertions;

namespace Flight.Domain.Tests.Invoices.Model.Entity
{
    public class ProductInvoiceTests
    {
        [Fact]
        public void ProductInvoice_WithQuantityZero_RequiredException()
        {
            FluentActions.Invoking(() => new ProductInvoiceDataBuilder().WithQuantity(default).Build())
                .Should().Throw<RequiredException>()
                .WithMessage("the quantity should be greater than zero.");
        }

        [Fact]
        public void ProductInvoice_WithProductNull_RequiredException()
        {
            FluentActions.Invoking(() => new ProductInvoiceDataBuilder().WithProduct(default!).Build())
                .Should().Throw<RequiredException>()
                .WithMessage("the product should not be null.");
        }

        [Fact]
        public void ProductInvoice_CalculateTotalWithIva_Success()
        {
            var productInvoice = new ProductInvoiceDataBuilder().Build();

            var totalWithIva = productInvoice.Invoking(x => x.CalculateTotalWithIva())
                 .Should().NotThrow();
            totalWithIva.Subject.Should().Be(357);
        }
    }
}
