using StoreApi.Models;
using StoreApi.Repositories;

namespace StoreApi.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<Category> Categories { get; }
        IRepository<Product> Products { get; }
        Task CompleteAsync();
    }
}