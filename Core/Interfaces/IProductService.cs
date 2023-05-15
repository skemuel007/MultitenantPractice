using Core.Dtos;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductService
    {
        Task<Product> CreateAsync(string name, string description, int rate);
        Task<Product> GetByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetAllAsync();
        string CreateWithBackgroundJob(List<ProductsDto> products);
    }
}
