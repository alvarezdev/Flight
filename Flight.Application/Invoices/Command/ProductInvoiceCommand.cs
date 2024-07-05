namespace Flight.Application.Invoice.Command
{
    public record ProductInvoiceCommand(Guid ProductId, int Quantity);

}
