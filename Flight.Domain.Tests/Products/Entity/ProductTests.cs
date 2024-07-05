using Flight.Domain.Exceptions;
using FluentAssertions;

namespace Flight.Domain.Tests.Products.Entity
{
    public class ProductTests
    {
        const int MinimunLengthName = 3;
        const int MaximunLengthName = 100;

        [Fact]
        public void Product_WithNameNull_RequiredException()
        {
            FluentActions.Invoking(() => new ProductDataBuilder().WithName(default!).Build())
                .Should().Throw<RequiredException>()
                .WithMessage("the name should not be null or empty.");
        }

        [Fact]
        public void Product_WithNameMinimunLength_RequiredException()
        {
            FluentActions.Invoking(() => new ProductDataBuilder().WithName("Ad").Build())
                .Should().Throw<RequiredException>()
                .WithMessage($"the name should be between {MinimunLengthName} and {MaximunLengthName} characters.");
        }

        [Fact]
        public void Product_WithNameMaximunLength_RequiredException()
        {
            FluentActions.Invoking(() => new ProductDataBuilder().WithName("The output includes credentials that you must protect. Be sure that you do not include these credentials").Build())
                .Should().Throw<RequiredException>()
                .WithMessage($"the name should be between {MinimunLengthName} and {MaximunLengthName} characters.");
        }

        [Fact]
        public void Product_WithValueZero_RequiredException()
        {
            FluentActions.Invoking(() => new ProductDataBuilder().WithValue(0).Build())
                .Should().Throw<RequiredException>()
                .WithMessage("the value should be greater than zero.");
        }
    }
}
