using System.Diagnostics;
using Task_1.Models;

namespace Task_1.Interfaces
{
    public interface IDashboardRepository
    {
        Task<Customer> GetUserById(string id);
        Task<Customer> GetUserByIdNoTracking(string id);
        bool Save();
        bool UpdateUser(Customer user);
    }
}
