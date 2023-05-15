using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Hangfire;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IBackgroundJobClient _backgroundJobs;
        public ProductService( ApplicationDbContext context,
            IBackgroundJobClient backgroundJobs)
        {
            _context = context;
            _backgroundJobs = backgroundJobs;
        }

        public async Task<Product> CreateAsync(string name, string description, int rate)
        {
            var product = new Product(name, description, rate);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public string CreateWithBackgroundJob(List<ProductsDto> products)
        {
            var jobId = _backgroundJobs.Enqueue<ProductService>(x => x.insertProduct(products));
            return jobId.ToString();
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public void insertProduct(List<ProductsDto > products)
        {
            List<Product> productsToInsert = new List<Product>();
            foreach (var product in products)
            {
                var newProduct = new Product(product.Name, product.Description, product.Rate);
                productsToInsert.Add(newProduct);

            }

            _context.Products.AddRange(productsToInsert);
            _context.SaveChanges();
        }
    }
}
