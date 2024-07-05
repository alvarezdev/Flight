using Flight.Domain.Exceptions;
using FluentAssertions;

namespace Flight.Domain.Tests.Customers.Entity
{
    public class CustomerTests
    {
        const int MinimunLengthName = 3;
        const int MaximunLengthName = 100;

        [Fact]
        public void Customer_WithNameNull_RequiredException()
        {
            FluentActions.Invoking(() => new CustomerDataBuilder().WithName(default!).Build())
                .Should().Throw<RequiredException>()
                .WithMessage("the name should not be null or empty.");
        }

        [Fact]
        public void Customer_WithNameLength_RequiredException()
        {
            FluentActions.Invoking(() => new CustomerDataBuilder().WithName("Ad").Build())
                .Should().Throw<RequiredException>()
                .WithMessage($"the name should be between {MinimunLengthName} and {MaximunLengthName} characters.");
        }

        [Fact]
        public void Customer_WithTypeInvalid_RequiredException()
        {
            FluentActions.Invoking(() => new CustomerDataBuilder().WithType((Domain.Customers.Entity.TypeCustomer)3).Build())
                .Should().Throw<RequiredException>()
                .WithMessage("the customer type is not valid.");
        }

        [Fact]
        public void Customer_IsPreferential_True()
        {
            var customer = new CustomerDataBuilder().WithType(Domain.Customers.Entity.TypeCustomer.Preferential).Build();

            customer.IsPreferential().Should().BeTrue();
        }

        [Fact]
        public void Customer_IsCommon_True()
        {
            var customer = new CustomerDataBuilder().WithType(Domain.Customers.Entity.TypeCustomer.Common).Build();

            customer.IsCommon().Should().BeTrue();
        }

        [Fact]
        public void Customer_IsSpecial_True()
        {
            var customer = new CustomerDataBuilder().WithType(Domain.Customers.Entity.TypeCustomer.Special).Build();

            customer.IsSpecial().Should().BeTrue();
        }
    }
}
