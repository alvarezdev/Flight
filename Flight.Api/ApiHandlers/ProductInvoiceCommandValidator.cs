using Flight.Application.Invoice.Command;
using FluentValidation;

namespace Flight.Api.ApiHandlers
{
    public class ProductInvoiceCommandValidator : AbstractValidator<ProductInvoiceCommand>
    {
        public ProductInvoiceCommandValidator()
        {
            RuleFor(command => command.ProductId)
                .NotEmpty();

            RuleFor(command => command.Quantity)
                .GreaterThan(default(int));
        }
    }
}
