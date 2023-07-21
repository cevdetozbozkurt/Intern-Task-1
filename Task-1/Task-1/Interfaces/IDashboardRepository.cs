using System.Diagnostics;
using Task_1.Models;

namespace Task_1.Interfaces
{
    public interface IDashboardRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        bool Save();
        bool Add(Product product);
        bool UpdateProduct(Product product);
        bool Delete(Product product);
    }
}
