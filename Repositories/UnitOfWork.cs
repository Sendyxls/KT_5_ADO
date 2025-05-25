using Microsoft.EntityFrameworkCore;
using StoreApi.Data;
using StoreApi.Models;

namespace StoreApi.Repositories
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        private readonly AppDbContext _context = context;

        private IRepository<Category>? _categories;
        private IRepository<Product>? _products;

        public IRepository<Category> Categories
            => _categories ??= new Repository<Category>(_context);

        public IRepository<Product> Products
            => _products ??= new Repository<Product>(_context);

        public async Task CompleteAsync()
            => await _context.SaveChangesAsync();
    }
}