using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository 
    { 

        private readonly StoreContext _context;

        // Inject StoreContext into ProductRepository Class using constructor dependency injection
        public ProductRepository(StoreContext context) // StoreContext: name of the class we want to inject
        {
            _context = context;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await  _context.Products
            
                .Include( p => p.ProductBrand)
                .Include( p => p.ProductType)
                .FirstOrDefaultAsync( p => p.Id == id );
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _context.Products
                .Include( p => p.ProductBrand)
                .Include( p => p.ProductType)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductType.ToListAsync();
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.ProductBrand.ToListAsync();
        } 
    }
}