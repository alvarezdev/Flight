using Flight.Application.Invoice.Command;
using Flight.Application.Invoice.Query.Dto;
using Flight.Application.Ports;
using Flight.Domain.Customers.Entity;
using Flight.Domain.Invoices.Model.Entity;
using Flight.Domain.Products.Entity;
using Flight.Infrastructure.Ports;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace Flight.Api.Tests.InvoiceApi;

public class InvoiceApiTests
{
    [Fact]
    public async Task GetSingleClientsSuccess()
    {
        await using var webApp = new ApiApp();
        var customer = new CustomerDataBuilder().Build();
        var product = new ProductDataBuilder().Build();
        var invoiceCreated = await CreateInvoice(webApp, customer, product);
        var client = webApp.CreateClient();

        var singleInvoice = await client.GetFromJsonAsync<InvoiceDto>($"/api/invoice/{invoiceCreated.Id}");

        singleInvoice.Should().NotBeNull();
        singleInvoice?.Customer.Should()
            .BeEquivalentTo(customer, options => options
                .Excluding(customer => customer.TypeCustomer));
        singleInvoice?.ProductsInvoice.Should()
            .HaveCount(1).And.SatisfyRespectively(
                first =>
                {
                    first.Quantity = 2;
                    first.Product.Id = product.Id;
                    first.Product.Name = product.Name;
                    first.Product.ApplyIva = product.ApplyIva;
                    first.Product.Value = product.Value;
                }
            );
    }

    [Fact]
    public async Task PostClientsSuccess()
    {
        await using var webApp = new ApiApp();
        var serviceCollection = webApp.GetServiceCollection();
        using var scope = serviceCollection.CreateScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var customer = new CustomerDataBuilder().Build();
        var product = new ProductDataBuilder().Build();
        await CreateCustomer(scope, customer);
        await CreateProduct(scope, product);
        await unitOfWork.SaveAsync(new CancellationTokenSource().Token);
        InsertInvoiceCommand invoice = new InsertInvoiceCommandBuilder()
            .WithCustomerId(customer.Id)
            .WithProductsInvoice([new(product.Id, 2)])
            .Build();
        var client = webApp.CreateClient();

        var request = await client.PostAsJsonAsync("/api/invoice/", invoice);

        request.EnsureSuccessStatusCode();
        var invoiceId = JsonSerializer.Deserialize<Guid>(await request.Content.ReadAsStringAsync(), GetOptions());
        var responseData = await client.GetFromJsonAsync<InvoiceDto>($"/api/invoice/{invoiceId}");
        responseData.Should().NotBeNull();
        responseData?.Id.Should().NotBeEmpty();
        responseData?.Customer.Id.Should().Be(customer.Id);
        responseData?.ValueTotal.Should().Be(238);
    }

    [Fact]
    public async Task PostClientsSuccessCancel()
    {
        await using var webApp = new ApiApp();
        var customer = new CustomerDataBuilder().WithType(TypeCustomer.Special).Build();
        var product = new ProductDataBuilder().Build();
        var invoiceCreated = await CreateInvoice(webApp, customer, product);
        var client = webApp.CreateClient();

        var request = await client.PostAsJsonAsync($"/api/invoice/{invoiceCreated.Id}/cancel", string.Empty);

        request.EnsureSuccessStatusCode();
        var responseData = await client.GetFromJsonAsync<InvoiceDto>($"/api/invoice/{invoiceCreated.Id}");
        responseData.Should().NotBeNull();
        responseData?.State.Should().Be(InvoiceState.Canceled.ToString());
    }

    static async Task<Invoice> CreateInvoice(ApiApp webApp, Customer customer, Product product)
    {
        var serviceCollection = webApp.GetServiceCollection();
        using var scope = serviceCollection.CreateScope();
        await CreateCustomer(scope, customer);
        await CreateProduct(scope, product);
        var invoiceRepository = scope.ServiceProvider.GetRequiredService<IRepository<Invoice>>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        ICollection<ProductInvoice> productsInvoice = [new ProductInvoiceDataBuilder().WithProduct(product).WithQuantity(2).Build()];
        var invoice = new InvoiceDataBuilder()
        .WithCustomer(customer)
        .WithProductsInvoice(productsInvoice)
        .Build();

        var invoiceCreated = await invoiceRepository.AddAsync(invoice);
        await unitOfWork.SaveAsync(new CancellationTokenSource().Token);

        return invoiceCreated;
    }

    static async Task CreateCustomer(IServiceScope scope, Customer customer)
    {
        var customerRepository = scope.ServiceProvider.GetRequiredService<IRepository<Customer>>();
        await customerRepository.AddAsync(customer);
    }

    static async Task CreateProduct(IServiceScope scope, Product product)
    {
        var productRepository = scope.ServiceProvider.GetRequiredService<IRepository<Product>>();
        await productRepository.AddAsync(product);
    }

    static JsonSerializerOptions GetOptions()
    {
        return new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }
}
