using Flight.Domain.Products.Entity;
using Flight.Domain.Products.Port;
using Flight.Infrastructure.Ports;

namespace Flight.Infrastructure.Adapters
{
    [Repository]
    public class ProductRepository(IRepository<Product> productRepository) : IProductRepository
    {
        public async Task<Product> AddAsync(Product product) => await productRepository.AddAsync(product);

        public async Task DeleteAsync(Product product) => await Task.Run(() => productRepository.DeleteAsync(product));

        public async Task<IEnumerable<Product>> GetAllAsync() => await productRepository.GetManyAsync();


        public async Task<Product> GetByIdAsync(Guid id) => await productRepository.GetOneAsync(id);

        public async Task<int> GetCountAsync() => await productRepository.GetCountAsync();

        public async Task UpdateAsync(Product product) => await Task.Run(() => productRepository.UpdateAsync(product));
    }
}
